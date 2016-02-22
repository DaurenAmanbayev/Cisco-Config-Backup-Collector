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
                if (client.isConnected)
                {
                    //change Username and Password for regex pattern with login Login: and other modification---
                    //аутентификация                                          
                    //если требуется привилегированный режим
                    if (host.enableMode)
                    {
                        //алгоритм работы
                        //password: отправляется пароль
                        //enable отправляется команда для перехода на привилегированный режим
                        //password: отправляет пароль привилегированного режима
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.password);
                        client.SendData("enable");
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.enablePassword);
                    }
                    //если не требуется
                    else
                    {
                        //login:
                        //password: 
                        rcvStr = client.ReceiveDataWaitWord(regexUsername, timeOut);
                        client.SendData(host.username);
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.password);
                    }
                    //режим входа
                    rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                    //использование команды
                    client.SendData(command);
                    rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                    listResult.Add(rcvStr);
                    client.Close();
                }
                else
                {
                    success = false;
                    listError.Add("Network error: Connection timed out!");
                }
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
                if (client.isConnected)
                {
                    //change Username and Password for regex pattern with login Login: and other modification---
                    //аутентификация
                    //если требуется привилегированный режим
                    if (host.enableMode)
                    {
                        //алгоритм работы
                        //password: отправляется пароль
                        //enable отправляется команда для перехода на привилегированный режим
                        //password: отправляем пароль привилегированного режима                   
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.password);
                        rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                        client.SendData("enable");
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.enablePassword);
                    }
                    //если не требуется
                    else if (!host.enableMode)
                    {
                        //login:
                        //password:                   
                        rcvStr = client.ReceiveDataWaitWord(regexUsername, timeOut);
                        client.SendData(host.username);
                        rcvStr = client.ReceiveDataWaitWord(regexPassword, timeOut);
                        client.SendData(host.password);
                    }
                    //режим входа
                    rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut);
                    //использование команды
                    foreach (string command in commands)
                    {
                        client.SendData(command);
                        rcvStr = client.ReceiveDataWaitWord(regexUseMode, timeOut + 2);//увеличил таймаут
                        listResult.Add(rcvStr);
                    }
                    client.Close();
                }
                else
                {
                    success = false;
                    listError.Add("Network error: Connection timed out!");
                }
            }
            catch (Exception ex)
            {
                success = false;
                listError.Add(ex.Message);
            }
        }
    }
}
