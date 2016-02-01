using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Expect
{
    public abstract class Expect
    {
        protected ConnectionData host;
        protected bool success = false;
        protected List<string> listError = new List<string>();
        protected List<string> listResult = new List<string>();
        public Expect(ConnectionData host)
        {
            this.host = host;
        }
        public abstract void ExecuteCommand(string command);
        public abstract void ExecuteCommands(List<string> commands);

        public string GetResult()
        {
            return string.Join(Environment.NewLine,listResult.ToArray());
        }
        public string GetError()
        {
            return string.Join(Environment.NewLine, listError.ToArray());
        }
        public bool isSuccess
        {
            get { return success; }
        }
    }
}
