using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theory_of_game
{
   public class Formula
    {
        public List<double> constants = new List<double>();// резерв 0 - x; 1-y остальные константы.
        // rezult = А(simbol) B
        public List<int> rezylt = new List<int>();// содердит номер константы
        public List<int> A = new List<int>();// содердит номер константы A
        public List<int> Simbol = new List<int>();// содердит знак
        public List<int> B = new List<int>();// содердит номер константы B

        public void startconstant()
        {
            constants.Add(0);
            constants.Add(0);
            constants.Add(0);
        }
    }
}
