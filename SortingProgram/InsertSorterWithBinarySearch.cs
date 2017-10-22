using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingProgram
{
    class InsertSorterWithBinarySearch : ISorter
    {
        private List<string> source;
        private List<string> output;

        private bool sortingFinished = false;

        private int will_insert_in;
        private int insert_ceiling;
        private int insert_floor;

        private int performing_cursor;

        private int length;
        private int worstTime;
        private int bestTime;

        public InsertSorterWithBinarySearch(List<String> source)
        {
            this.source = source;
            this.performing_cursor = 1;
            this.will_insert_in = 0;
            this.length = source.Count;
            
            output = new List<string>();
            output.Add(source[0]);
            
            this.insert_ceiling = 0;
            this.insert_floor = output.Count;

            if (this.length < 2) sortingFinished = true;

            for (int i = 2; i <= length; i++)
            {
                worstTime += (int)Math.Ceiling(Math.Log(i, 2));
                bestTime += (int)Math.Floor(Math.Log(i, 2));
            }
            
        }

        public bool appliedCompare(int difference_val_A_B)
        {
            if (difference_val_A_B > 0)
            {
                this.insert_ceiling = (this.insert_ceiling + this.insert_floor) / 2 + 1;
            }
            else
            {
                this.insert_floor = (this.insert_ceiling + this.insert_floor) / 2;
            }

            if (this.insert_floor == this.insert_ceiling)
            {
                this.output.Insert(this.insert_ceiling, source[this.performing_cursor]);
                this.performing_cursor++;
                this.sortingFinished = this.performing_cursor >= source.Count;

                this.insert_ceiling = 0;
                this.insert_floor = this.output.Count;
            }
            return sortingFinished;
        }

        public int getMAXTime()
        {
            return worstTime;
        }

        public int getMINTime()
        {
            return bestTime;
        }

        public string getNextA()
        {
            if (sortingFinished) return "";
            return this.output[(this.insert_ceiling+this.insert_floor)/2];
        }

        public string getNextB()
        {
            if (sortingFinished) return "";
            return this.source[this.performing_cursor];
        }

        public List<string> getOutput()
        {
            return output;
        }
    }
}
