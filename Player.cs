using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLayer
{
    public class Player
    {
        public Player(string name, int money = 1500, int start_debt = 0)
        {
            m_ID = s_global_ID_registry;
            s_global_ID_registry++;

            m_name = name;
            m_alive = true;
            Playing = true;

            m_money = money;
            m_debt = start_debt;

            m_owned_tiles = new HashSet<int>();
        }


        public int Move(int amount)
        {
            m_position += amount;

            if (m_position >= 40)
                m_money += GameControlHub.START_TILE_PAYOUT;

            m_position %= GameControlHub.TILE_COUNT; // board cycle

            return m_position;
        }


        public void SetPosition(int position)
        {
            if (position < 0 || position >= 40)
            {
                m_position = 0;
                return;
            }

            m_position = position;
        }


        public void AddOwnership(Tile tile, int level = 0)
        {
            m_owned_tiles.Add(tile.Position);
            tile.SetOwner(this);

            tile.Level = level;
        }

        public void RemoveOwnership(Tile tile)
        {
            m_owned_tiles.Remove(tile.Position);
            tile.SetOwner(GameControlHub.emptyPlayer);
        }

        public bool IsOwnerOf(Tile tile)
        {
            return m_owned_tiles.Contains(tile.Position);
        }



        public bool isOwnerOf(int tile_position)
        {
            return m_owned_tiles.Contains(tile_position);
        }


        public void Loose() => Lost_games++;

        public void Win() => Won_games++;

        public HashSet<int> OwnedTiles => m_owned_tiles;

        public int Debt
        {
            get => m_debt; set => m_debt = value;
        }

        public int Money
        {
            set
            {
                m_money = value;
                if (m_money < 0)
                    m_alive = false;
            }
            get { return m_money; }
        }

        public int StartMoney
        {
            set
            {
                m_startMoney = value;
                m_money = value;
            }
            get { return m_startMoney; }
        }

        public int StartDebt
        {
            set 
            {
                m_startDebt = value;
                m_debt = value;
            }
            get { return m_startDebt; }
        }

        public int Position
        {
            set
            {
                if (value < 0 || value >= 40)
                {
                    m_position = 0;
                }
                else
                {
                    m_position = value;
                }
            }
            get { return m_position; }
        }

        public bool Alive
        {
            get { return m_alive; }
        }

        public bool Playing { get; set; }

        public string Name { get => m_name; set => m_name = value; }

        public int Lost_games { get => m_lost_games; set => m_lost_games = value; }
        public int Won_games { get => m_won_games; set => m_won_games = value; }

        private string m_name;
        private int m_position;
        private int m_money;
        private int m_debt;
        private int m_startMoney;
        private int m_startDebt;

        private HashSet<int> m_owned_tiles;

        private readonly int m_ID;

        private int m_lost_games = 0;
        private int m_won_games = 0;
        private bool m_alive;

        private static int s_global_ID_registry = 0; // used to init Player's ID without duplicates
    }
}
