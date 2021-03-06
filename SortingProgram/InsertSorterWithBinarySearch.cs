﻿using System;
using System.Collections.Generic;
using System.Text;

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
            this.source = new List<string>();
            this.source.AddRange(source);
            this.performing_cursor = 1;
            this.will_insert_in = 0;
            this.length = this.source.Count;
            
            output = new List<string>();
            output.Add(this.source[0]);
            
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
            List<string> ret = new List<string>();
            ret.AddRange(output);
            ret.AddRange(source.GetRange(output.Count,source.Count - output.Count));
            return ret;
        }

        public int getProgress()
        {
            throw new NotImplementedException();
        }

        public void setProgress(int progress)
        {
        }
    }
}
