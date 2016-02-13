using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        //подшрузка данных
        private void LoadData()
        {
            richTextBoxConfig.Text = currentConfig.Current;
        }
        //сохранить конфигурацию в тесктовый файл
        private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();//создали экземпляр
            save.Filter = "Text Files(*.txt)|*.txt";
            save.FilterIndex = 0;//по умолчанию фильтруются текстовые файлы
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.FileName);
                if (save.FileName.Contains(".txt"))
                {
                    try
                    {
                        writer.Write(richTextBoxConfig.Text); //записываем в файл содержимое поля
                        writer.Close();//закрываем writer                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Not access to save file!", "ConfigWatcher - Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }               
            }
        }
        //подробная информация о конфигурации
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = string.Format("Configuration id: {0}, load date: {1}", currentConfig.Id, currentConfig.Date.ToString());
            MessageBox.Show(info, "Configuration details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       
    }
}
