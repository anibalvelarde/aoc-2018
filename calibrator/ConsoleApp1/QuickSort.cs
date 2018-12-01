using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class QuickSort
    {

        public int[] array;
        public int len;

        public void sort()
        {
            run(0, len - 1);
        }

        private void run(int left, int right)
        {
            int pivot, leftend, rightend;

            leftend = left;
            rightend = right;
            pivot = array[left];

            while (left < right)
            {
                while ((array[right] >= pivot) & amp; &amp; (left < right))
                {
                    right--;
                }

                if (left != right)
                {
                    array[left] = array[right];
                    left++;
                }

                while ((array[left] >= pivot) & amp; &amp; (left < right))
                {
                    left++;
                }

                if (left != right)
                {
                    array[right] = array[left];
                    right--;
                }
            }

            array[left] = pivot;
            pivot = left;
            left = leftend;
            right = rightend;

            if (left < pivot) { sort(left, pivot - 1); }
            if (right > pivot)
            {
                sort(pivot + 1, right);
            }
        }
    }
}
