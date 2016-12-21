using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPlanner
{
    interface IPriorityQueue<T>
    {
        T getTopElement();
        void removeTopElement();
        void addElement(T element, int priority);
        bool isEmpty();
        void changePriorityByIndex(int index, int newPriority);
    }
    class PriorityQueue<T> : IPriorityQueue<T>
    {
        private Heap<KeyValuePair<T, int>> heap;
        public KeyValuePair<T, int>[] getArray()
        {
            return heap.heap;
        }
        public PriorityQueue()
        {
            heap = new Heap<KeyValuePair<T, int>>(100, (a, b) =>
            {
                if (a.Value > b.Value)
                    return -1;
                if (a.Value < b.Value)
                    return 1;
                return 0;
            });
        }

        public void addElement(T element, int priority)
        {
            heap.Add(new KeyValuePair<T, int>(element, priority));
        }

        public bool isEmpty()
        {
            return heap.isEmpty();
        }

        public void removeTopElement()
        {
            if (heap.isEmpty())
                throw new Exception("Queue is empty!");
            else
                heap.RemoveTopValue();
        }

        public T getTopElement()
        {
            if (heap.isEmpty())
                throw new Exception("Queue is empty!");
            else
                return heap.GetTopValue().Key;
        }
        public void changePriorityByIndex(int index, int newPriority)
        {
            KeyValuePair<T, int> tmp = new KeyValuePair<T, int>(heap.GetByIndex(index).Key, newPriority);
            heap.DeleteByIndex(index);
            heap.Add(tmp);
        }
        public int getTopPriority()
        {
            if (heap.isEmpty())
                throw new Exception("Queue is empty!");
            else
                return heap.GetTopValue().Value;
        }
    }
}
