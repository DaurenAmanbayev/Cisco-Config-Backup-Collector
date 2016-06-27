using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //абстрактный класс для автоматизации процесса подключения и сбора конфигурации
    public abstract class Expect
    {
        protected ConnectionData _host;        
        protected bool _success = false;//статус подключения, работы
        protected List<string> _listError = new List<string>();//список ошибок
        protected List<string> _listResult = new List<string>();//список строк результатов
        public Expect(ConnectionData host)
        {
            this._host = host;
        }
        //использование одной команды
        public abstract void ExecuteCommand(string command);
        //использование нескольких команд
        public abstract void ExecuteCommands(List<string> commands);
        //вернуть результат
        public string GetResult()
        {
            return string.Join(Environment.NewLine,_listResult.ToArray());
        }
        //вернуть ошибку
        public string GetError()
        {
            return string.Join(Environment.NewLine, _listError.ToArray());
        }
        //удачное подключение или нет?
        public bool isSuccess
        {
            get { return _success; }
        }
    }
}
