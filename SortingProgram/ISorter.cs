using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
