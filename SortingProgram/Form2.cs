using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static SortingProgram.Form1;

namespace SortingProgram
{
    public partial class Form2 : Form
    {
        public static List<String> output;
        public static bool running = false;
        int counter;

        public ISorter sortingEngine;

        public Form2(List<String> a)
        {
            InitializeComponent();

            sortingEngine = new InsertWithBinarySearchAndFirstValTest(a);
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

        internal void setProgress(int progress)
        {
            this.sortingEngine.setProgress(progress);
            button1.Text = sortingEngine.getNextA();
            button2.Text = sortingEngine.getNextB();
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
            instance.updateTextBox(sb.ToString());

            instance.dataSource.progress = sortingEngine.getProgress();
            instance.WriteSave();
        }

        private void updateTitle()
        {
            String total = "best : " + sortingEngine.getMINTime() + "/worst : " + sortingEngine.getMAXTime();
            this.toolStripStatusLabel1.Text = "action count : " + counter + "     finishing in : " + total;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.A)
            {
                this.button1_Click(sender, e);
            }
            else if(e.KeyData == Keys.D)
            {
                this.button2_Click(sender, e);
            }
        }
    }
}
