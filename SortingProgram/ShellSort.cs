using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingProgram
{
    class ShellSort : ISorter
    {
        private bool sortingFinished = false;

        private List<string> source;

        private List<int> HibbardSequence;

        private int whereInHibbard;
        private int whereInOutput;

        private int time;

        public ShellSort(List<string> a)
        {
            source = a;
            
            HibbardSequence = new List<int>();
            int firstOfHibbard = (int)Math.Floor(Math.Log(source.Count, 2));
            while(firstOfHibbard >= 1)
            {
                HibbardSequence.Add((1 << firstOfHibbard) - 1);
                firstOfHibbard--;
            }
            whereInHibbard = 0;
            whereInOutput = 0;

            time = 0;
            foreach(int integer in HibbardSequence)
            {
                time += source.Count - integer;
            }
            
        }

        public bool appliedCompare(int difference_val_A_B)
        {
            if(difference_val_A_B < 0)
            {
                String temp = this.source[whereInOutput];
                this.source[whereInOutput] = this.source[HibbardSequence[whereInHibbard] + whereInOutput];
                this.source[HibbardSequence[whereInHibbard] + whereInOutput] = temp;
            }

            whereInOutput++;
            if(whereInOutput + HibbardSequence[whereInHibbard] == this.source .Count)
            {
                whereInHibbard++;
                whereInOutput = 0;

                sortingFinished = whereInHibbard >= this.HibbardSequence.Count;
            }

            return sortingFinished;
        }

        public int getMAXTime()
        {
            return time;
        }

        public int getMINTime()
        {
            return time;
        }

        public string getNextA()
        {
            if (sortingFinished) return "";
            return this.source[whereInOutput];
        }

        public string getNextB()
        {
            if (sortingFinished) return "";
            return this.source[HibbardSequence[whereInHibbard] + whereInOutput];
        }

        public List<string> getOutput()
        {
            return source;
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
