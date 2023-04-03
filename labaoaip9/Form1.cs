using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace labaoaip9
{
    public partial class Form1 : Form
    {
        Rectangle rectagle;
        Square square;
        Figure figure;
       
        ShapeContainer shapeContainer;
        Init init;

        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private int ConvertCharToInt(object bruh)
        { 
            return Convert.ToInt32(bruh.ToString());
            
        }
        private void SelectingPerformingOperation(Operator op)
        {
            if (op.symbolOperator == 'R')
            {
                this.figure = new Rectangle
                (Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToString
                (operands.Pop().value));
                op = new Operator(this.figure.Draw, 'R');
                ShapeContainer.AddFigure(figure);
                comboBox1.Items.Add(figure.name);
                op.operatorMethod();
            }


        }
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
        bool flag;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //выполняется обработка входной строки
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (IsNotOperation(textBox1.Text[i]))
                        {
                            if (!(Char.IsDigit(textBox1.Text[i])))
                            {
                                this.operands.Push(new Operand(textBox1.Text[i]));
                                continue;
                            }
                            else if (Char.IsDigit(textBox1.Text[i]))
                            {
                                if (Char.IsDigit(textBox1.Text[i + 1]))
                                {
                                    if (flag)
                                    {
                                        this.operands.Push(new Operand(textBox1.Text[i]));
                                    }
                                    this.operands.Push(new Operand(ConvertCharToInt(this.operands.Pop().value) * 10 + ConvertCharToInt(textBox1.Text[i + 1])));
                                    flag = false;
                                    continue;
                                }

                            }

                        }
                        else if ((textBox1.Text[i + 1] == ','
                               || textBox1.Text[i + 1] == ')')
                               && !(Char.IsDigit(textBox1.Text[i - 1])))
                        {
                            this.operands.Push(new Operand(ConvertCharToInt
                            (textBox1.Text[i])));
                            continue;
                        }
                        else if (textBox1.Text[i] == 'R')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator
                                (textBox1.Text[i]));
                            }
                        }
                        else if (textBox1.Text[i] == '(')
                        {
                            this.operators.Push(OperatorContainer.FindOperator
                            (textBox1.Text[i]));
                        }
                        else if (textBox1.Text[i] == ')')
                        {
                            do
                            {
                                if (operators.Peek().symbolOperator == '(')
                                {
                                    operators.Pop();
                                    break;
                                }
                                if (operators.Count == 0)
                                {
                                    break;
                                }
                            }
                            while (operators.Peek().symbolOperator != '(');
                        }
                        if (operators.Peek() != null)
                        {
                            this.SelectingPerformingOperation(operators.Peek());
                        }
                        else
                        {
                            MessageBox.Show("Введенной операции не существует");
                        }

                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


                
            }
            
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }