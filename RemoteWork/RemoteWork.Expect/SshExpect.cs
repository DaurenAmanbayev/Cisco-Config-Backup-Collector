using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                success = true;
            }
            catch (Exception ex)//заменить на проброс исключения
            {
                success = false;
                listError.Add(ex.Message);
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
                    foreach (string command in commands)
                    {
                        Execute(sshclient, command);
                    }
                    sshclient.Disconnect();
                }
                success = true;
            }
            catch (Exception ex)//заменить на проброс исключения
            {
                success = false;
                listError.Add(ex.Message);
            }

        }
        //выполнение команды с новым клиентом
        private void Execute(SshClient client, string command)
        {
            using (var cmd = client.CreateCommand(command))
            {
                cmd.Execute();
                listResult.Add(cmd.Result);
                if (cmd.Error.Length != 0)
                {
                    listError.Add(cmd.Error);
                }

            }
        }

    }
}
