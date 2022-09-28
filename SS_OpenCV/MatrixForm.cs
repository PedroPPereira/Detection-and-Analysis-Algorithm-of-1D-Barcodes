using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class MatrixForm : Form
    {
        public MatrixForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr!=45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }        
        
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8 && chr != 45)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number");
            }
        }
    }
}
