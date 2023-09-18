using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class InternalDataCollector
    {
        public InternalDataCollector()
        {
            m_playerMoneyTracker = new Dictionary<int, List<int>>(6);

            m_playerMoneyTracker[0] = new List<int>(1000);
            m_playerMoneyTracker[1] = new List<int>(1000);
            m_playerMoneyTracker[2] = new List<int>(1000);
            m_playerMoneyTracker[3] = new List<int>(1000);
            m_playerMoneyTracker[4] = new List<int>(1000);
            m_playerMoneyTracker[5] = new List<int>(1000);

            m_tileDataTracker = new Dictionary<int, Pair<int, int>>(40);

            for (int i = 0; i < 40;  i++)
            {
                m_tileDataTracker[i] = new Pair<int, int>(0, 0);
            }
        }

        private Dictionary<int, List<int>> m_playerMoneyTracker;

        private Dictionary<int, Pair<int, int>> m_tileDataTracker;
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

