using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public static class Algorithms
    {
        #region Sorting Algorithms (quicksort)
        /// <summary>
        /// Quick sorting algorithms for Directory item based on the full path
        /// </summary>
        /// <param name="array"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>

        public static List<DirectoryItem> quickSort(List<DirectoryItem> array, int low, int high)
        {
            if (low < high)
            {
                int pi = partition(array, low, high);
                quickSort(array, low, pi - 1);
                quickSort(array, pi + 1, high);
            }
            return array;
        }
        public static int partition(List<DirectoryItem> array, int low, int high)
        {
            string piviot = array[high].FullPath;
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (string.Compare(array[j].FullPath, piviot) < 0)
                {
                    i++;
                    DirectoryItem temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            DirectoryItem temp2 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp2;
            return i + 1;
        }
        #endregion

        #region Searching Algrorithms (binary search)
        /// <summary>
        /// Binary search for both string arrays
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int binarySearch(List<DirectoryItem> array, string target)
        {
            int high = array.Count - 1;
            int low = 0;
            int mid = -1;
            while(low < high)
            {
                mid = (high + low) / 2;
                if(array[mid].FullPath == target)
                {
                    return mid;
                }else if(string.Compare(array[mid].FullPath, target) > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return mid;
        }
        #endregion
    }
}
