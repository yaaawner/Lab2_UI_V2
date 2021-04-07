using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    struct Grid1D
    {
        public float Step { get; set; }
        public int Num { get; set; }

        public Grid1D(float step, int num)
        {
            Step = step;
            Num = num;
        }

        public override string ToString()
        {
            return "Step: " + Step.ToString() + "; Num: " + Num.ToString();
        }

        public string ToString(string format)
        {
            return "Step: " + Step.ToString(format) + "; Num: " + Num.ToString(format);
        }
    }
}
