using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemoteWork
{
    //класс для извлечения данных по указанному паттерну
    class RegexExtract
    {
        private RegexExtract() { }
        //реализация паттерна Синглтон
        private static RegexExtract _singletone;
        public static RegexExtract Singletone
        {
            get
            {
                if (_singletone == null)
                    _singletone = new RegexExtract();
                return _singletone;
            }
        }
        //извлечь строки из текста
        private List<string> ExtractLine(string line)
        {
            List<string> Lines = new List<string>();
            string pattern = "\r\n";
            int prev = 0, next = 0;
            int last = line.LastIndexOf(pattern) + 2;//step of pattern
            int size = line.Length;
            bool stop = true;
            while (stop)
            {
                next = line.IndexOf(pattern, prev);//находим требуемый индекс
                if (next > prev)//если следующий индекс больше, чем предыдущий 
                {
                    Lines.Add(line.Substring(prev, next - prev));//добавить отрезок строки между индексами
                    prev = next + 2;
                }
                else//если следующий индекс меньше, чем предыдущий 
                {
                    if (last < size)//проверяем, что индекс последнего совпадения меньше чем размер текста
                    {
                        Lines.Add(line.Substring(last, size - last));//добавляем конец строки в наш перечень
                    }
                    break;
                }
            }
            return Lines;
        }
       
        //заменить совпадающий паттерн
        public string ReplacePattern(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }
        //извлечь список адресов из текста
        public List<IPAddress> ExtractIpAddress(string input)
        {
            string pattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";//clear ip retrieve

            Regex regexIp = new Regex(pattern);
            MatchCollection result = regexIp.Matches(input);
            List<IPAddress> list = new List<IPAddress>();
            IPAddress host;
            foreach (Match match in result)
            {
                string strAddress = match.ToString();
                if (IPAddress.TryParse(strAddress, out host))
                {
                    list.Add(host);
                }
            }
            return list;
        }
        //извлечь адрес из текста
        public string ExtractIP(string input)
        {
            string pattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";//clear ip retrieve

            Regex regex = new Regex(pattern);
            Match match = regex.Match(input.Trim());
            return match.ToString();
        }
        //список совпадений по паттерну
        public ArrayList Separator(string input, string pattern)
        {
            Regex reg = new Regex(pattern);
            MatchCollection result = reg.Matches(input);
            ArrayList list = new ArrayList();

            foreach (Match match in result)
            {
                list.Add(match);
            }
            return list;

        }

        #region NETWORK INFO VALIDATE
        //проверка ключа snmp
        public string ParseOidKey(string input)
        {
            string pattern = @"(\d+\.){1,}\d";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input.Trim());
            return match.ToString();
        }
        //проверка snmp community
        public string ParseCommunity(string input)
        {
            string pattern = @"(\S){1,}";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input.Trim());
            return match.ToString();
        }
        //проверка email
        public bool MailValidate(string input)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(input.Trim());
            return match.Success;
        }
        #endregion

    }
}
