﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    public class TelnetExpect : Expect
    {
        Telnet client;
        //изменить таймаут???
        int timeOut = 120;
        public TelnetExpect(ConnectionData host)
            : base(host)
        {
            client = new Telnet(host.address, host.port);
            this.host = host;
        }
        /*
         * CLEAR ALL CONSOLE COMMANDS!!!
         */
        public override void ExecuteCommand(string command)
        {
            try
            {
                string loginStatus = client.Login(host.username, host.password, timeOut);
                string prompt = loginStatus.TrimEnd();
                prompt = loginStatus.Substring(prompt.Length - 1, 1);
                //!!!! clear all console commands
                //результат возвращаемый соединением
                //Console.WriteLine(prompt);
                if (prompt != "$" && prompt != ">" && prompt != ":" && prompt != "#")
                    throw new Exception("Connection failed!");

                client.WriteLine(command);
                prompt = client.Read();
                listResult.Add(prompt);
                client.Disconnect();
                success = true;
            }
            catch (Exception ex)//заменить проброс исключения, или использовать список проблем
            {
                success = false;
                listError.Add(ex.Message);
            }
        }
        public override void ExecuteCommands(List<string> commands)
        {
            try
            {
                string loginStatus = client.Login(host.username, host.password, timeOut);
                string prompt = loginStatus.TrimEnd();
                prompt = loginStatus.Substring(prompt.Length - 1, 1);
                //результат возвращаемый соединением
               // Console.WriteLine(prompt);
                if (prompt != "$" && prompt != ">" && prompt != ":" && prompt != "#")
                    throw new Exception("Connection failed!");

                //prompt = client.Read();
               // Console.WriteLine(prompt);
                //if (prompt != "$" && prompt != ">" && prompt != ":")
                //    throw new Exception("Connection failed!");

                foreach (string command in commands)
                {
                    client.WriteLine(command);
                    prompt = client.Read();
                    listResult.Add(prompt);
                }
                client.Disconnect();
                success = true;
            }
            catch (Exception ex)//заменить проброс исключения
            {
                success = false;
                listError.Add(ex.Message);
            }
        }
    }
}
