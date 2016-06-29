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
        TelnetMint _client;
        string _rcvStr;
        int _timeOut=1;//1 секунда таймаут
        //при авторизации запрос имени пользователя или логина
        //username string should be Username: or Login: not case sensitive
        Regex _regexUsername = new Regex("Username:|Login:", RegexOptions.IgnoreCase);
        Regex _regexPassword = new Regex("Password:", RegexOptions.IgnoreCase);
        //проверка данных с помощью регулярных выражений
        //last input should be one of these variants
        //: or > or $ or #
        Regex regexUseMode = new Regex("[:,$,>,#]$");

        public TelnetMintExpect(ConnectionData host)
            : base(host)
        {
            _client = new TelnetMint(host.address, host.port);
            this._host = host;
        }
        //используем команду
        public override void ExecuteCommand(string command)
        {
            _success = true;
            try
            {
                if (_client._isConnected)
                {
                    //change Username and Password for regex pattern with login Login: and other modification---
                    //аутентификация                                          
                    //если требуется привилегированный режим
                    if (_host.enableMode)
                    {
                        //алгоритм работы
                        //password: отправляется пароль
                        //enable отправляется команда для перехода на привилегированный режим
                        //password: отправляет пароль привилегированного режима
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        _client.SendData(_host.password);
                        _client.SendData("enable");
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        _client.SendData(_host.enablePassword);
                    }
                    //если не требуется
                    else
                    {
                        //login:
                        //password: 
                        _rcvStr = _client.ReceiveDataWaitWord(_regexUsername, _timeOut);
                        _client.SendData(_host.username);
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        _client.SendData(_host.password);
                    }
                    //режим входа
                    _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut);
                    //использование команды
                    _client.SendData(command);
                    _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut);
                    _listResult.Add(_rcvStr);
                    _client.Close();
                }
                else
                {
                    _success = false;
                    _listError.Add("Network error: Connection timed out!");
                }
            }
            catch (Exception ex)
            {
                _success = false;
                _listError.Add(ex.Message);
            }      
        
        }
        //используем список команд
        /*
         * придумать решение для устройств, требующие для аутентификации лишь пароль
         * проверка возвращаемой строки на содержание %Authentication failed%
         */
        public override void ExecuteCommands(List<string> commands)
        {
            _success = true;
            try
            {
                /*
                 * проблема, что делать с входом, где не требуется логин, а только пароль
                 */
                if (_client._isConnected)
                {
                    //заменить на switch
                    //change Username and Password for regex pattern with login Login: and other modification---
                    //аутентификация
                    //если требуется привилегированный режим
                    if (_host.enableMode && !_host.anonymousLogin)
                    {
                        //алгоритм работы
                        //login: отправляется логин
                        _rcvStr = _client.ReceiveDataWaitWord(_regexUsername, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.username);
                        //password: отправляется пароль
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.password);
                        _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut);
                        //enable отправляется команда для перехода на привилегированный режим
                        //password: отправляем пароль привилегированного режима   
                        _client.SendData("enable");
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.enablePassword);
                    }
                    //если не требуется
                    else if (!_host.enableMode && !_host.anonymousLogin)
                    {
                        //login:
                        //password:                   
                        _rcvStr = _client.ReceiveDataWaitWord(_regexUsername, _timeOut);
                       // CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.username);
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                       // CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.password);
                    }
                    //АНОНИМНЫЙ ВХОД НА УСТРОЙСТВО, когда не требуется ввод пользователя
                    else if(_host.enableMode && _host.anonymousLogin)
                    {
                        //password: отправляется пароль
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.password);
                        _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut);
                        //enable отправляется команда для перехода на привилегированный режим
                        //password: отправляем пароль привилегированного режима   
                        _client.SendData("enable");
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.enablePassword);
                    }
                    //АНОНИМНЫЙ ВХОД НА УСТРОЙСТВО, когда не требуется ввод пользователя
                    else if (!_host.enableMode && _host.anonymousLogin)
                    {
                        //password: отправляется пароль
                        _rcvStr = _client.ReceiveDataWaitWord(_regexPassword, _timeOut);
                        //CheckIsReceiveWordMatched(_rcvStr);//CHECKOUT WITH EXCEPTIONS
                        _client.SendData(_host.password);
                    }
                    //режим входа
                    _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut);
                    //использование команды
                    foreach (string command in commands)
                    {
                        _client.SendData(command);
                        _rcvStr = _client.ReceiveDataWaitWord(regexUseMode, _timeOut + 2);//увеличил таймаут
                        //попробовать увеличить таймаут, если строка не найдена
                        _listResult.Add(_rcvStr);
                    }
                    _client.Close();
                }
                else
                {
                    _success = false;
                    _listError.Add("Network error: Connection timed out!");
                }
            }
            catch (Exception ex)
            {
                _success = false;
                _listError.Add(ex.Message);
            }
        }
        //проверить как отразиться на производительности опроса
        private void CheckIsReceiveWordMatched(string msg)
        {
            //если строка подключения не соответствует ожидаемому, выбросить исключение с несоответствующей строкой
            if(!_client._isReceiveWordHasMatched)
                throw new Exception(msg);
        }
    }
}
