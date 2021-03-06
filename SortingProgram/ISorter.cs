﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SortingProgram
{
    public interface ISorter
    {
        string getNextA();
        string getNextB();
        bool appliedCompare(int difference_val_A_B);
        List<string> getOutput();
        int getMINTime();
        int getMAXTime();
        void setProgress(int progress);
        int getProgress();
    }
}
