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
        int counter;
        String total;

        public ISorter sortingEngine;

        public Form2(List<String> a)
        {
            InitializeComponent();

            sortingEngine = new InsertWithBinarySearchAndFirstValTest(a);
            total = "best : "+sortingEngine.getMINTime()+"/worst : "+sortingEngine.getMAXTime();
            running = true;

            button1.Text = sortingEngine.getNextA();
            button2.Text = sortingEngine.getNextB();
            
            counter = 0;
            this.updateTitle();
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
            this.updateTitle();
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
            this.updateTitle();
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

        private void updateTitle()
        {
            this.label2.Text = "action count : " + counter + "     finishing in : " + total;
        }
    }
}
