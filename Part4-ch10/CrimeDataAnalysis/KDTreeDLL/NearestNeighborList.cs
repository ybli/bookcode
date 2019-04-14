using System;
using System.Collections.Generic;
using System.Text;

namespace KDTreeDLL
{
    /// <summary>
    /// Bjoern Heckel's solution to the KD-Tree n-nearest-neighbor problem
    /// </summary>
    class NearestNeighborList
    {
        public static int REMOVE_HIGHEST = 1;
        public static int REMOVE_LOWEST = 2;

        PriorityQueue m_Queue = null;
        int m_Capacity = 0;

        // constructor
        public NearestNeighborList(int capacity)
        {
            m_Capacity = capacity;
            m_Queue = new PriorityQueue(m_Capacity, Double.PositiveInfinity);
        }

        public double getMaxPriority()
        {
            if (m_Queue.length() == 0)
            {
                return Double.PositiveInfinity;
            }
            return m_Queue.getMaxPriority();
        }

        public bool insert(Object _object, double priority)
        {
            if (m_Queue.length() < m_Capacity)
            {
                // capacity not reached
                m_Queue.add(_object, priority);
                return true;
            }
            if (priority > m_Queue.getMaxPriority())
            {
                // do not insert - all elements in queue have lower priority
                return false;
            }
            // remove object with highest priority
            m_Queue.remove();
            // add new object
            m_Queue.add(_object, priority);
            return true;
        }

        public bool isCapacityReached()
        {
            return m_Queue.length() >= m_Capacity;
        }

        public Object getHighest()
        {
            return m_Queue.front();
        }

        public bool isEmpty()
        {
            return m_Queue.length() == 0;
        }

        public int getSize()
        {
            return m_Queue.length();
        }

        public Object removeHighest()
        {
            // remove object with highest priority
            return m_Queue.remove();
        }

    }
}
