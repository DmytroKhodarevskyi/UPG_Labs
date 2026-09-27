using System;
using System.Collections.Generic;
using System.Text;

namespace Labs
{
    public interface IDynamicArray<T>
    {
        public int Count { get; }
        public int Length { get; }

        /// <summary>
        /// Set the capacity
        /// </summary>
        public int Capacity { set; }

        /// <summary>
        /// Add an item to the top of the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item);

        /// <summary>
        /// Insert item at index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item);

        /// <summary>
        /// Clears the array without resizing it
        /// </summary>
        public void Clear();

        /// <summary>
        /// Print for debug
        /// </summary>
        public void Print();

        /// <summary>
        /// Remove item at index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index);

        /// <summary>
        /// Index of the item, exception if not found
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item);

        /// <summary>
        /// Check if item is in array
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item);

        /// <summary>
        /// Gets an item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetItem(int index);
    }

    public class DynamicArray<T> : IDynamicArray<T>
    {
        private T[] _items = new T[1];
        private int _count;
        public int Count => _count; // Current count of objects
        public int Length => _items.Length; // Maximum capacity

        private void Extend()
        {
            if (Count >= Length)
            {
                var extended = new T[Length * 2];
                _items.CopyTo(extended, 0);
                _items = extended;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

        private void Resize(int size)
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("size");

            var newarr = new T[size];
            int sizeOfCopy = Math.Min(size, Length);
            Array.Copy(_items, newarr, sizeOfCopy);
            _items = newarr;
            _count = Math.Min(size, _count);
        }

        public int Capacity
        {
            get => Length;
            set => Resize(value);
        }

        public void Print()
        {
            foreach (var item in _items)
            {
                Console.WriteLine($"Item: {item}");
            }

            Console.WriteLine($"Capacity: {Length}");
            Console.WriteLine($"Count: {Count}");
        }

        public void Add(T item)
        {
            Extend();
            _items[_count++] = item;
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);
            Extend();

            // Shift from index to +1
            Array.Copy(_items, index, _items, index + 1, Count - index);

            // Set item to index
            _items[index] = item;

            // Update count
            _count++;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);

            // Shift from index all left
            Array.Copy(_items, index + 1, _items, index, Count - index - 1);

            // Reset last element to default
            _items[Count] = default;

            // Update count
            _count--;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(item, _items[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            for (int i = 0; Count > 0; i++)
            {
                _items[i] = default;
                _count--;
            }
        }

        public T GetItem(int index)
        {
            CheckIndex(index);
            return _items[index];
        }
    }
}
