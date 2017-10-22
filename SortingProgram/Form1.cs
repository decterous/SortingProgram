using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingProgram
{
    public partial class Form1 : Form
    {
        public static TextBox output;
        public static Form1 instance;

        public Form2 popup;
        public Form1()
        {
            InitializeComponent();
            output = textBox1;
            instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.running) return;

            String input = textBox1.Text;
            String[] input_splits = input.Split('\n');

            List<String> ori_input = new List<string>();
            foreach (String ins in input_splits)
            {
                if(ins.Length > 0) ori_input.Add(ins.Trim());
            }
            
            if (ori_input.Count >= 2) {
                popup = new Form2(ori_input);
                popup.Show();
            }

            Thread my = new Thread(myThreadFunction);
            my.Start();
        }


        private void myThreadFunction()
        {
            while (Form2.running)
            {
                List<String> ins = popup.sortingEngine.getOutput();
                StringBuilder sb = new StringBuilder();
                foreach (String ars in ins)
                {
                    sb.AppendLine(ars);
                }
                updateTextBox(sb.ToString());
                Thread.Sleep(200);
            }
        }

        public delegate void delegateUpdateTextBox(String str);
        public void updateTextBox(String str)
        {
            if(this.textBox1.InvokeRequired)
            {
                delegateUpdateTextBox ut = new delegateUpdateTextBox(updateTextBox);
                this.Invoke(ut, str);
            }
            else
            {
                textBox1.Text = str;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String input = textBox1.Text;
            String[] input_splits = input.Split('\n');
            List<String> ori_input = new List<string>();
            foreach (String ins in input_splits)
            {
                if (ins.Length > 0) ori_input.Add(ins.Trim());
            }

            Random random = new Random();

            List<String> rand_input = new List<string>();
            while (ori_input.Count > 0)
            {
                int i = 0;
                if (ori_input.Count > 1)
                    i = random.Next() % ori_input.Count;
                rand_input.Add(ori_input[i]);
                ori_input.RemoveAt(i);
            }
            
            StringBuilder sb = new StringBuilder();
            foreach (String ars in rand_input)
            {
                sb.AppendLine(ars);
            }
            textBox1.Text = sb.ToString();
        }
    }
}
