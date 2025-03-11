using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void Imput(int n)
        {
            Queue<int> st = new Queue<int>();

            for (int i = 1; i <= (int)numericUpDown1.Value; i++)
            {
                st.Enqueue(i);
            }
            label2.Text += $"Размерность : {st.Count}\n";
            label2.Text += $"Верхний элемент : {(int)numericUpDown1.Value}\n";
            label2.Text += $"Очередь : ";
            foreach (var i in st)
            {
                label2.Text += i + " ";
            }

            st.Clear();
            label2.Text += $"\nНовая размерность : {st.Count}";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32((int)numericUpDown1.Value);
            Imput(n);
            
        }
    }
}
