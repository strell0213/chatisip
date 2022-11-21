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
        public int IsBanned { get; set; }
        public string nickName { get { return NickName; } set { value = NickName; } }
        public Online() { }
        public Online(string NickName, int IsBanned) { 
            this.NickName = NickName;
            this.IsBanned = IsBanned;
        }
    }
}
