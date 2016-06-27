using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //класс для автоматизации сбора конфигурацию по протоколу SSH
    public class SshExpect : Expect
    {
        ConnectionInfo connInfo;//строка подключения
        public SshExpect(ConnectionData host)
            : base(host)
        {
            connInfo = new ConnectionInfo(host.address, host.port, host.username, new AuthenticationMethod[]{
                new PasswordAuthenticationMethod(host.username, host.password)
                }
            );
        }
        //выполнение команды
        public override void ExecuteCommand(string command)
        {
            try
            {
                using (var sshclient = new SshClient(connInfo))
                {
                    sshclient.Connect();                  
                    Execute(sshclient, command);
                    sshclient.Disconnect();
                }
                _success = true;
            }
            catch (Exception ex)//заменить на проброс исключения
            {
                _success = false;
                _listError.Add(ex.Message);
            }
        }
        //выполнение списка команд
        public override void ExecuteCommands(List<string> commands)
        {
            try
            {
                using (var sshclient = new SshClient(connInfo))
                {
                    sshclient.Connect();
                    //если требуется привилегированный режим
                    if (_host.enableMode)
                    {
                        ExecuteEnableModeCommands(sshclient, commands);
                    }
                    //если не требуется привилегированный режим
                    else
                    {
                        foreach (string command in commands)
                        {
                            Execute(sshclient, command);
                        }
                    }
                    sshclient.Disconnect();
                }
                _success = true;
            }
            catch (Exception ex)//заменить на проброс исключения
            {
                _success = false;
                _listError.Add(ex.Message);
            }

        }
        //выполнение команд с новым клиентом
        private void Execute(SshClient client, string command)
        {
            using (var cmd = client.CreateCommand(command))
            {
                cmd.Execute();
                _listResult.Add(cmd.Result);
                if (cmd.Error.Length != 0)
                {
                    _listError.Add(cmd.Error);
                }

            }
        }
        //выполнение команд в привилегированном режиме
        private void ExecuteEnableModeCommands(SshClient sshClient, List<string> commands)
        {
            using (ShellStream client = sshClient.CreateShellStream("terminal", 80, 24, 800, 600, 1024))
            {
                SendCommandLF("enable", client);//переходим в привилегированный режим
                SendCommand(_host.enablePassword, client);//подтверждаем наши права
                foreach (string command in commands)
                {
                    _listResult.Add(SendCommand(command, client));
                }
                //Console.WriteLine("4 [" + SendCommand("terminal pager 0", client) + "]");
                //Console.WriteLine("3 [" + SendCommand("show version", client) + "]");
                //Console.WriteLine("5 [" + SendCommand("show run", client) + "]");
                //Console.WriteLine("1 [" + SendCommandW("show interface ?", client) + "]");
            }
        }
        //команды выполняемые в потоке
        #region SHELL STREAM COMMANDS
        /*
         * CR -"\r"
         * LF -"\n"
         * CRLF -"\r\n"
         * отличие в методе отправки сообщений
         */
        static string SendCommandLF(string command, ShellStream shell)
        {
            StreamReader reader;
            StreamWriter writer;
            reader = new StreamReader(shell);
            writer = new StreamWriter(shell);
            writer.AutoFlush = true;
            writer.Write(command + "\n");
            while (shell.Length == 0)
                Thread.Sleep(500);
            return reader.ReadToEnd();
        }
        static string SendCommand(string command, ShellStream shell)
        {
            StreamReader reader;
            StreamWriter writer;

            reader = new StreamReader(shell);
            writer = new StreamWriter(shell);
            writer.AutoFlush = true;
            writer.WriteLine(command);
            while (shell.Length == 0)
                Thread.Sleep(500);
            return reader.ReadToEnd();
        }
        static string SendCommandCR(string command, ShellStream shell)
        {
            StreamReader reader;
            StreamWriter writer;
            reader = new StreamReader(shell);
            writer = new StreamWriter(shell);
            writer.AutoFlush = true;
            writer.Write(command + "\r");
            while (shell.Length == 0)
                Thread.Sleep(500);
            return reader.ReadToEnd();
        }
        static string SendCommandW(string command, ShellStream shell)
        {
            StreamReader reader;
            StreamWriter writer;
            reader = new StreamReader(shell);
            writer = new StreamWriter(shell);
            writer.AutoFlush = true;
            writer.Write(command);

            while (shell.Length == 0)
                Thread.Sleep(500);
            return reader.ReadToEnd();
        }
        #endregion

    }
}
