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


        public  double funk( double wx,  double wy)
        {
            Calkuer calkuer = new Calkuer();
            formula.constants[1] = (double)wx;
            formula.constants[2] = (double)wy;
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
            eps =  double.Parse(textBox3.Text);
            c1 = int.Parse(textBox4.Text);
            c2 = int.Parse(textBox5.Text);
            c3 = int.Parse(textBox6.Text);
            c4 = int.Parse(textBox7.Text);
            //Объявление основных используемых переменных
             double[] x = new  double[n];
             double[] x1 = new  double[n];
             double[] y = new  double[n];
             double[] y1 = new  double[n];
             double[] pc = new  double[n];
             double[] fi = new  double[n];
             double[] x2 = new  double[n];
             double[] y2 = new  double[n];
             double[] d = new  double[n];
             double[,] z1 = new  double[n,9];
             double[,] a1 = new  double[n,n];
            int ai = 1, ai1 = 0, ai2 = 0, ai3 = 0, ai4 = 0;
             double dx = (c2 - c1) / (n - 1), dy = (c4 - c3) / (n - 1);
            int i3=-1, i4;
            Stack< double> P1C = new Stack< double> ();
            Stack< double>  P1D= new Stack< double> ();
            Stack< double> P2C= new Stack< double>();
            Stack< double>  P2D= new Stack< double> ();
            Form forma;
            for (int i=0; i < n; i++)
            {
                for (int j =0; j < n; j++)
                {
                   a1[i,j]=funk(c1 + (c2 - c1) * (i - 1), c3 + (c4 - c3) * (j - 1));
                } //функция выбирается через checkbox
            }

            // метка 57 (Собираем массив А)
             double omax = 1000000; //~ max V(n))/n 
             double omin = 0; //~(min U(n))/n 

            int J=(n + 1) / 2; // случайно выбраный столбец (+ его инициализация)
            int I=0; // инициализация константы со строкой.
              double C=0;
            int K=0; // надо сейвить до цикла
            bool repit = true; //baka! Не юзай метки.
            for (; repit;)
            {
                I = 0;//I у нас счет с нуля, или поправку делай на массивы

                for (int i = 0; i < n; i++) // I1
                {
                    pc[i] = pc[i] + a1[i,J] ;
                    if (pc[i] <= pc[I]){ I = i;}// если I (элемент столбца) не минимальный, то I=i   pc - вектор первого игрока
                }//метка 1

                x[I] = x[I] + 1; // x имеет размерность количества строк и показывает сколько раз была выбранна I-я строка 

                J = 0;//запомнили первый столбец

                for (int j = 0; j < n; j++)// J1
                {
                    fi[j] = fi[j] + a1[I, j];
                    if (fi[j] >= fi[J]) J = j;// если J (элемент строки) не мax, то J=j   Fi - вектор второго игрока
                } //метка 3

                y[J] = y[J] + 1; // x имеет размерность количества столбцов и показывает сколько раз был выбранн J-й столбец 

                 double omax1 = pc[I] / ai; //Уточнили условия в начале min U(n))/n
                 double omin1 = fi[J] / ai; //Уточнили условия в начале max V(n))/n
                // метка 6
               if ((omax - omax1) >= 0) //  зачем то сравниваем начальные условия  
                {
                    omax = omax1; // уточнили начальное условие  
                    ai1 = ai; // определили ai1
                    ai3++;
                    C = 0; K = 0; //поправка на счет с нуля 
                    for (int i = 0; i < n; i++) //(i2) поправка на счет с нуля 
                    {
                        x1[i] = x[i] / ai;
                        d[i] = Math.Abs(x1[i] - x2[i]);
                        C += d[i];
                        x2[i] = x1[i];

                        if (d[i] > d[K]){ K = i;}
                    }
                    //метка 7

                    /*
                    for (int i = 1; i < n; i++) //(i2) поправка на счет с нуля Было:"i < n"
                    {
                        i3 = n - i; // нужна ли поправка? 2->1? Было:"i3 = n + 1 - i;"
                        i4 = i3 - 1;
                        z1[i3, 5] = z1[i4, 5];
                        z1[i3, 6] = z1[i4, 6];
                    }
                    //метка 71
                    z1[1, 5] = C;
                    z1[1, 6] = d[K];
                    */
                    P1C.Push(C);
                    P1D.Push(d[K]);
                }
                //метка 8 она же 5
                if ((omin - omin1) <= 0)
                {
                    omin = omin1;
                    ai2 = ai; ai4++;
                    C = 0;  K = 0;
                    for (int i = 0; i < n; i++) //(i2) поправка 
                    {
                        y1[i] = y[i] / ai;
                        d[i] = Math.Abs(y1[i] - y2[i]);
                        C += d[i]; y2[i] = y1[i];
                        if (d[i] > d[K]) K = i;
                    } //метка 10

                   /* for (int i = 1; i < n; i++) //(i2) поправка
                    {
                        i3 = n - i; // нужна ли поправка
                        i4 = i3 - 1;
                        z1[i3, 7] = z1[i4, 7];
                        z1[i3, 8] = z1[i4, 8];
                    }
                    //метка 73
                    z1[1, 7] = C;
                    z1[1, 8] = d[K];
                    */
                    P2C.Push(C);
                    P2D.Push(d[K]);
                }
                ai++;
                if (ai > nmax){repit = false;}
                if (Math.Abs(omax - omin) < eps)   { repit = false; } // Тут нужно и дти на метку 11 но ее нет
            }
                //печать i3,ai,ai2,ai3,ai4
                 double F = (omax + omin) / 2;
                //печать f, omax, omin
            Stack< double> X1= new Stack< double>();
            Stack< double> Y1= new Stack< double>();
            Stack< double> X2= new Stack< double>();
            Stack< double> Y2= new Stack< double>();
                for(int i = 0; i < n; i++)
                {
                    X1.Push(x1[i]);
                    Y1.Push(c4 + (i - 1) * dy);
                    X2.Push(y1[i]);
                    Y2.Push(c2 + (i - 1) * dx);

                  //  z1[i, 0] = x1[i];
                  //  z1[i, 1] = c4 + (i - 1) * dy;
                  //  z1[i, 2] = y1[i];
                  //  z1[i, 3] = c2 + (i - 1) * dx;
                }
            //вывод констант 
            textBox8.Multiline = true;
            textBox8.Text = "Вычисленные значения:" + Environment.NewLine;
            textBox8.Text += "i3 = " + i3 +  "; ai = " + ai + "; ai1 = " + ai1 + "; ai2 = " + ai2 + "; ai3 = " + ai3 + "; ai4 = " + ai4 + Environment.NewLine;
            textBox8.Text += "f = " + F + "; omax" + omax + "; omin = " + omin +  Environment.NewLine;
            //Вывод массива
            textBox8.Text += "Результаты:" + Environment.NewLine;

            textBox8.Text += "Игрок 1 (D): ";
            for (;P1D.Count!=0;)
            {
            textBox8.Text += P1D.Pop() + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 1 (C): ";
            for (;P1C.Count!=0;)
            {
                textBox8.Text += P1C.Pop() + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 2 (D): ";
            for (;P2D.Count!=0;)
            {
            textBox8.Text += P2D.Pop() + " ";
            }
            
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 2 (C): ";
            for (;P2C.Count!=0;)
            {
                textBox8.Text +=P2C.Pop().ToString("#.##") + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 1 (В.С/кол-во выб): ";
            for (;X1.Count!=0;)
            {
                textBox8.Text += X1.Pop() + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 1 (?): ";
            for (;Y1.Count!=0;)
            {
            textBox8.Text += Y1.Pop() + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 2 (В.С/кол-во выб): ";
            for (;X2.Count!=0;)
            {
                textBox8.Text += X2.Pop() + " ";
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 1 (?): ";
            for (;Y2.Count!=0;)
            {
            textBox8.Text += Y2.Pop() + " ";
            }


              /*  for (int j = 0; j < 9; j++) // поправка
                {
                    textBox8.Text += z1[i, j] + " ";
                }
                    textBox8.Text += Environment.NewLine;
            }*/
            //переменные для нахождения максимина и минимакса нужно искать по матрице A1!!
             double maxmin = 0, minmax = 0, local_min_j = 0, local_max_i = 0;
            //поиск наибольшего минимума
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0) local_min_j = a1[i, j];
                    if (local_min_j > a1[i, j]) local_min_j = a1[i, j];
                }
                if (i == 0) { maxmin = local_min_j; }
                if (maxmin < local_min_j) { maxmin = local_min_j; }
            }
            //поиск наименьшего максимума
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == 0) local_max_i = a1[i, j];
                    if (local_max_i < a1[i, j]) local_max_i = a1[i, j];
                }
                if (j == 0) { minmax = local_max_i; }
                if (minmax > local_max_i) { minmax = local_max_i; }
            }
            //вывод максимин и минимакс
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "MinMax = " + minmax + ";  MaxMin = " + maxmin;
            textBox8.Text += Environment.NewLine;
            forma = new Form2(a1);
            forma.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            textBox8.Text = "Для работы программы необходимо внести следующие данные:" + Environment.NewLine;
            textBox8.Text += "n - размерность матрицы [n,n], значение размерности не может превышать 9;" + Environment.NewLine;
            textBox8.Text += "nmax - максимальное количество игр;eps - погрешность расчета;" + Environment.NewLine;
            textBox8.Text += "c1, c2, c3, c4 - коэффициенты уравнения;" + Environment.NewLine;
            textBox8.Text += "Z(x,y) - функция расчета элементов;" + Environment.NewLine;
            /*textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;*/
        }
    }
}
