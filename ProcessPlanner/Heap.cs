using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPlanner
{
    class Heap<T> 
    {
        public T[] heap;
        private int HeapSize = 0;
        private Func<int, int> left = x => 2 * x + 2;
        private Func<int, int> right = x => 2 * x + 1;
        private Func<int, int> ancestor = x => (x - 1) / 2;
        private Func<T, T, int> sortCriteria;
        public Heap(int size = 100, Func<T, T, int> sortCriteria = null)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException("Initial heap size could'nt be lower then 1!");
            if (sortCriteria == null)
                throw new ArgumentOutOfRangeException("Sort criteria should be explicitly defined!");
            this.sortCriteria = sortCriteria;
            heap = new T[size];
        }
        private void Swap(int x, int y)
        {
            var tmp = heap[x];
            heap[x] = heap[y];
            heap[y] = tmp;
        }
        private void SiftUp(int x)
        {
            if (x == 0)
                return;
            int a;
            if ((a = sortCriteria(heap[x], heap[ancestor(x)])) == 1)/*heap[x].CompareTo(heap[ancestor(x)]) == -1*/
            {
                Swap(x, ancestor(x));
                SiftUp(ancestor(x));
            }
        }
        private void SiftDown(int x)
        {
            if (x > HeapSize - 1)
                return;
            int maxIndex = x;
            T maxVal = default(T);
            if (right(x) < HeapSize && sortCriteria(heap[right(x)], heap[x]) == 1)
            {
                maxIndex = right(x);
                maxVal = heap[right(x)];
            }
            if (left(x) < HeapSize && sortCriteria(heap[left(x)], maxVal) == 1)
            {
                maxIndex = left(x);
                maxVal = heap[left(x)];
            }
            if (sortCriteria(maxVal, heap[x]) == -1)
                maxIndex = x;
            if (maxIndex != x)
            {
                Swap(x, maxIndex);
                SiftDown(maxIndex);
            }
        }
        public void Add(T elem)
        {
            if (HeapSize + 1 >= heap.GetLength(0))
                Array.Resize<T>(ref heap, (HeapSize + 1) * 2);
            heap[HeapSize++] = elem;
            SiftUp(HeapSize - 1);
        }
        public T GetTopValue()
        {
            if (HeapSize == 0)
                throw new InvalidOperationException("Heap is Empty!");
            T max = heap[0];
            RemoveTopValue(); //needed to fix sync problems
            return max;
        }
        public void RemoveTopValue()
        {
            if (HeapSize == 0)
                throw new InvalidOperationException("Heap is Empty!");
            heap[0] = heap[HeapSize - 1];
            heap[HeapSize - 1] = default(T);
            HeapSize--;
            SiftDown(0);
        }
        public void DeleteByIndex(int index)
        {
            if (index > HeapSize)
                throw new Exception("No such index!");
            heap[index] = default(T);
            HeapSize--;
            SiftDown(index);
        }
        public T GetByIndex(int index)
        {
            if (index > HeapSize)
                throw new Exception("No such index!");
            return heap[index];
        }
        public bool isEmpty()
        {
            return HeapSize == 0;
        }
    }
}
