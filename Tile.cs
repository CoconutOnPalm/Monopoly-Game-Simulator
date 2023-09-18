using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLayer
{

    public enum Property
    {
        isPurchasable,  // can player purchase this tile
        isStreetTile,   // is this tile a town/place or is it a utility
        isTransport,    // transport
        isPublicService,// power plants-like tiles
        isChanceCard,   // for chance and community chest tiles
        isPaycheck,     // start/parking tiles
        isPenalty,      // penalty 1M & 2M tiles
        isPolice,       // police only
        isJail          // jail only
    }

    public enum Color
    {
        Brown,
        Cyan,
        Magenta,
        Orange,
        Red,
        Yellow,
        Green,
        Blue,
        Utility,    // Transport, Power plants
        Other
    }


    internal class Price
    {

        public Price() { }

        public Price(int[] prices)
        {
            m_prices = prices;
        }

        public Price(int price)
        {
            for (int i = 0; i < m_prices.Length; i++)
            {
                m_prices[i] = price;
            }
        }

        // copy constructor
        public Price(Price other)
        {
            for (int i = 0; i < m_prices.Length; i++)
            {
                m_prices[i] = other.m_prices[i];
            }
        }

        public int GetPrice(int level)
        {
            if (level < 0 || level > 5)
            {
                return 0;
            }

            return m_prices[level];
        }

        public void SetPrice(int[] prices)
        {
            m_prices = prices;
        }

        public void SetPrice(int price)
        {
            for (int i = 0; i < m_prices.Length; i++)
            {
                m_prices[i] = price;
            }
        }


        private int[] m_prices = { 0, 0, 0, 0, 0, 0 };
    }




    internal class Tile
    {
        public Tile(string name, int position, Color color, HashSet<Property> properties, Price price, int value)
        {
            Name = new string(name.ToCharArray());
            Color = color;
            m_price = new Price(price);
            Value = value;
            m_position = position;
            m_totalProfit = 0;
            m_total_passes = 0;

            m_properties = new HashSet<Property>(properties);
            m_owner = GameControlHub.emptyPlayer;
            m_level = 0;
        }

        public Tile()
        {
            Name = "undefined";
            Color = Color.Other;
            m_price = new Price();
            Value = 0;
            m_totalProfit = 0;

            m_position = -1;

            m_properties = new HashSet<Property>();
            m_owner = GameControlHub.emptyPlayer;
            m_level = 0;
        }


        public HashSet<Property> Properties => m_properties;



        public void SetOwner(Player player)
        {
            m_owner = player;
        }

        public Player getOwner() => m_owner;

        public void SwapOwners(Player present_owner, Player future_owner)
        {
            present_owner.OwnedTiles.Remove(m_position);
            future_owner.OwnedTiles.Add(m_position);

            m_owner = future_owner;
        }


        public int getFare()
        {
            return m_price.GetPrice(Level);
        }


        public override string ToString()
        {
            if (m_properties.Contains(Property.isPolice))
            {
                return "!";
            }
            else if (m_properties.Contains(Property.isJail))
            {
                return "J";
            }
            else if (m_properties.Contains(Property.isPaycheck))
            {
                return "O";
            }
            else if (m_properties.Contains(Property.isPenalty))
            {
                return "$";
            }
            else if (m_properties.Contains(Property.isPublicService))
            {
                return "S";
            }
            else if (m_properties.Contains(Property.isChanceCard))
            {
                return "?";
            }
            else if (m_properties.Contains(Property.isStreetTile))
            {
                return "#";
            }
            else if (m_properties.Contains(Property.isTransport))
            {
                return "T";
            }
            else
            {
                return "";
            }
        }


        public void Print()
        {
            switch (Color)
            {
                case Color.Other:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case Color.Brown:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Color.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Color.Orange:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Color.Utility:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }

            Console.Write(this.ToString());

            Console.ResetColor();
        }


        public int Position => m_position;

        public int Level
        {
            set
            {
                if (m_level < 0 || m_level > 5)
                {
                    m_level = 0;
                }
                else
                {
                    m_level = value;
                }
            }
            get => m_level;
        }

        public Price Price { get => m_price; }

        public ulong TotalProfit { get => m_totalProfit; set => m_totalProfit = value; }

        internal Color Color { get => m_color; set => m_color = value; }
        public string Name { get => m_name; set => m_name = value; }
        public int Value { get => m_value; set => m_value = value; }
        public int TotalPasses { get => m_total_passes; set => m_total_passes = value; }

        private readonly int m_position;

        private string m_name;

        private Color m_color;
        private HashSet<Property> m_properties;

        private Price m_price;  // going to this tile costs this amount
        private int m_value; // it takes m_value money to buy this tile
        private int m_level; // each house +1 lv, hotel = 5 lv


        private Player m_owner;
        private ulong m_totalProfit;
        private int m_total_passes; // how many times a player stepped on this tile
    }
}
