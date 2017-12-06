using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Theory_of_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Formula formula = new Formula();
        double[,] a1; int n;

        public double funk(double wx, double wy)
        {
            Calkuer calkuer = new Calkuer();
            formula.constants[1] = wx;
            formula.constants[2] = wy;
            return (calkuer.Calculation(formula));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            n = 0; int nmax = 0, c1 = 0, c2 = 0, c3 = 0, c4 = 0;
            double eps = 0;
            string function = textBox9.Text;
            Reader reader = new Reader();
            reader.input_string(function);
            formula = reader.Reading();
           //Получение значений n,nps,eps,c1,c2,c3,c4 из текстбоксов
            n = int.Parse(textBox1.Text);
            nmax = int.Parse(textBox2.Text);
            eps = double.Parse(textBox3.Text);
            if (true)//extBox10.Text == null)//при загрузке матрицы нам это не нужно
            {
                c1 = int.Parse(textBox4.Text);
                c2 = int.Parse(textBox5.Text);
                c3 = int.Parse(textBox6.Text);
                c4 = int.Parse(textBox7.Text);
            }
            if (textBox10.Text != "") { f_load_matrix(n); }
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
            if (textBox10.Text == "")
            {
                a1 = new double[n, n];
            }
            int ai = 1, ai1 = 0, ai2 = 0, ai3 = 0, ai4 = 0;
            double dx = (c2 - c1) / (n - 1), dy = (c4 - c3) / (n - 1);
            int i3=-1, i4;
            if (textBox10.Text == "")
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        a1[i, j] = funk(c1 + (c2 - c1) * (i - 1), c3 + (c4 - c3) * (j - 1));
                    } //функция выбирается через checkbox
                }
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

                    for (int i = 1; i < n; i++) //(i2) поправка
                    {
                        i3 = n - i; // нужна ли поправка
                        i4 = i3 - 1;
                        z1[i3, 7] = z1[i4, 7];
                        z1[i3, 8] = z1[i4, 8];
                    }
                    //метка 73
                    z1[1, 7] = C;
                    z1[1, 8] = d[K];
                }
                ai++;
                if (ai > nmax){repit = false;}
                if (Math.Abs(omax - omin) < eps)   { repit = false; } // Тут нужно и дти на метку 11 но ее нет
            }
                //печать i3,ai,ai2,ai3,ai4
                double F = (omax + omin) / 2;
                //печать f, omax, omin
                for(int i = 0; i < n; i++)
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
            for (int i = 0; i < n; i++) // поправка 
            {
                for (int j = 0; j < 9; j++) // поправка
                {
                    textBox8.Text += z1[i, j] + " ";
                }
                    textBox8.Text += Environment.NewLine;
            }
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
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            textBox8.Text = "Для работы программы необходимо внести следующие данные:" + Environment.NewLine;
            textBox8.Text += "n - размерность матрицы [n,n], значение размерности не может превышать 9;" + Environment.NewLine;
            textBox8.Text += "nmax - максимальное количество игр;eps - погрешность расчета;" + Environment.NewLine;
            textBox8.Text += "c1, c2, c3, c4 - коэффициенты уравнения;" + Environment.NewLine;
            textBox8.Text += "Z(x,y) - функция расчета элементов;" + Environment.NewLine;
            textBox8.Text += "В программе существует возможность загрузить матрицу из внешного файла." + Environment.NewLine;
            textBox8.Text += "Для загрузки необходимо переместить файл с матрицей в директорию с исполняемым файлом и ввести название файла в соответствующую графу." + Environment.NewLine;
            /*textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;
            textBox8.Text += "" + Environment.NewLine;*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*load_matrix = true;
            //Получение имени файла
            string file_name = textBox10.Text;
            file_name += ".txt";
            //Получение адреса файла для подключения
            string adress = get_adress(file_name);
            string matrix_string = "";
            //чтение файла
            System.IO.StreamReader file = new System.IO.StreamReader(@adress);
            string line;            
            for (int i = 0; (line = file.ReadLine()) != null; i++)
            {            
                matrix_string += line;                
            }
            string n_s = textBox1.Text;
            n = Convert.ToInt32(n_s);
            int inc = 0;
            a1 = new double[n, n];
            String[] arr_srt_number = matrix_string.Split(' ');
            foreach (string element in arr_srt_number)
            {
                a1[(inc/n),(inc%n)] = Convert.ToInt32(element); inc++;
            }
            /*
            char[] mat_char = matrix_string.ToCharArray(0, matrix_string.Length);
            for (int i = 0; i < n; i++)
            {
                //переменная для прохода символьного массива
                int k = 0;
                for (int j = 0; j < n; j++)
                {
                    if ((mat_char[i * n + k] == ' ')) { k++; }
                    if ((mat_char[i * n + k] == '\n') || (mat_char[i * n + k] == '\r')) { k += 2; }
                    if ((mat_char[i * n + k] != '\n') && (mat_char[i * n + k] != '\r') && (mat_char[i * n + k] != ' '))
                    {
                        if (mat_char[i * n + k] == '-') { a1[i, j] = (-1)*(int)Char.GetNumericValue(mat_char[i * n + k+1]); }
                        else { a1[i, j] = (int)Char.GetNumericValue(mat_char[i * n + k]); }
                    }
                    //if ((map_char[i * y_range + k] == '\n') || (map_char[i * y_range + k] == '\r')) { k--; }
                    k++;
                }
                
                //while()
                //button3_Click(object sender, EventArgs e);
                
            }
            
            textBox10.Text = "Матрица загружена";*/
        }

        private string get_adress(string file_name)
        {
            string adress = Path.GetFullPath(file_name);
            return adress;
        }

        private void f_load_matrix(int n)
        {            
            //Получение имени файла
            string file_name = textBox10.Text;
            file_name += ".txt";
            //Получение адреса файла для подключения
            string adress = get_adress(file_name);
            string matrix_string = "";
            //чтение файла
            System.IO.StreamReader file = new System.IO.StreamReader(@adress);
            string line;
            for (int i = 0; (line = file.ReadLine()) != null; i++)
            {
                matrix_string += line + " ";
            }
            //string n_s = textBox1.Text;
            //n = Convert.ToInt32(n_s);
            int inc = 0;
            a1 = new double[n, n];
            String[] arr_srt_number = matrix_string.Split(' ');
            foreach (string element in arr_srt_number)
            {
                try
                {
                    a1[(inc / n), (inc % n)] = Convert.ToInt32(element); inc++;
                }
                catch { }
            }
            textBox10.Text = "Матрица загружена";
        }
    }
}
