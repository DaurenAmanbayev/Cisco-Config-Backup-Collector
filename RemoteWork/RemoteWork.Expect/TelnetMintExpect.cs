using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //автоматизация сбора по протоколу телнет
    //итоговая версия
    public class TelnetMintExpect: Expect
    {
        TelnetMint client;
        string rcvStr;
        int timeOut=1;//1 секунда таймаут
        //при авторизации запрос имени пользователя или логина
        //username string should be Username: or Login: not case sensitive
        Regex regexUsername = new Regex("Username:|Login:", RegexOptions.IgnoreCase);
        Regex regexPassword = new Regex("Password:", RegexOptions.IgnoreCase);
        //проверка данных с помощью регулярных выражений
        //last input should be one of these variants
        //: or > or $ or #
        Regex regexUseMode = new Regex("[:,$,>,#]$");

        public TelnetMintExpect(ConnectionData host)
            : base(host)
        {
            client = new TelnetMint(host.address, host.port);
            this.host = host;
        }
        //используем команду
        public override void ExecuteCommand(string command)
        {
            success = true;
            try
            {
                //change Username and Password for regex pattern with login Login: and other modification---
                //аутентификация
                rcvStr = client.ReceiveDataWaitWord(regexUsername, timeOut);
                client.SendData(host.username);
                rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                client.SendData(host.password);               
                //если требуется привилегированный режим
                if (host.enableMode)
                {
                    rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                    client.SendData(host.enablePassword);
                }
                //режим входа
                 rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                //использование команды
                client.SendData(command);
                rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                listResult.Add(rcvStr);
                client.Close();
            }
            catch (Exception ex)
            {
                success = false;
                listError.Add(ex.Message);
            }      
        
        }
        //используем список команд
        public override void ExecuteCommands(List<string> commands)
        {
            success = true;
            try
            {
                //change Username and Password for regex pattern with login Login: and other modification---
                //аутентификация
                rcvStr = client.ReceiveDataWaitWord(regexUsername, timeOut);
                client.SendData(host.username);
                rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                client.SendData(host.password);
                //если требуется привилегированный режим
                if (host.enableMode)
                {
                    rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                    client.SendData(host.enablePassword);
                }
                //режим входа
                rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                //использование команды
                foreach (string command in commands)
                {
                    client.SendData(command);
                    rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                    listResult.Add(rcvStr);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                success = false;
                listError.Add(ex.Message);
            }
        }
    }
}
