using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //проблема с сетевыми устройствами, не успевает возвращать строку ответа
    //с сервером телнет проблем нет
    public class Telnet
    {
        TcpClient tcpSocket;//клиент
        int TimeOutMs = 150;//таймаут
        public Telnet(string Hostname, int Port)
        {
            tcpSocket = new TcpClient(Hostname, Port);

        }
        //подключение к серверу
        public string Login(string Username, string Password, int LoginTimeOutMs)
        {
            int oldTimeOutMs = TimeOutMs;
            TimeOutMs = LoginTimeOutMs;
            string s = Read();
            if (!s.TrimEnd().EndsWith(":"))
                throw new Exception("Failed to connect : no login prompt");
            WriteLine(Username);

            s += Read();
            if (!s.TrimEnd().EndsWith(":"))
                throw new Exception("Failed to connect : no password prompt");
            WriteLine(Password);

            s += Read();
            TimeOutMs = oldTimeOutMs;
            return s;
        }
        //отправить строку
        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
        }
        //отправить значение
        public void Write(string cmd)
        {
            if (!tcpSocket.Connected) return;//если подключение не доступно
            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }
        //прочесть доступные данные
        public string Read()
        {           
            if (!tcpSocket.Connected) return null;//если нет подключение, возвращаем пустую строку
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);//читаем строку
                System.Threading.Thread.Sleep(TimeOutMs);//ждем таймаут
            } while (tcpSocket.Available > 0);//пока подключение доступно
            return sb.ToString();//возвраащем строку
        }
        //проверить состояние клиента
        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }
        //отключить клиент
        public void Disconnect()
        {
            tcpSocket.Close();
        }
        //обработать полученные данные 
        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)//если подключение доступно
            {
                int input = tcpSocket.GetStream().ReadByte();//читаем поток
                switch (input)//проверка
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC:
                        // интерпретация команды
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //литерал IAC = 255 добавляем в строку
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // отвечаем на все команды с "WONT", пока нам приходит SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA)
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append((char)input);//добавлем символ в строку
                        break;
                }
            }
        }
    }
}
