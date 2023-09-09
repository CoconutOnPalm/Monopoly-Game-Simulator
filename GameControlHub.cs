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
    internal class GameControlHub
    {
        public GameControlHub()
        {
            //Tiles = new Dictionary<string, Tile>(TILE_COUNT)
            //{
            //    // NOTE: Elektrociepłownia and ALBA are not purchasable because they are so bad that no one buys them irl

            //    {
            //        "Start",
            //        new Tile("Start", 0, Color.Other,
            //    new HashSet<Property> { Property.isPaycheck },
            //    new Price(), 0)
            //    },

            //    {
            //        "Krzyki",
            //        new Tile("Krzyki", 1, Color.Brown,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 2, 10, 30, 90, 160, 250 }), 60)
            //    },
            //    {
            //        "Kasa Społeczna 1",
            //        new Tile("Kasa Społeczna", 2, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(0), 0)
            //    },
            //    {
            //        "Archikatedra Św. Jana",
            //        new Tile("Archikatedra Św. Jana", 3, Color.Brown,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 4, 20, 60, 180, 320, 450 }), 60)
            //    },

            //    {
            //        "Gazeta",
            //        new Tile("Gazeta", 4, Color.Other,
            //    new HashSet<Property> { Property.isPenalty },
            //    new Price(200), 0)
            //    },

            //    {
            //        "Koleje Dolnośląskie",
            //        new Tile("Koleje Dolnośląskie", 5, Color.Utility,
            //    new HashSet<Property> { Property.isTransport, Property.isPurchasable },
            //    new Price(new int[] { 0, 25, 50, 100, 200, 200 }), 200)
            //    },

            //    {
            //        "Panorama Racławicka",
            //        new Tile("Panorama Racławicka", 6, Color.Cyan,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 6, 30, 90, 270, 400, 550 }), 100)
            //    },
            //    {
            //        "Szansa 1",
            //        new Tile("Szansa", 7, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(), 0)
            //    },
            //    {
            //        "Park Szczytnicki",
            //        new Tile("Park Szczytnicki", 8, Color.Cyan,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 6, 30, 90, 270, 400, 550 }), 100)
            //    },
            //    {
            //        "Ogród Botaniczny",
            //        new Tile("Ogród Botaniczny", 9, Color.Cyan,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 8, 40, 100, 300, 450, 600 }), 120)
            //    },

            //    {
            //        "Więzienie",
            //        new Tile("Więzienie", 10, Color.Other,
            //    new HashSet<Property> { Property.isJail },
            //    new Price(), 0)
            //    },

            //    {
            //        "Uniwersytet Medyczny",
            //        new Tile("Uniwersytet Medyczny", 11, Color.Magenta,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 10, 50, 150, 450, 625, 750 }), 140)
            //    },
            //    {
            //        "Elektrociepłownia Wrocław",
            //        new Tile("Elektrociepłownia Wrocław", 12, Color.Utility,
            //    new HashSet<Property> { Property.isPublicService },
            //    new Price(0), 150)
            //    },
            //    {
            //        "Uniwersytet Przyrodniczy",
            //        new Tile("Uniwersytet Przyrodniczy", 13, Color.Magenta,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 10, 50, 150, 450, 625, 750 }), 140)
            //    },
            //    {
            //        "Uniwersytet Ekonomiczny",
            //        new Tile("Uniwersytet Ekonomiczny", 14, Color.Magenta,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 12, 60, 180, 500, 700, 900 }), 160)
            //    },

            //    {
            //        "Dworzec PKP",
            //        new Tile("Dworzec PKP", 15, Color.Utility,
            //    new HashSet<Property> { Property.isTransport, Property.isPurchasable },
            //    new Price(new int[] { 0, 25, 50, 100, 200, 200 }), 200)
            //    },

            //    {
            //        "Arkady Wrocławskie",
            //        new Tile("Arkady Wrocławskie", 16, Color.Orange,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 14, 70, 200, 550, 750, 950 }), 180)
            //    },
            //    {
            //        "Kasa Społeczna 2",
            //        new Tile("Kasa Społeczna", 17, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(), 0)
            //    },
            //    {
            //        "Aleja Bielany",
            //        new Tile("Aleja Bielany", 18, Color.Orange,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 14, 70, 200, 550, 750, 950 }), 180)
            //    },
            //    {
            //        "Pasaż Grunwaldzki",
            //        new Tile("Pasaż Grunwaldzki", 19, Color.Orange,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 16, 80, 220, 600, 800, 1000 }), 200)
            //    },

            //    {
            //        "Parking",
            //        new Tile("Parking", 20, Color.Other,
            //    new HashSet<Property> { Property.isPaycheck },
            //    new Price(), 0)
            //    },

            //    {
            //        "Green2Day",
            //        new Tile("Green2Day", 21, Color.Red,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 18, 90, 250, 700, 875, 1050 }), 220)
            //    },
            //    {
            //        "Szansa 2",
            //        new Tile("Szansa", 22, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(), 0)
            //    },
            //    {
            //        "Pegaz",
            //        new Tile("Pegaz", 23, Color.Red,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 18, 90, 250, 700, 875, 1050 }), 220)
            //    },
            //    {
            //        "Buisness Garden",
            //        new Tile("Buisness Garden", 24, Color.Red,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 20, 100, 300, 750, 925, 1100 }), 240)
            //    },

            //    {
            //        "MPK Wrocław",
            //        new Tile("MPK Wrocław", 25, Color.Utility,
            //    new HashSet<Property> { Property.isTransport, Property.isPurchasable },
            //    new Price(new int[] { 0, 25, 50, 100, 200 , 200 }), 200)
            //    },

            //    {
            //        "Grape Hotel",
            //        new Tile("Grape Hotel", 26, Color.Yellow,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 22, 110, 330, 800, 975, 1150 }), 260)
            //    },
            //    {
            //        "Most Grunwaldzki",
            //        new Tile("Most Grunwaldzki", 27, Color.Yellow,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 22, 110, 330, 800, 975, 1150 }), 260)
            //    },
            //    {
            //        "ALBA",
            //        new Tile("ALBA", 28, Color.Utility,
            //    new HashSet<Property> { Property.isPublicService },
            //    new Price(), 150)
            //    },
            //    {
            //        "Rynek Ratusz",
            //        new Tile("Rynek Ratusz", 29, Color.Yellow,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 24, 120, 360, 850, 1025, 1200 }), 280)
            //    },

            //    {
            //        "Policja",
            //        new Tile("Policja", 30, Color.Other,
            //    new HashSet<Property> { Property.isPolice },
            //    new Price(), 50)
            //    },

            //    {
            //        "Sky Tower",
            //        new Tile("Sky Tower", 31, Color.Green,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 26, 130, 390, 900, 1100, 1275 }), 300)
            //    },
            //    {
            //        "Uniwersytet Wrocławski",
            //        new Tile("Uniwerstet Wrocławski", 32, Color.Green,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 26, 130, 390, 900, 1100, 1275 }), 300)
            //    },
            //    {
            //        "Kasa Społeczna 3",
            //        new Tile("Kasa Społeczna", 33, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(), 0)
            //    },
            //    {
            //        "Politechnika Wrocławska",
            //        new Tile("Politechnika Wrocławska", 34, Color.Green,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 28, 150, 450, 1000, 1200, 1400 }), 320)
            //    },

            //    {
            //        "Polinka",
            //        new Tile("Polinka", 35, Color.Utility,
            //    new HashSet<Property> { Property.isTransport, Property.isPurchasable },
            //    new Price(new int[] { 0, 25, 50, 100, 200 , 200 }), 200)
            //    },
            //    {
            //        "Szansa 3",
            //        new Tile("Szansa", 36, Color.Other,
            //    new HashSet<Property> { Property.isChanceCard },
            //    new Price(), 0)
            //    },

            //    {
            //        "Hala Stulecia",
            //        new Tile("Hala Stulecia", 37, Color.Blue,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 35, 175, 500, 1100, 1300, 1500 }), 350)
            //    },
            //    {
            //        "Radio Wrocław",
            //        new Tile("Radio Wrocław", 38, Color.Other,
            //    new HashSet<Property> { Property.isPenalty },
            //    new Price(), 100)
            //    },
            //    {
            //        "Ostrów Tumski",
            //        new Tile("Ostrów Tumski", 39, Color.Blue,
            //    new HashSet<Property> { Property.isPurchasable, Property.isStreetTile },
            //    new Price(new int[] { 50, 200, 600, 1400, 1700, 2000 }), 400)
            //    }
            //};


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

                    while(reader.Read())
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



        public void Run()
        {
            for (int i = 0; i < GAMES_PER_SIMULATION; i++)
            {
                int n = 0;
                while (m_simulation_running)
                {
                    this.NextTurn();
                    n++;
                }

                //System.Diagnostics.Debugger.Break();

                foreach (var player in Players)
                {
                    if (player.Debt == 0 && player.Money > 0) // money as safety check
                    {
                        // game is won when player does not loose and his debt is >0
                        player.Win();
                    }
                }

                m_player_who_lost.Loose();

                m_player_who_lost = emptyPlayer;
                m_simulation_running = true;

            }

        }


        public void NextTurn()
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
                        //Console.WriteLine("{0} payed {1} to {2}", player.Name, tile.getFare(), owner.Name);
                    }
                }

                if (CheckDebt(player)) // player out of money and debt = game over
                {
                    m_simulation_running = false;
                }

                AutoPayDebt(player);
                CheckForMoneyOverflow(player);


                //Console.WriteLine("Player: {0} \t moved by: {1} \t his position: {2}", player.Name, first + second, player.Position);

                if (first == second)
                    i--; // duplicate - roll one more time
            }

            //Console.WriteLine();
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


        void CheckForMoneyOverflow(Player player)
        {
            if (player.Money > 2000000)
            {
                m_simulation_running = false;
            }
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

        public static readonly int TILE_COUNT = 40;
        public static readonly int MAX_DEBT = 5000;
        public static readonly int START_TILE_PAYOUT = 200; // payout for passing 'start' tile
        public static readonly int GAMES_PER_SIMULATION = 1000; // how many games are played before resoults are shown
        public static readonly Player emptyPlayer = new Player("_empty_player");
    }
}
