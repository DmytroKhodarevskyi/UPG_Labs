using System.Drawing;

namespace Labs.Tests
{
    public class DynamicArrayTests
    {
        private DynamicArray<int> _arr = new();

        [Fact]
        public void DA_Initialisation()
        {
            Assert.Equal(1, _arr.Length);
            Assert.Equal(0, _arr.Count);
        }

        [Fact]
        public void DA_Add_AddSingle()
        {
            _arr.Add(1);

            Assert.Equal(1, _arr.Count);
            Assert.Equal(1, _arr.Length);

            var item = _arr.GetItem(0);

            Assert.Equal(1, item);
        }

        [Fact]
        public void DA_GetItem_GetSingle()
        {
            _arr.Add(1);

            var item = (int)_arr.GetItem(0);
            Assert.Equal(1, item);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        public void DA_Insert_InsertAtZero(params int[] arr)
        {
            _arr.Insert(0, arr[0]);

            Assert.Equal(1, _arr.Length);
            Assert.Equal(_arr.GetItem(0), arr[0]);

            _arr.Insert(0, arr[1]);

            Assert.Equal(2, _arr.Length);
            Assert.Equal(_arr.GetItem(0), arr[1]);

            _arr.Insert(0, arr[2]);

            Assert.Equal(4, _arr.Length);
            Assert.Equal(_arr.GetItem(0), arr[2]);
        }

        [Fact]
        public void DA_Clear_Clear()
        {
            _arr.Add(1);
            _arr.Add(1);
            _arr.Add(1);

            Assert.Equal(3, _arr.Count);

            _arr.Clear();

            Assert.Equal(0, _arr.Count);
            Assert.False(_arr.Contains(1));
        }

        [Fact]
        public void DA_Capacity_SetCapacityZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _arr.Capacity = 0);
        }

        [Fact]
        public void DA_Capacity_SetCapacityLowerThanAll()
        {
            int n = 5;
            for (int i = 0; i < n; i++)
            {
                _arr.Add(i);
            }

            Assert.Equal(8, _arr.Length);
            Assert.Equal(5, _arr.Count);

            _arr.Capacity = 4;

            Assert.Equal(4, _arr.Length);
            Assert.Equal(4, _arr.Count);

            Assert.Equal(0, _arr.GetItem(0));
            Assert.Equal(3, _arr.GetItem(3));
        }

        [Fact]
        public void DA_Capacity_SetCapacityLowerLengthHigherCount()
        {
            int n = 5;
            for (int i = 0; i < n; i++)
            {
                _arr.Add(i);
            }

            Assert.Equal(8, _arr.Length);
            Assert.Equal(5, _arr.Count);

            _arr.Capacity = 6;

            Assert.Equal(6, _arr.Length);
            Assert.Equal(5, _arr.Count);

            Assert.Equal(0, _arr.GetItem(0));
            Assert.Equal(4, _arr.GetItem(4));
        }

        [Fact]
        public void DA_Capacity_SetCapacityHigherThanAll()
        {
            _arr.Add(1);

            Assert.Equal(1, _arr.Count);
            Assert.Equal(1, _arr.Length);

            _arr.Capacity = 10;

            Assert.Equal(1, _arr.Count);
            Assert.Equal(10, _arr.Length);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        public void DA_IndexOf_CheckIndex(params int[] arr)
        {
            _arr.Insert(0, arr[0]);
            _arr.Insert(1, arr[1]);
            _arr.Insert(2, arr[2]);

            int i = _arr.IndexOf(arr[0]);
            Assert.Equal(0, i);
        }

        [Fact]
        public void DA_Add_MultipleAdd()
        {
            for (int i = 0; i < 100_000; i++)
            {
                _arr.Add(i);
            }
        }
    }
}
