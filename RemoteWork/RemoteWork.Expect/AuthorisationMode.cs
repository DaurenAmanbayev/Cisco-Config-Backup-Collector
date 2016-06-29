using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    enum AuthorisationMode
    {
        Anonymous,//когда не требуется пользователь
        AnonymousWithEnable,//анониманая с повышением привилегий
        Simple,//обычная авторизация
        SimpleWithEnable//обычная авторизация с повышением привилегий
    }
}
