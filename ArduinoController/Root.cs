using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoController
{
    class Root
    {
        static Arduino ard;
        public static void root(Arduino a)
        {
            ard = a;
            //put code here.
        }
    }
}
