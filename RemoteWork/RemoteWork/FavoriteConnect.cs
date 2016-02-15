using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork
{
    //структура для запуска задачи
    struct FavoriteConnect
    {
        public Favorite favorite;
        public List<string> commands;
        public RemoteTask task;
    }
}
