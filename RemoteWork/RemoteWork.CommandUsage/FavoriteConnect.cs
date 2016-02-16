using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.CommandUsage
{
    //Используется в LOOP USAGE
    public struct FavoriteConnect
    {
        public Favorite favorite;
        public List<string> commands;
        public RemoteTask task;
    }   
}
