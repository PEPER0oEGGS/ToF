using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
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
            n = 0; int nmax = 0;
            double eps = 0;
            string function = textBox9.Text;
            Reader reader = new Reader();
            reader.input_string(function);
            formula = reader.Reading();
           //Получение значений n,nps,eps
            n = int.Parse(textBox1.Text);
            nmax = int.Parse(textBox2.Text);
            eps = double.Parse(textBox3.Text);
            if (textBox10.Text != "") { f_load_matrix(n); }
            //Объявление основных используемых переменных
            double[] x = new double[n];
            double[] y = new double[n];
            double[] pc = new double[n];
            double[] fi = new double[n];
           
            double omax1;
            double omin1;

            Form forma;


            if (textBox10.Text == "")
            {
                a1 = new double[n, n];
            }
            int ai = 1;
            if (textBox10.Text == "")
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        a1[i, j] = funk(4, 3);
                    } //функция выбирается через checkbox
                }
            }
           
            // метка 57 (Собираем массив А)
            double omax = 1000000; //~ max V(n))/n 
            double omin = -1000000; //~(min U(n))/n 
            int J=(n + 1)/2-1; // случайно выбраный столбец (+ его инициализация)
            int I=0; // инициализация константы со строкой.
             //baka! Не юзай метки
            for (bool repit = true; repit;)
            {
                I = 0; // Выбрали первый элемент столбца 

                for (int i = 0; i < n; i++) // перебираем столбец и ищем минимальный
                { pc[i] = pc[i] + a1[J,i] ; // Вектор = вектор + элементы J строки 
                if (pc[i] <= pc[I]){ I = i;}// если I (элемент столбца) не минимальный, то I=i   pc - вектор первого игрока
                }
                x[I] = x[I] + 1; // x имеет размерность количества строк и показывает сколько раз была выбранна I-я строка 
                J = 0; //запомнили первый столбец

                for (int j = 0; j < n; j++) //перебираем строку и ищем минимальное
                {fi[j] = fi[j] + a1[j, I]; // Вектор = вектор + элементы I стотбца 
                 if (fi[j] >= fi[J]){J = j;} // если J (элемент строки) не мax, то J=j   Fi - вектор второго игрока
                } 
                y[J] = y[J] + 1; // x имеет размерность количества столбцов и показывает сколько раз был выбранн J-й столбец 

                omax1 = pc[I] / ai; //Уточнили условия в начале min U(n))/n  
                omin1 = fi[J] / ai; //Уточнили условия в начале max V(n))/n

                if (Math.Abs(omax - omax1) >= 0) { omax = omax1;   }// уточнили начальное условие
                if (Math.Abs(omin - omin1) >= 0) {omin = omin1; }// уточнили начальное условие
                ai++;

                if (ai > nmax){repit = false; ai--; }
                if (Math.Abs(omax - omin) < eps){ repit = false; }
            }
                
            
            //печать i3,ai,ai2,ai3,ai4
            double F = (omax + omin) / 2;//печать f, omax, omin
            //вывод констант 
            textBox8.Multiline = true;
            textBox8.Text = "Вычисленные значения:" + Environment.NewLine;
            textBox8.Text += "Тактов вычисления: " + ai + "/"+ nmax + Environment.NewLine;
            textBox8.Text += "Среднее = " + F + "; omax = " + omax + "; omin = " + omin +  Environment.NewLine;// ДОЛЖНЫ БЫТЬ ДРОБНЫЕ
            //Вывод массива
            textBox8.Text += "Результаты:" + Environment.NewLine;
            
            Queue< double> X1= new Queue< double>();
            Queue< double> X2= new Queue< double>();
                for(int i = 0; i < n; i++)// ДОЛЖНЫ БЫТЬ ДРОБНЫЕ
            {
                    X1.Enqueue(x[i]/ai);  // пока приравниваю к количеству выборов, должна быть вероятность (x1[i])
                    X2.Enqueue(y[i]/ai);// пока приравниваю к количеству выборов, должна быть вероятность (y1[i])
            }// ДОЛЖНЫ БЫТЬ ДРОБНЫЕ

            textBox8.Text += "Игрок 1 (Выборы столбцов): ";
            for (;X1.Count!=0;)
            {
                textBox8.Text += Math.Round(X1.Dequeue(), 3) + " "; // ДОЛЖНЫ БЫТЬ ДРОБНЫЕ
            }
            textBox8.Text += Environment.NewLine;
            textBox8.Text += "Игрок 2 (Выборы строк): ";
            for (; X2.Count != 0;)
            {
                textBox8.Text += Math.Round(X2.Dequeue(), 3) + " "; // ДОЛЖНЫ БЫТЬ ДРОБНЫЕ
            }
            textBox8.Text += Environment.NewLine;

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

            forma = new Matrix(a1);
            forma.ShowDialog();
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
