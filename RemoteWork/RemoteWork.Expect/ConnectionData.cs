using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //структура с данными для подключения
    public struct ConnectionData
    {
        public string address;
        public string username;
        public string password;
        public int port;
        //данные для привилегированного режима
        public bool enableMode;
        //анонимный вход в устройство
        public bool anonymousLogin;
        public string enablePassword;
    }
}
