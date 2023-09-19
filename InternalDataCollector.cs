using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class InternalDataCollector
    {
        public InternalDataCollector()
        {
            PlayerMoneyTracker = new Dictionary<int, List<Pair<int, int>>>(6);

            PlayerMoneyTracker[0] = new List<Pair<int, int>>(1000);
            PlayerMoneyTracker[1] = new List<Pair<int, int>>(1000);
            PlayerMoneyTracker[2] = new List<Pair<int, int>>(1000);
            PlayerMoneyTracker[3] = new List<Pair<int, int>>(1000);
            PlayerMoneyTracker[4] = new List<Pair<int, int>>(1000);
            PlayerMoneyTracker[5] = new List<Pair<int, int>>(1000);

            TileDataTracker = new Dictionary<int, Pair<ulong, int>>(40);

            for (int i = 0; i < 40;  i++)
            {
                TileDataTracker[i] = new Pair<ulong, int>(0, 0);
            }
        }


        public void AddMoneyData(int index, int money, int debt)
        {
            PlayerMoneyTracker[index].Add(new Pair<int, int>(money, debt));
        }


        public void AddTileData(int index, ulong profit, int passes)
        {
            TileDataTracker[index].first = profit;
            TileDataTracker[index].second = passes;
        }

        private Dictionary<int, List<Pair<int, int>>> m_playerMoneyTracker;
        private Dictionary<int, Pair<ulong, int>> m_tileDataTracker;

        internal Dictionary<int, List<Pair<int, int>>> PlayerMoneyTracker { get => m_playerMoneyTracker; set => m_playerMoneyTracker = value; }
        internal Dictionary<int, Pair<ulong, int>> TileDataTracker { get => m_tileDataTracker; set => m_tileDataTracker = value; }
    }
}


class Pair<T, U> : ICloneable
{
    public Pair()
    {

    }

    public Pair(T first, U second)
    {
        this.first = first;
        this.second = second;
    }

    public Pair(Pair<T, U> other)
    {
        first = other.first;
        second = other.second;
    }

    public T first;
    public U second;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

