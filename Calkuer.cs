﻿using System;
namespace Theory_of_game
{
   
    public class Calkuer
    {
        Formula formula = new Formula();
        double[] bufer = new double[1000];
        bool Atupe = true;
        bool Btupe = true;


        public double Calculation(Formula f)
        {
            formula = f;
            for (int mark = formula.rezylt.Count-1; mark >=0;)
            {
                if (formula.A[mark] <= 0) { Atupe = true; } else { Atupe = false; }
                if (formula.B[mark] <= 0) { Btupe = true; } else { Btupe = false; }

                bufer[formula.rezylt[mark]] = Operation(mark);

                mark--;
            }

            return (bufer[1]);
        }

        double Operation(int mark)
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
      * 10-) не используется
      * 11 sin
      * 12 cos
      * 13 abc
      * 14 sqrt
      * дополнить функции
      */
            switch (formula.Simbol[mark])
            {
                case -4:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + bufer[formula.B[mark]]); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + formula.constants[-formula.B[mark]]); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + formula.constants[-formula.B[mark]]); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + bufer[formula.B[mark]]); }
                    break;

                case -5:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] - bufer[formula.B[mark]]); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]]  - formula.constants[-formula.B[mark]]); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]]  - formula.constants[-formula.B[mark]]); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] - bufer[formula.B[mark]]); }
                    break;

                case -6:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] * bufer[formula.B[mark]]); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] * formula.constants[-formula.B[mark]]); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] * formula.constants[-formula.B[mark]]); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] * bufer[formula.B[mark]]); }
                    break;
                case -7:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] / bufer[formula.B[mark]]); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] / formula.constants[-formula.B[mark]]); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] / formula.constants[-formula.B[mark]]); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] / bufer[formula.B[mark]]); }
                    break;
                case -8:
                    if (!Atupe && !Btupe) { return (System.Math.Pow(bufer[formula.A[mark]], bufer[formula.B[mark]])); }
                    if (!Atupe && Btupe) { return (System.Math.Pow(bufer[formula.A[mark]],formula.constants[-formula.B[mark]])); }
                    if (Atupe && Btupe) { return (System.Math.Pow(formula.constants[-formula.A[mark]],formula.constants[-formula.B[mark]])); }
                    if (Atupe && !Btupe) { return (System.Math.Pow(formula.constants[-formula.A[mark]], bufer[formula.B[mark]])); }
                    break;
                case -9:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + bufer[formula.B[mark]]); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + formula.constants[-formula.B[mark]]); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + formula.constants[-formula.B[mark]]); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + bufer[formula.B[mark]]); }
                    break;
                case -11:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + Math.Sin(bufer[formula.B[mark]])); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + Math.Sin(formula.constants[-formula.B[mark]])); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + Math.Sin(formula.constants[-formula.B[mark]])); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + Math.Sin(bufer[formula.B[mark]])); }
                    break;
                case -12:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + Math.Cos(bufer[formula.B[mark]])); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + Math.Cos(formula.constants[-formula.B[mark]])); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + Math.Cos(formula.constants[-formula.B[mark]])); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + Math.Cos(bufer[formula.B[mark]])); }
                    break;
                case -14:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + Math.Sqrt(bufer[formula.B[mark]])); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + Math.Sqrt(formula.constants[-formula.B[mark]])); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + Math.Sqrt(formula.constants[-formula.B[mark]])); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + Math.Sqrt(bufer[formula.B[mark]])); }
                    break;
                case -13:
                    if (!Atupe && !Btupe) { return (bufer[formula.A[mark]] + Math.Abs(bufer[formula.B[mark]])); }
                    if (!Atupe && Btupe) { return (bufer[formula.A[mark]] + Math.Abs(formula.constants[-formula.B[mark]])); }
                    if (Atupe && Btupe) { return (formula.constants[-formula.A[mark]] + Math.Abs(formula.constants[-formula.B[mark]])); }
                    if (Atupe && !Btupe) { return (formula.constants[-formula.A[mark]] + Math.Abs(bufer[formula.B[mark]])); }
                    break;
            }
            return (0);


        }


    }
}
