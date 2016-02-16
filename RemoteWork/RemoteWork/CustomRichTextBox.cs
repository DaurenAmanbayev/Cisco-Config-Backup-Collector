using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWork
{
    class CustomRichTextBox:RichTextBox
    {
        //fastest richtextbox
        //использует другую версию реализации WordPad  с улучшенной производительностью
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams i_Params = base.CreateParams;
                try
                {
                    // Available since XP SP1
                    Win32.LoadLibrary("MsftEdit.dll"); // throws

                    // Replace "RichEdit20W" with "RichEdit50W"
                    i_Params.ClassName = "RichEdit50W";
                }
                catch
                {
                    // Windows XP without any Service Pack.
                }
                return i_Params;
            }
        }
    }
}
