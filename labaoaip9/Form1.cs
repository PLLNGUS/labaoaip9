﻿using System;
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

        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private int ConvertCharToInt(object bruh)
        { 
            return Convert.ToInt32(bruh.ToString());
            
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

                    }


                }
            }
        }
    }