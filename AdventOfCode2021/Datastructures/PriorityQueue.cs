using System;
using System.Text;

namespace AdventOfCode2021.Datastructures
{
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        public static int DEFAULT_CAPACITY = 100;
        public int size; // Number of elements in heap
        public T[] array; // The heap array

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public PriorityQueue()
        {
            array = new T[DEFAULT_CAPACITY];
        }

        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------
        public int Size()
        {
            return size;
        }

        public void Clear()
        {
            array = new T[DEFAULT_CAPACITY];
            size = 0;
        }

        public void Add(T x)
        {
            if (size + 1 == array.Length)
                Array.Resize(ref array, array.Length * 2);

            // Percolate up
            int hole = ++size;
            array[0] = x;

            // Iterate as long as x < the parent node
            for (; x.CompareTo(array[hole / 2]) < 0; hole /= 2)
            {
                array[hole] = array[hole / 2];
            }

            array[hole] = x;
        }

        // Removes the smallest item in the priority queue
        public T Remove()
        {
            if (size == 0)
                throw new PriorityQueueEmptyException();
            T minItem = array[1];
            array[1] = array[size--];
            // Percolate down
            PercolateDown(1);
            return minItem;
        }

        public override string ToString()
        {
            if (size == 0)
                return "";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i <= size; i++)
            {
                stringBuilder.Append(array[i]);
                if (i != size)
                    stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();
        }

        private void PercolateDown(int hole)
        {
            int child;
            T tmp = array[hole];

            for (; hole * 2 <= size; hole = child)
            {
                child = hole * 2;
                if (child != size && array[child + 1].CompareTo(array[child]) < 0)
                    child++;
                if (array[child].CompareTo(tmp) < 0)
                    array[hole] = array[child];
                else
                    break;
            }

            array[hole] = tmp;
        }
        
        public void AddFreely(T x)
        {
            array[++size] = x;
        }

        public void BuildHeap()
        {
            for (int i = size / 2; i > 0; i--)
                PercolateDown(i);
        }

        public class PriorityQueueEmptyException : System.Exception
        {
            // Is thrown when Remove is called on an empty queue
        }
    }
}