using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SortingProgram.Form1;

namespace SortingProgram
{
    public partial class Form2 : Form
    {
        public static List<String> output;
        public static bool running = false;
        int iter_k;
        int iter_j;
        int length;
        int counter;
        String total;

        public ISorter sortingEngine;

        public Form2(List<String> a)
        {
            InitializeComponent();

            sortingEngine = new InsertSorterWithBinarySearch(a);
            total = "最好："+sortingEngine.getMINTime()+" 最糟："+sortingEngine.getMAXTime();
            running = true;

            button1.Text = sortingEngine.getNextA();
            button2.Text = sortingEngine.getNextB();

            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            counter = 0;
            //total = (a.Count - 2) * (a.Count - 1);
            //output = new List<string>();
            //output.AddRange(a);
            //iter_j = 1;
            //length = iter_k = a.Count - 1;
            this.Text = "排序进度 ：" + counter + " 仍需操作数：" + total;
            //button1.Text = output[iter_k - 1];
            //button2.Text = output[iter_k];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sortingEngine.appliedCompare(1))
            {
                this.Close();
            }

            button1.Text = sortingEngine.getNextA();
            button2.Text = sortingEngine.getNextB();

            counter++;
            //iter_k--;
            //if(iter_k < iter_j)
            //{
            //    iter_j++;
            //    if (iter_j > length) this.Close();
            //    iter_k = length;
            //}

            //button1.Text = output[iter_k - 1];
            //button2.Text = output[iter_k];
            this.Text = "排序进度 ：" + counter + "/" + total;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sortingEngine.appliedCompare(-1))
            {
                this.Close();
            }

            button1.Text = sortingEngine.getNextA();
            button2.Text = sortingEngine.getNextB();
            counter++;
            //String temp = output[iter_k];
            //output[iter_k] = output[iter_k - 1];
            //output[iter_k - 1] = temp;
            //iter_k--;
            //if (iter_k < iter_j)
            //{
            //    iter_j++;
            //    if (iter_j > length) this.Close();
            //    iter_k = length;
            //}

            //button1.Text = output[iter_k - 1];
            //button2.Text = output[iter_k];
            this.Text = "排序进度 ：" + counter + "/" + total;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;

            List<String> ins = sortingEngine.getOutput();
            StringBuilder sb = new StringBuilder();
            foreach (String ars in ins)
            {
                sb.AppendLine(ars);
            }
            Form1.instance.updateTextBox(sb.ToString());
        }
    }
}
