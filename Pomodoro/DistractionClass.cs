using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pomodoro
{
    [Serializable]
    public class DistractionClass
    {
        public string text { get; set; }

        public DistractionClass(string text)
        {
            this.text = text;
        }
    }
}
