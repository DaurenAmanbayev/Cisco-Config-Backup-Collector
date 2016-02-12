using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWorkUtil
{
    //реализовать перегрузку присваивания для создания полной копии объекта
    //не ссылаясь на объект
    //https://msdn.microsoft.com/en-us/library/aa288467%28v=vs.71%29.aspx
    //можно использовать objectcopier 
    //нужно выгрузить данные, чтобы они были не в базе данных, а локальными
    //затем производить по ним требуемые операции
    //перенести данный класс в RemoteWork.Data
    struct FavoriteConnect
    {
        public Favorite favorite;
        public List<string> commands;
        public RemoteTask task;
    }   
}
