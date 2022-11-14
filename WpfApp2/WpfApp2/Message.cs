using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Message
    {
        public int ID { get; set; }
        private string NickName, Mes;
        public string nickName { get { return NickName; } set { value = NickName; } }
        public string mes { get { return Mes; } set { Mes = value; } }
        public Message() { }
        public Message(string NickName, string Mes)
        {
            this.NickName = NickName;
            this.Mes = Mes;
        }
    }
}
