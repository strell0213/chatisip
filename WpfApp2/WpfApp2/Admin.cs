using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Admin
    {
        public int AdminID { get; set; }
        private string Nick, Pass;
        public string nick { get { return Nick; } set { value = Nick; } }
        public string pass { get { return Pass; } set { Pass = value; } }
        public Admin() { }

        public Admin(string Nick, string Pass) { 
            this.Nick = Nick;
            this.Pass = Pass;
        }
    }
}
