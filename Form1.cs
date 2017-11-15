using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Theory_of_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Formula formula = new Formula();

        public double funk(double wx, double wy)
        {
            Calkuer calkuer = new Calkuer();
            formula.constants[1] = wx;
            formula.constants[2] = wx;
            return (calkuer.Calculation(formula));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int n = 0, nmax = 0, c1 = 0, c2 = 0, c3 = 0, c4 = 0;
            double eps = 0;
            string function = textBox9.Text;
            Reader reader = new Reader();
            reader.input_string(function);
            formula = reader.Reading();
           //Получение значений n,nps,eps,c1,c2,c3,c4 из текстбоксов
            n = int.Parse(textBox1.Text);
            nmax = int.Parse(textBox2.Text);
            eps = double.Parse(textBox3.Text);
            c1 = int.Parse(textBox4.Text);
            c2 = int.Parse(textBox5.Text);
            c3 = int.Parse(textBox6.Text);
            c4 = int.Parse(textBox7.Text);
            //Объявление основных используемых переменных
            double[] x = new double[n];
            double[] x1 = new double[n];
            double[] y = new double[n];
            double[] y1 = new double[n];
            double[] pc = new double[n];
            double[] fi = new double[n];
            double[] x2 = new double[n];
            double[] y2 = new double[n];
            double[] d = new double[n];
            double[,] z1 = new double[n,9];
            double[,] a1 = new double[n,n];
            int ai = 1, ai1 = 0, ai2 = 0, ai3 = 0, ai4 = 0;
            double dx = (c2 - c1) / (n - 1), dy = (c4 - c3) / (n - 1);
            int i3=-1, i4;

            for (int i=0; i < n; i++)
            {
                for (int j =0; j < n; j++) {a1[i,j]=funk(c1 + (c2 - c1) * (i - 1), c3 + (c4 - c3) * (j - 1)); } //функция выбирается через checkbox
            }
           
            // метка 57 (Собираем массив А)
            double omax = 1000000;
            double omin = 0;

            bool repit = true; //baka! Не юзай метки.
            for (; repit;)
            {
                int k = 1;//понадобится в выводе ответа (i)

                for (int i = 1; i < n; i++) // I1
                {
                    pc[i] = pc[i] + a1[i, (n + 1) / 2];
                    if (pc[i] <= pc[k]) k = i;//уточнить условие
                }//метка 1

                x[k] = x[k] + 1;

                int g = 1;//понадобится в дальнейшем (J)

                for (int i = 1; i < n; i++)// J1
                {
                    fi[i] = fi[i] + a1[k, i];
                    if (fi[i] <= fi[g]) g = i;//уточнить условие
                } //метка 3

                y[g] = y[g] + 1;

                double omax1 = pc[k] / ai;
                double omin1 = fi[g] / ai;
                double c, r;
                if ((omax - omax1) < 0)
                {
                    omax = omax1; ai1 = ai;
                    ai3++;
                    c = 0; r = 0;
                    for (int i = 1; i < n; i++) //(i2)
                    {
                        x1[i] = x[i] / ai;
                        d[i] = Math.Abs(x1[i] - x2[i]);
                        c += d[i]; x2[i] = x1[i];

                        if (d[i] > d[(int)r]) r = i;
                    }
                    //метка 7


                    for (int i = 2; i < n; i++) //(i2)
                    {
                        i3 = n + 1 - i;
                        i4 = i3 - 1;
                        z1[i3, 5] = z1[i4, 5];
                        z1[i3, 6] = z1[i4, 6];
                    }
                    //метка 71
                    z1[1, 5] = c;
                    z1[1, 6] = d[(int)r];
                }
                //метка 8
                if ((omin - omin1) < 0)
                {
                    omin = omin1;
                    ai2 = ai; ai4++;
                    c = 0; r = 1;
                    for (int i = 1; i < n; i++) //(i2)
                    {
                        y1[i] = y[i] / ai;
                        d[i] = Math.Abs(y1[i] - y2[i]);
                        c += d[i]; y2[i] = y1[i];
                        if (d[i] > d[(int)r]) r = i;
                    } //метка 10

                    for (int i = 2; i < n; i++) //(i2)
                    {
                        i3 = n + 1 - i;
                        i4 = i3 - 1;
                        z1[i3, 7] = z1[i4, 7];
                        z1[i3, 8] = z1[i4, 8];
                    }
                    //метка 73
                    z1[1, 7] = c;
                    z1[1, 8] = d[(int)r];
                }
                ai++;
                if (ai > nmax)
                {
                    if (Math.Abs(omax - omin) > eps)
                    {
                        repit = false;
                    }
                }
            }
                //печать i3,ai,ai2,ai3,ai4
                double F = (omax + omin) / 2;
                //печать f, omax, omin
                for(int i = 1; i < n; i++)
                {
                    z1[i, 1] = x1[i];
                    z1[i, 2] = c4 + (i - 1) * dy;
                    z1[i, 3] = y1[i];
                    z1[i, 4] = c2 + (i - 1) * dx;
                }
            //вывод констант 
            textBox8.Multiline = true;
            textBox8.Text = "Вычисленные значения:" + Environment.NewLine;
            textBox8.Text += "i3 = " + i3 +  "; ai = " + ai + "; ai1 = " + ai1 + "; ai2 = " + ai2 + "; ai3 = " + ai3 + "; ai4 = " + ai4 + Environment.NewLine;
            textBox8.Text += "f = " + F + "; omax" + omax + "; omin = " + omin +  Environment.NewLine;
            //Вывод массива
            textBox8.Text += "Полученная матрица:" + Environment.NewLine;
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    textBox8.Text += z1[i, j] + " ";
                }
                    textBox8.Text += Environment.NewLine;
            }
            //переменные для нахождения максимина и минимакса
            double maxmin = 0, minmax = 0, local_min_j = 0, local_max_i = 0;
            //поиск наибольшего минимума
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0) local_min_j = z1[i, j];
                    if (local_min_j > z1[i, j]) local_min_j = z1[i, j];
                }
                if (i == 0) { maxmin = local_min_j; }
                if (maxmin < local_min_j) { maxmin = local_min_j; }
            }
            //поиск наименьшего максимума
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == 0) local_min_j = z1[i, j];
                    if (local_max_i < z1[i, j]) local_max_i = z1[i, j];
                }
                if (j == 0) { minmax = local_max_i; }
                if (minmax > local_max_i) { minmax = local_max_i; }
            }
            //вывод максимин и минимакс
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "MinMax = " + minmax + "MaxMin = " + maxmin;
            textBox8.Text += Environment.NewLine;
        }
    }
}
