using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    //класс Telnet другая имплементация
    //ожидает возврата требуемой строки с указнным таймаутом
    public class TelnetMint
    {
        TcpClient client;
        IPEndPoint remote;
        NetworkStream networkStream;
        int buffSize = 256;
        public string ENTER
        {
            get
            {
                return "\r\n";
            }
        }
        //конструктор с параметрами 
        public TelnetMint(string host, int port)
        {
            if (port < 1 || port > 65536)
            {
                //проверка валидности порта
                throw new Exception("Invalid port!");
            }
            IPAddress ip = System.Net.Dns.GetHostAddresses(host)[0];
            remote = new IPEndPoint(ip, port);
            client = new TcpClient();
            //подключаемся
            client.Connect(remote);
            networkStream = client.GetStream();

        }
        //конструктор по умолчанию
        public TelnetMint(string host)
            : this(host, 23)
        {
        }
        //закрыть подключение клиента
        public void Close()
        {
            client.Close();
        }
         ~TelnetMint()
        {
            client.Close(); 
        }
        /// <summary>
        /// отправка сообщения
        /// </summary>
        /// <param name="command"></param>
        public void SendData(string command)
        {
            if (!command.EndsWith(ENTER))
                command += ENTER;
            byte[] data = System.Text.Encoding.Default.GetBytes(command);
            networkStream.Write(data, 0, data.Length);
        }
       //вернуть информацию, если строка имеет совпадения по паттерну регулярных выражений за промежуток времени
        public string ReceiveDataWaitWord(Regex msg, int second)
        {
            StringBuilder result = new StringBuilder(buffSize);
            string temp = string.Empty;
            DateTime current = DateTime.Now.AddSeconds(second);
            do
            {
                temp = ReceiveData();
                result.Append(temp);
                if (msg.Match(temp).Success)
                {
                    return result.ToString();
                }

            } while (DateTime.Now < current);
            return result.ToString();
        }
        //вернуть информацию, если строка имеет совпадения по строке за промежуток времени
        public string ReceiveDataWaitWord(string msg, int second)
        {
            StringBuilder result = new StringBuilder(buffSize);
            string temp = string.Empty;
            DateTime current = DateTime.Now.AddSeconds(second);
            do
            {
                temp = ReceiveData();
                result.Append(temp);
                if (temp.TrimEnd().EndsWith(msg))
                {
                    return result.ToString();
                }

            } while (DateTime.Now < current);
            return result.ToString();
        }
        //вернуть полученные данные
        public string ReceiveData()
        {
            byte[] tempData = new byte[buffSize];
            List<byte> data = new List<byte>();
            int count = 0;
            do
            {
                if (!networkStream.DataAvailable)
                {
                    //если нет данных вернуть пустую строку
                    return "";
                }
                count = networkStream.Read(tempData, 0, tempData.Length);
                data.AddRange(tempData.Take(count));//добавляем данные 
            } while (count == buffSize);
            return Negotiate(data.ToArray()); //возвращаем
        }
        //обработать корректность данных???
        string Negotiate(byte[] data)
        {
            List<byte> sendData = new List<byte>();
            for (int i = 0; i < data.Length; i += 3)
            {
                if (data[i] == 255)
                {
                    byte[] remoteData = data.Skip(i).Take(3).ToArray();
                    for (int j = 0; j < remoteData.Length; j++)
                    {
                        if (remoteData[j] == 253)
                            remoteData[j] = 252;
                    }
                    sendData.AddRange(remoteData);
                }
                else
                {
                    break;
                }
            }
            byte[] sendByte = sendData.ToArray();
            networkStream.Write(sendByte, 0, sendByte.Length);
            if (sendByte.Length == data.Length)
            {
                //вернуть полученные данные
                return ReceiveData();
            }
            return System.Text.Encoding.Default.GetString(data.Skip(sendByte.Length).ToArray());

        }
    }
}
