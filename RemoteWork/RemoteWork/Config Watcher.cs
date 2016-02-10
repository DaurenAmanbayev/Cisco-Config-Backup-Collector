using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWork
{
    public partial class Config_Watcher : Form
    {
        Config currentConfig;
        public Config_Watcher(Config config)
        {
            InitializeComponent();
            currentConfig = config;
            LoadData();
        }

        private void LoadData()
        {
            richTextBoxConfig.Text = currentConfig.Current;
        }
    }
}
