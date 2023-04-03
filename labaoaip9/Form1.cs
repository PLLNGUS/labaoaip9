using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labaoaip9
{
    public partial class Form1 : Form
    {

        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private bool IsNotOperation(char item)
        {
            if (!(item == 'R' || item == 'M' || item == 'E' || item == 'C' || item == 'S' || item == ',' || item == '(' || item == ')'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (IsNotOperation(textBox1.Text[i]))
                {
                    if (!(Char.IsDigit(textBox1.Text[i])))
                    {
                        this.operands.Push(new Operand(textBox1.Text[i]));
                        continue;
                    }


                }
            }
        }
    }