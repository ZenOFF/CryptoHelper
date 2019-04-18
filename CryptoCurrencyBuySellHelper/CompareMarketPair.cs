using System.Collections.Generic;

namespace NoviceCryptoTraderAdvisor
{
    internal class CompareMarketPair : IComparer<MarketPair>
    {
        private DirectionSort _directionSort;

        public CompareMarketPair(DirectionSort direction)
        {
            _directionSort = direction;
        }

        public int Compare(MarketPair x, MarketPair y)
        {
            if (_directionSort == DirectionSort.Ascending)
            {
                if (x.RSIValue.CompareTo(y.RSIValue) > 0)
                {
                    return 1;
                }
                else if (x.RSIValue.CompareTo(y.RSIValue) < 0)
                {
                    return -1;
                }
            }
            else if (_directionSort == DirectionSort.Descending)
            {
                if (x.RSIValue.CompareTo(y.RSIValue) > 0)//60 > 50
                {
                    return -1;
                }
                else if (x.RSIValue.CompareTo(y.RSIValue) < 0)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}