using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Online
    {
        public int ID { get; set; }
        private string NickName;
        public string nickName { get { return NickName; } set { value = NickName; } }
        public Online() { }
        public Online(string NickName) { 
            this.NickName = NickName;
        }
    }
}
