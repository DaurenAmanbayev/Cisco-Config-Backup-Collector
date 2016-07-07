using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RemoteWork.Expect;

namespace RemoteWork.Collector
{
    /*
     * утилита для сбора конфигураций по указанному файлу устройств для быстрого тестирования
     * указать строку подключения отдельно???
     или собирать данные и записывать конфигурации в файл
     добавить в конфигурацию возможность настройки шифрования и тому подобное
     */
    class Program
    {
        static void Main(string[] args)
        {
        }
        //сбор конфигураций
        private void Connection()
        {

        }
        //сохраняет список устройств согласно указанному списку в каталог указанный в конфигурации
        //требуется ли создать клиента для создания библиотеки устройств и удобного редактирования или же предоставить лишь схему XML документа
        private void SaveConfiguration()
        {

        }
        //парсим наш список устройств для дальнейшего использования данных
        //создать структуру документа xml для удобного редактирования, создать редактор устройств
        private List<ConnectionData> ParseXml()
        {
            return new List<ConnectionData>();
        }

    }


}
