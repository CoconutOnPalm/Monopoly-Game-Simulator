using Monopoly_Game_Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SimulationLayer
{
    public enum SimualationExitCode
    {
        Default,
        TurnLimitExceeded,
        BankWentBankrupt,
        Error
    }

    public enum TurnExitCode
    {
        Default,
        BankWentBankrupt,
        Error
    }



    internal class GameControlHub
    {
        public GameControlHub()
        {
            string simEntryDataConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SimulationEntryData.mdf;Integrated Security=True";

            using (SqlConnection sqlConnection = new SqlConnection(simEntryDataConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [TileEntryData];", sqlConnection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    Tiles = new Dictionary<int, Tile>(TILE_COUNT);

                    while (reader.Read())
                    {
                        LoadTileFromDatabase(reader);
                    }
                }
                sqlConnection.Close();
            }


            foreach (var tile in Tiles)
            {
                Console.Write(tile.Key + " \t");
                Console.Write(tile.Value.Name + " \t");
                Console.Write(tile.Value.Position + " \t");
                Console.Write(tile.Value.Color + " \t");
                Console.Write(tile.Value.Price.GetPrice(0) + " \t");
                Console.Write(tile.Value.Properties.ElementAt(0) + " \t");
                Console.WriteLine();
            }


            using (SqlConnection sqlConnection = new SqlConnection(simEntryDataConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [PlayerEntryData];", sqlConnection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    Players = new List<Player>();

                    while (reader.Read())
                    {
                        LoadPlayerFromDatabase(reader);
                    }
                }
                sqlConnection.Close();
            }


            foreach (var player in Players)
            {
                Console.Write(player.Name + " \t");
                Console.Write(player.Money + " \t");
                Console.Write(player.Debt + " \t");
                Console.WriteLine();
            }


            foreach (var player in Players)
            {
                Console.WriteLine("{0}'s properties:", player.Name);
                foreach (var property in player.OwnedTiles)
                {
                    Console.WriteLine(Tiles.ElementAt(property).Key);
                }
                Console.WriteLine();

                UpdateTransportCards(player);
            }
        }



        void LoadTileFromDatabase(SqlDataReader reader)
        {
            Func<string, HashSet<Property>> getProperties = str =>
            {
                HashSet<Property> properties = new HashSet<Property>();
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '1')
                    {
                        properties.Add((Property)i);
                    }
                }
                return properties;
            };

            Tiles[(int)reader["id"]] = new Tile(
                reader["TileName"].ToString(),
                (int)reader["Position"],
                (Color)reader["Color"],
                getProperties(reader["Properties"].ToString()),
                new Price(new int[] { (int)reader["PriceLevel0"], (int)reader["PriceLevel1"], (int)reader["PriceLevel2"], (int)reader["PriceLevel3"], (int)reader["PriceLevel4"], (int)reader["PriceLevel5"] }),
                (int)reader["Value"]);
        }

        void LoadPlayerFromDatabase(SqlDataReader reader)
        {
            Players.Add(new Player(reader["PlayerName"].ToString(), (int)reader["EntryBalance"], (int)reader["EntryDebt"]));
        }




        public void AssignProperties()
        {

        }


        /// <summary>
        /// Runs the simulation
        /// </summary>
        /// <param name="simulationMode">True -> multisim mode | False -> singlesim mode</param>
        public (SimualationExitCode, int) Run(bool simulationMode)
        {
            SimualationExitCode exitCode = new SimualationExitCode();
            exitCode = SimualationExitCode.Default;

            // multisim mode
            if (simulationMode)
            {
                for (int i = 0; i < GAMES_PER_SIMULATION; i++)
                {
                    while (m_simulation_running)
                    {
                        if (NextTurn() == TurnExitCode.BankWentBankrupt)
                            exitCode = SimualationExitCode.BankWentBankrupt;
                    }

                    // decide which player wins
                    foreach (var player in Players)
                    {
                        if (player.Debt == 0 && player.Money > 0) // money as safety check
                        {
                            // game is won when player does not loose and his debt is > 0
                            player.Win();
                        }
                    }

                    m_player_who_lost.Loose();

                    m_player_who_lost = emptyPlayer;
                    m_simulation_running = true;
                }

                return (exitCode, 0);
            }
            // singlesim mode
            else
            {
                int turns = 0;
                while (m_simulation_running)
                {
                    if (NextTurn() == TurnExitCode.BankWentBankrupt)
                        exitCode = SimualationExitCode.BankWentBankrupt;

                    turns++;

                    if (turns > 1000)
                    {
                        m_simulation_running = false;
                        exitCode = SimualationExitCode.TurnLimitExceeded;
                    }
                }

                //System.Diagnostics.Debugger.Break();

                foreach (var player in Players)
                {
                    if (player.Debt == 0 && player.Money > 0) // money as safety check
                    {
                        // game is won when player does not loose and his debt is > 0
                        player.Win();
                    }
                }

                m_player_who_lost.Loose();

                m_player_who_lost = emptyPlayer;
                m_simulation_running = true;

                return (exitCode, turns);
            }
        }


        public TurnExitCode NextTurn()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                var player = Players[i];

                int first = Utils.Random.RollDice(), second = Utils.Random.RollDice();
                int movement = player.Move(first + second);
                var tile = Tiles.ElementAt(player.Position).Value;

                var parking = Tiles[20].Price; // parking alias


                // going to jail via police tile
                if (tile.Properties.Contains(Property.isPolice))
                {
                    player.SetPosition(10); // jail

                    if (player.Money >= 50)
                    {
                        player.Money -= 50;
                        parking.SetPrice(parking.GetPrice(0) + 50); // add 50 to parking
                    }
                    else
                    {
                        player.Money = 0; // edge case, would have to implement jailtime system
                    }

                    continue;
                }

                // parking tile
                if (tile == Tiles[20])
                {
                    player.Money += parking.GetPrice(0);
                    parking.SetPrice(0);
                }

                // paying for standing on someone's tile
                if (!player.IsOwnerOf(tile))
                {
                    if (tile.getOwner() == emptyPlayer)
                    {
                        // buying tiles will be added later (maybe)
                    }
                    else
                    {
                        var owner = tile.getOwner();

                        player.Debt += tile.getFare();
                        owner.Money += tile.getFare();
                        tile.TotalProfit += (ulong)tile.getFare();
                    }
                }

                if (CheckDebt(player)) // player out of money and debt = game over
                {
                    m_simulation_running = false;
                }

                AutoPayDebt(player);

                if (CheckForMoneyOverflow(player))
                {
                    return TurnExitCode.BankWentBankrupt;
                }

                if (first == second)
                    i--; // duplicate - roll one more time
            }


            return TurnExitCode.Default;
        }


        /// <summary>
        /// checks if player's debt is below limit, and tries to pay it if it is possible
        /// </summary>
        /// <param name="player"></param>
        /// <returns>debt OK => false | player lost => true</returns>
        bool CheckDebt(Player player)
        {
            if (player.Debt > MAX_DEBT)
            {
                player.Money -= player.Debt - MAX_DEBT;
                player.Debt = MAX_DEBT;

                if (player.Money < 0)
                {
                    m_player_who_lost = player;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// automatically pays all debts if amount of money is large enough
        /// </summary>
        /// <param name="player"></param>
        void AutoPayDebt(Player player)
        {
            if (player.Money > MAX_DEBT * 1.5)
            {
                player.Money -= player.Debt;
                player.Debt = 0;
            }
        }


        bool CheckForMoneyOverflow(Player player)
        {
            if (player.Money > 2000000)
            {
                m_simulation_running = false;
                return true;
            }

            return false;
        }


        void UpdateTransportCards(Player player)
        {
            int trasport_cards = 0;

            foreach (var c in player.OwnedTiles)
            {
                if (Tiles.ElementAt(c).Value.Properties.Contains(Property.isTransport))
                {
                    trasport_cards++;
                }
            }

            foreach (var c in player.OwnedTiles)
            {
                if (Tiles.ElementAt(c).Value.Properties.Contains(Property.isTransport))
                {
                    Tiles.ElementAt(c).Value.Level = trasport_cards;
                }
            }
        }


        private void AssignMultipleProperties(Player player, int[] properties, int level = 0)
        {
            foreach (var property in properties)
            {
                player.AddOwnership(Tiles[property], level);
            }
        }

        private void AssignMultipleProperties(Player player, Color[] streetColors, int level = 0)
        {
            List<int> properties = new List<int>();

            foreach (var color in streetColors)
            {
                foreach (var tile in Tiles)
                {
                    if (tile.Value.Color == color)
                    {
                        properties.Add(tile.Key);
                    }
                }
            }

            AssignMultipleProperties(player, properties.ToArray(), level);
        }


        public List<Player> Players;
        public Dictionary<int, Tile> Tiles;

        private bool m_simulation_running = true;
        private Player m_player_who_lost = emptyPlayer;

        public static int MAX_DEBT = 5000;
        public static int START_TILE_PAYOUT = 200; // payout for passing 'start' tile
        public static int GAMES_PER_SIMULATION = 1000; // how many games are played before resoults are shown
        public static readonly int TILE_COUNT = 40;
        public static readonly Player emptyPlayer = new Player("_empty_player");
    }
}
