using System;
using System.Collections.Generic;
using System.Text;

namespace SortingProgram
{
    public class MergeSorter:ISorter
    {
        private bool sortingComplete = false;

        private List<string> source;

        private List<SortingElement> elementListA;
        private List<SortingElement> elementListB;
        private SortingElement targetSortingElement;

        private int length;
        private int nowSpliter;

        private int timeMINCount;
        private int timeMAXCount;
        public MergeSorter(List<string> source)
        {
            this.source = source;
            this.length = source.Count;
            this.elementListA = new List<SortingElement>();
            int counter = 0;
            foreach(string str in source)
            {
                elementListA.Add(new SortingElement(counter));
                counter++;
            }
            this.elementListB = new List<SortingElement>();

            this.targetSortingElement = new SortingElement();

            this.nowSpliter = 1;

            if (nowSpliter >= length) this.sortingComplete = true;

            this.nowSpliter = this.nowSpliter * 2;
            
            int dSpliter = 2;
            while(dSpliter <= this.length)
            {
                int eleCount = this.length / dSpliter;
                timeMINCount += eleCount * dSpliter;
                timeMAXCount += eleCount * 2 * dSpliter;
                if (this.length % dSpliter > 0)
                {
                    timeMINCount += 1;
                    timeMAXCount += dSpliter;
                }
                dSpliter = dSpliter * 2;
            }

        }

        public int getMINTime()
        {
            return this.timeMINCount;
        }

        public int getMAXTime()
        {
            return this.timeMAXCount;
        }
        public string getNextA()
        {
            int target = elementListA[0].partList[0];
            return this.source[target];
        }

        public string getNextB()
        {
            if (sortingComplete) return "";
            int target = elementListA[1].partList[0];
            return this.source[target];
        }

        public bool appliedCompare(int difference_val_A_B)
        {
            int target = 0;
            if (difference_val_A_B > 0)
            {
                target = this.elementListA[0].partList[0];
                this.elementListA[0].partList.RemoveAt(0);
            }
            else
            {
                target = this.elementListA[1].partList[0];
                this.elementListA[1].partList.RemoveAt(0);
            }
            this.targetSortingElement.partList.Add(target);
            
            if(elementListA[0].partList.Count == 0
                || elementListA[1].partList.Count == 0)
            {
                if(elementListA[0].partList.Count == 0)
                {
                    this.targetSortingElement.partList.AddRange(this.elementListA[1].partList);
                }
                else if(elementListA[1].partList.Count == 0)
                {
                    this.targetSortingElement.partList.AddRange(this.elementListA[0].partList);
                }

                this.elementListB.Add(targetSortingElement);
                this.targetSortingElement = new SortingElement();

                this.elementListA.RemoveRange(0, 2);

                if(elementListA.Count == 0)
                {
                    this.elementListA = this.elementListB;
                    this.elementListB = new List<SortingElement>();
                    this.sortingComplete = this.nowSpliter >= length;
                    this.nowSpliter = this.nowSpliter * 2;
                }
                else if(elementListA.Count == 1)
                {
                    this.elementListB.AddRange(this.elementListA);
                    this.elementListA.RemoveAt(0);

                    this.elementListA = this.elementListB;
                    this.elementListB = new List<SortingElement>();
                    this.sortingComplete = this.nowSpliter >= length;
                    this.nowSpliter = this.nowSpliter * 2;
                }
            }
            return this.sortingComplete;
        }

        public List<string> getOutput()
        {
            List<SortingElement> outputList = elementListB;
            if (sortingComplete) outputList = elementListA;
            List<string> ret = new List<string>();
            foreach(SortingElement sort in outputList)
            {
                foreach(int i in sort.partList)
                {
                    ret.Add(source[i]);
                }
            }
            return ret;
        }

        public void setProgress(int progress)
        {
        }

        public int getProgress()
        {
            throw new NotImplementedException();
        }

        private class SortingElement
        {
            public List<int> partList;

            public SortingElement(int i)
            {
                this.partList = new List<int>();
                partList.Add(i);
            }

            public SortingElement()
            {
                this.partList = new List<int>();
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (partList.Count > 0) sb.Append("{");
                foreach (int i in partList)
                {
                    sb.Append(i);
                    sb.Append(",");
                }
                if (sb.Length > 0) sb[sb.Length - 1] = '}';
                return sb.ToString();
            }
        }
    }

}