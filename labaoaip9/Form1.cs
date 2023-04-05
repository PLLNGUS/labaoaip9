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
    
        Figure figure;
        Square square;
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
            
            if (op.symbolOperator == 'S')
            {
                this.figure = new Square
                (Convert.ToInt32                   
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToString
                (operands.Pop().value));
                op = new Operator(this.figure.Draw, 'S');
                ShapeContainer.AddFigure(figure);
                op.operatorMethod();
            }
            if (op.symbolOperator == 'M')
            {
                int Y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                int X = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                string Name = Convert.ToString(operands.Pop().value);
                foreach(Figure figure in ShapeContainer.figureList) 
                {
                    if (figure.name == Name)
                    {
                      figure.MoveTo(X, Y);
                    }
                }
                op = new Operator(this.figure.Draw, 'M');
                ShapeContainer.AddFigure(figure);
                op.operatorMethod();
            }
            if (op.symbolOperator == 'D')
            {
               
                string Name = Convert.ToString(operands.Pop().value);
                foreach (Figure figure in ShapeContainer.figureList)
                {
                    if (figure.name == Name)
                    {
                        figure.DeleteF(figure);
                    }
                }
                op = new Operator(this.figure.Draw, 'M');
                ShapeContainer.AddFigure(figure);
                op.operatorMethod();
            }

        }

        private bool IsNotOperation(char item)
        {
            if (!(item == 'S' || item == 'M' || item == 'D'  || item == ',' || item == '(' || item == ')'))
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
            init = new Init();
            shapeContainer = new ShapeContainer();
            InitializeComponent();
            Init.bitmap = new
    Bitmap(pictureBox1.ClientSize.Width,
    pictureBox1.ClientSize.Height);
            Init.pen = new Pen(Color.Black, 5);

            Init.pictureBox = pictureBox1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
               
            {
                operators.Clear();
                operands.Clear();
                try
                {
                    bool flag = true;
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
                                else if (((textBox1.Text[i + 1] == ',' || textBox1.Text[i + 1] == ')'))
                               && (Char.IsDigit(textBox1.Text[i])) && (textBox1.Text[i - 1] == ','))
                                {
                                    this.operands.Push(new Operand(textBox1.Text[i]));
                                    continue;
                                }
                                else if ((textBox1.Text[i + 1] == ',')
                                && (Char.IsDigit(textBox1.Text[i])))
                                {

                                    flag = true;
                                    continue;
                                }

                            }
                           
                        }
                        else if (textBox1.Text[i] == 'S')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator(textBox1.Text[i]));
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        else if (textBox1.Text[i] == 'M')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator(textBox1.Text[i]));
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        else if (textBox1.Text[i] == 'D')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator(textBox1.Text[i]));
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        
                        else if (textBox1.Text[i] == '(')
                        {
                            this.operators.Push(OperatorContainer.FindOperator(textBox1.Text[i]));
                        }
                        else if (textBox1.Text[i] == ')')
                        {
                            do
                            {
                                if (operators.Peek().symbolOperator == '(')
                                //

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
                        


                    }

                    if (operators.Peek() != null)
                    {
                        this.SelectingPerformingOperation(operators.Peek());
                        richTextBox1.AppendText(textBox1.Text);
                        richTextBox1.AppendText("Команда успешно выполнена\n");
                    }
                    else
                    {
                        MessageBox.Show("Введенной операции не существует");
                    }
            }
                catch (InvalidOperationException) {
                richTextBox1.AppendText(textBox1.Text);
                    richTextBox1.AppendText("Команда успешно выполнена\n");
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }