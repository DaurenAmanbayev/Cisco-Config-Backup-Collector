using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteWork.Access;
using RemoteWork.Data;
using System.Data.Entity;
using RemoteWork.Expect;
using System.Threading;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RemoteWork.CommandUsage;

namespace RemoteWorkUtil
{
    class Program
    {
        public static ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        //static RconfigContext context;
        static string logJournal = "rconfig-journal.log";
        static int taskId;
        static void Main(string[] args)
        {           
            //если первый аргумент 
            if (args.Length > 0)
            {
                //спарсить первый аргумент 
                if (Int32.TryParse(args[0], out taskId))
                {
                    //если успешно проиницилизировать задачу
                    //чтобы не загружать компьютер по умолчанию используем метод с потоками
                    //производительность методов
                    CommandUsage comm = new CommandUsage(taskId, CommandUsageMode.TaskParallelUsage);                   
                    comm.taskCompleted += FinishAfterComplete;
                    //запустить выполнение
                    comm.Dispatcher();                   
                    //запускаем ожидание, пока задачи выполняться
                    waitHandle.Wait(); 
                }
                else
                {
                    //записать в логи провал
                    Logging(string.Format("TASK failed!!! Argument is not correct!!"));
                }
            }
            else
            {
                //записать в логи провал
                Logging(string.Format("TASK failed!!! Argument is null!!"));
            }
        }
        //сбрасываем замирание основного потока
        public static void FinishAfterComplete()
        {            
            waitHandle.Set();
        }
        //логгирование процедуры
        #region LOGGING
        private static void Logging(string log)
        {
            string[] content = new string[1] { "**** Logging data ****" };
            FileInfo fileInf = new FileInfo(logJournal);
            if (fileInf.Exists && fileInf.Length < 4000000)//если размер не превышает 4 Мб, прочитать и дополнить данные лога
            {
                FileRead(logJournal, ref content);
            }
            string buffer = string.Join("\n", content);
            string line = "\n";
            string space = " => ";
            string date = DateTime.Now.ToString();
            WriteCharacters(buffer + line + date + space + log, logJournal);
        }
        private static void FileRead(string targetPath, ref string[] content)
        {
            try
            {
                content = File.ReadAllLines(targetPath);
            }
            catch (Exception)
            {

            }
        }
        private static async void WriteCharacters(string targetText, string targetPath)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(targetPath))
                {
                    await writer.WriteLineAsync(targetText);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }  

}
