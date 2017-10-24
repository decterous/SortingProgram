using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingProgram
{
    public partial class Form1 : Form
    {
        public static string filename = "./data.dat";

        public static TextBox output;
        public static Form1 instance;

        public Form2 popup;

        public DataSource dataSource;

        public Form1()
        {
            InitializeComponent();
            output = textBox1;
            instance = this;

            if (File.Exists(filename))
            {
                this.LoadSave();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.running) return;

            if(dataSource == null) dataSource = new DataSource(0,textBox1.Text);
            
            if (!textBox1.Text.Equals(dataSource.input))
            {
                dataSource = new DataSource(0, textBox1.Text);
            }

            String[] input_splits = dataSource.input.Split('\n');
            List<String> ori_input = new List<string>();
            foreach (String ins in input_splits)
            {
                if(ins.Length > 0) ori_input.Add(ins.Trim());
            }
            
            if (ori_input.Count >= 2) {
                popup = new Form2(ori_input);
                popup.setProgress(dataSource.progress);
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

        private void LoadSave()
        {
            if (!File.Exists(filename)) return;
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            dataSource = bf.Deserialize(fs) as DataSource;
            this.textBox1.Text = dataSource.input;
            fs.Close();
        }
        
        internal void WriteSave()
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            dataSource = new DataSource(dataSource.progress, textBox1.Text);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, dataSource);
            fs.Close();
        }

        private void ToolStripMenuItem_save_Click(object sender, EventArgs e)
        {
            WriteSave();
        }
    }

    [Serializable]
    public class DataSource
    {
        public int progress;
        public string input;

        public DataSource(int v, string text)
        {
            this.progress = v;
            this.input = text;
        }
    }
}
