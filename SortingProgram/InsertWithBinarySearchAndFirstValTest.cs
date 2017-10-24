﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingProgram
{
    //improved when the origin list is highly sorted
    class InsertWithBinarySearchAndFirstValTest : ISorter
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

        public InsertWithBinarySearchAndFirstValTest(List<String> a)
        {
            this.source = new List<string>();
            this.source.AddRange(a);
            this.performing_cursor = 1;
            this.will_insert_in = 0;
            this.length = this.source.Count;

            output = new List<string>();
            output.Add(this.source[0]);

            this.insert_ceiling = 0;
            this.insert_floor = output.Count;

            if (this.length < 2) sortingFinished = true;
            worstTime = bestTime = this.source.Count - 1;
            for (int i = 2; i < length; i++)
            {
                worstTime += (int)Math.Ceiling(Math.Log(i, 2));
            }
            
        }

        public bool appliedCompare(int difference_val_A_B)
        {
            if (this.insert_floor == this.output.Count && this.insert_ceiling == 0)
            {
                if (difference_val_A_B > 0)
                {
                    this.insert_ceiling = this.insert_floor;
                }
                else
                {
                    this.insert_floor --;
                }
            }
            else
            {
                if (difference_val_A_B > 0)
                {
                    this.insert_ceiling = (this.insert_ceiling + this.insert_floor) / 2 + 1;
                }
                else
                {
                    this.insert_floor = (this.insert_ceiling + this.insert_floor) / 2;
                }
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

            if (this.insert_floor == this.output.Count && this.insert_ceiling == 0)
            {
                return this.output[this.insert_floor-1];
            }

            return this.output[(this.insert_ceiling + this.insert_floor) / 2];
        }

        public string getNextB()
        {
            if (sortingFinished) return "";
            return this.source[this.performing_cursor];
        }

        public List<string> getOutput()
        {
            List<string> ret = new List<string>();
            ret.AddRange(output);
            ret.AddRange(source.GetRange(output.Count, source.Count - output.Count));
            return ret;
        }
    }
}