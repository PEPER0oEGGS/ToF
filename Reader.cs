using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Theory_of_game
{
   public class Reader
    {
        Formula yrav = new Formula();

       // string imput = "x^(2*sin(y+3)-5)-sin(sqrt(2*y^2))"; как выучим синусы будем это считать
        string imput = "x^(2*(y+3)-54)-((2*y^2))";
        int reading_mark = 0;
        double[] read = new double[1000];
        //метод для изменения строки
        public void input_string(string input)
        {
            imput = input;
        }
        public Formula Reading()
        {
            int mark = 0;
            for (; reading_mark < imput.Length;)
            {
                if (imput[reading_mark] == '1' || imput[reading_mark] == '2' || imput[reading_mark] == '3' || imput[reading_mark] == '4' || imput[reading_mark] == '5' || imput[reading_mark] == '6' || imput[reading_mark] == '7' || imput[reading_mark] == '8' || imput[reading_mark] == '9' || imput[reading_mark] == '0') { read[mark] = readNunber(); reading_mark++; mark++; }
                else{ read[mark] = readSimbol(); reading_mark++; mark++;}
            }

            listCreated(mark);

          return (yrav);
        }

        double readNunber()
        {

            string number = "";

            for (; true;)
            {
                number = number + imput[reading_mark];
                if (reading_mark + 1 == imput.Length){ break; }
                if (imput[reading_mark + 1] == '1' || imput[reading_mark + 1] == '2' || imput[reading_mark + 1] == '3' || imput[reading_mark + 1] == '4' || imput[reading_mark + 1] == '5' || imput[reading_mark + 1] == '6' || imput[reading_mark + 1] == '7' || imput[reading_mark + 1] == '8' || imput[reading_mark + 1] == '9' || imput[reading_mark + 1] == '0'|| imput[reading_mark + 1] == ',') { reading_mark++; }
                else { break; }
            }

            return (double.Parse(number));

        }

        double readSimbol()
        {
            /* 
             * С минусом, только числа без минуса
             * 1-x
             * 2-y
             * 3 ошибка
             * 4-+
             * 5--
             * 6-*
             * 7-/
             * 8-^
             * 9-(
             * 10-)
             * 11 sin
             * 12 cos 
             * 13 abc
             * 14 sqrt
             * дополнить функции
             */

            if (imput[reading_mark] == 'p') {
                if (imput[reading_mark] == 'i')
                {return (Math.PI);}
            }
            if (imput[reading_mark] == 'e'){ return (Math.E); }
            if (imput[reading_mark] == 'x') { return (-1); }
            if (imput[reading_mark] == 'y') { return (-2); }
            if (imput[reading_mark] == '+') { return (-4); }
            if (imput[reading_mark] == '-') { return (-5); }
            if (imput[reading_mark] == '*') { return (-6); }
            if (imput[reading_mark] == '/') { return (-7); }
            if (imput[reading_mark] == '^') { return (-8); }
            if (imput[reading_mark] == '(') { return (-9); }
            if (imput[reading_mark] == ')') { return (-10);}

            if (imput[reading_mark] == 's') {reading_mark++;
            if (imput[reading_mark] == 'i') {reading_mark++; 
            if (imput[reading_mark] == 'n') {reading_mark++; 
            return (-11); }}
            if (imput[reading_mark] == 'q') { reading_mark++;
            if (imput[reading_mark] == 'r') { reading_mark++;
            if (imput[reading_mark] == 't') { reading_mark++;
            return (-14); }}}
            }
            if (imput[reading_mark] == 'c') {reading_mark++;
            if (imput[reading_mark] == 'o') {reading_mark++;
            if (imput[reading_mark] == 's') {reading_mark++;
            return (-12); }}}
            if (imput[reading_mark] == 'a') {reading_mark++;
            if (imput[reading_mark] == 'b') {reading_mark++;
            if (imput[reading_mark] == 's') {reading_mark++;
            return (-13); }}}
            
            // тут вернуть синусы косинусы итд
            return (3); // ошибка чтения
        }

        
        void listCreated(int markofmass)
        {
            /* 
             * С минусом, только числа без минуса
             * 1-x
             * 2-y
             * 3-число
             * 4-+
             * 5--
             * 6-*
             * 7-/
             * 8-^
             * 9-(
             * 10-)
             * 11 sin
             * 12 cos 
             * 13 abc
             * 14 sqrt
             * дополнить функции
             */

            int byffermark = 1;// текущая константа 
            yrav.startconstant();
            Stack<int> stack = new Stack<int>(); // стек последней констант
            stack.Push(byffermark);// в стеке всегда лежит ответ
            int listmark = 0; //метка готовых строк

            for (int mark = 0; mark < markofmass;)
            {
                if (read[mark] < 0 && (read[mark] != -1 && read[mark] != -2))// это знак 
                {

                    yrav.rezylt.Add(byffermark);
                    yrav.A.Add(byffermark);// символ А равен искомому числу (если не заполнен)
                    if (read[mark] == -10)// закрывающаяся скобка )
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        stack.Pop();
                        byffermark = stack.Peek();
                    }
                    else
                    {
                        if (read[mark] == -9 || read[mark] == -11 || read[mark] == -12 || read[mark] == -13 || read[mark] == -14)// открывабщаяся скобка в синусе корне итд *(
                        { stack.Push(byffermark); }
                        yrav.Simbol.Add((int)read[mark]);
                        byffermark++;
                        yrav.B.Add(byffermark);
                    }
                    mark++;
                    listmark++;
                }
                if (read[mark] >= 0 )// это число
                {
                    yrav.rezylt.Add(byffermark);
                    yrav.A.Add(-yrav.constants.Count);
                    yrav.constants.Add(read[mark]);
                    if (mark + 1 < markofmass) { mark++; } else {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        break;
                    }                       
                    if (read[mark] == -10)// закрывающаяся скобка )
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        stack.Pop();
                        byffermark=stack.Peek();
                        if (mark + 1 < markofmass) { mark++; }
                        else
                        {
                            yrav.Simbol.Add(-4);
                            yrav.B.Add(0);
                            break;
                        }
                    }
                    else
                    {
                        yrav.Simbol.Add((int)read[mark]);
                        byffermark++;
                        mark++;
                        yrav.B.Add(byffermark);
                    }
                    listmark++;
                }
                if (read[mark] == -1)// это x
                {
                    yrav.rezylt.Add(byffermark);
                    yrav.A.Add(-1);
                    if (mark + 1 < markofmass) { mark++; }
                    else
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        break;
                    }
                    if (read[mark] == -10)// закрывающаяся скобка )
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        stack.Pop();
                        byffermark = stack.Peek();
                        if (mark + 1 < markofmass) { mark++; }
                        else
                        {
                            yrav.Simbol.Add(-4);
                            yrav.B.Add(0);
                            break;
                        }
                    }
                    else
                    {
                        yrav.Simbol.Add((int)read[mark]);
                        byffermark++;
                        mark++;
                        yrav.B.Add(byffermark);
                    }
                    listmark++;

                } // копирнуть функции числа, но ссылки не на буфер а на резерв с х и y
                if (read[mark] == -2)// это y
                {
                    yrav.rezylt.Add(byffermark);
                    yrav.A.Add(-2);
                    if (mark + 1 < markofmass) { mark++; }
                    else
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        break;
                    }
                    if (read[mark] == -10)// закрывающаяся скобка )
                    {
                        yrav.Simbol.Add(-4);
                        yrav.B.Add(0);
                        stack.Pop();
                        byffermark = stack.Peek();
                        if (mark + 1 < markofmass) { mark++; }
                        else
                        {
                            yrav.Simbol.Add(-4);
                            yrav.B.Add(0);
                            break;
                        }
                    }
                    else
                    {
                        yrav.Simbol.Add((int)read[mark]);
                        byffermark++;
                        mark++;
                        yrav.B.Add(byffermark);
                    }
                    listmark++;
                }

                // если 1 знак заполнить знак.
                // символ А равен искомому числу (если не заполнен)
                // Б- новfz новая буфермарка
                // первая строчка заполена

                // если 1 число заполнить А
                // 2 знак
                // новая буфермарка
                // если там скобка то A+0
                // первая строчка заполена

            }
        }
    }
}
