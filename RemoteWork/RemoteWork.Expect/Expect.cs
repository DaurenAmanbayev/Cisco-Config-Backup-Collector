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
        protected ConnectionData host;        
        protected bool success = false;//статус подключения, работы
        protected List<string> listError = new List<string>();//список ошибок
        protected List<string> listResult = new List<string>();//список строк результатов
        public Expect(ConnectionData host)
        {
            this.host = host;
        }
        //использование одной команды
        public abstract void ExecuteCommand(string command);
        //использование нескольких команд
        public abstract void ExecuteCommands(List<string> commands);
        //вернуть результат
        public string GetResult()
        {
            return string.Join(Environment.NewLine,listResult.ToArray());
        }
        //вернуть ошибку
        public string GetError()
        {
            return string.Join(Environment.NewLine, listError.ToArray());
        }
        //удачное подключение или нет?
        public bool isSuccess
        {
            get { return success; }
        }
    }
}
