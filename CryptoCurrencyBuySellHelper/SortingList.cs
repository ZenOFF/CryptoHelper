using System;
using System.Collections.Generic;
using System.Linq;

namespace NoviceCryptoTraderAdvisor
{
    internal class SortingList
    {
        //шейкерная сортировка
        private void ShakerSort(List<string[]> TempListMarket)
        {
            double CurrentRSIValue = 0;
            double NextRSIValue = 0;

            int left = 0;
            int right = TempListMarket.Count - 1;

            while (left <= right)
            {
                for (int i = left; i < right; i++)
                {
                    CurrentRSIValue = Convert.ToDouble(TempListMarket[i].GetValue(2).ToString());
                    NextRSIValue = Convert.ToDouble(TempListMarket[i + 1].GetValue(2).ToString());

                    if (CurrentRSIValue > NextRSIValue)
                        Swap(TempListMarket, i, i + 1);
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    CurrentRSIValue = Convert.ToDouble(TempListMarket[i].GetValue(2).ToString());

                    NextRSIValue = Convert.ToDouble(TempListMarket[i - 1].GetValue(2).ToString());

                    if (NextRSIValue > CurrentRSIValue)
                        Swap(TempListMarket, i - 1, i);
                }
                left++;
            }
        }

        private void Swap(List<string[]> TempActiveMarketList, int i, int j)
        {
            string[] Temp = TempActiveMarketList[i];
            TempActiveMarketList[i] = TempActiveMarketList[j];
            TempActiveMarketList[j] = Temp;
        }

        public IEnumerable<T> Quicksort<T>(IEnumerable<T> InputList, IComparer<T> comparer)
        {
            if (InputList.Count() < 2)
            {
                return InputList;
            }

            T pivot = InputList.First();

            List<T> vv = Quicksort(InputList.Where(x => comparer.Compare(x, pivot) < 0), comparer)
            .Concat(new List<T> { pivot })
            .Concat(Quicksort(InputList.Where(x => comparer.Compare(x, pivot) > 0), comparer)).ToList();

            return vv;
        }
    }
}