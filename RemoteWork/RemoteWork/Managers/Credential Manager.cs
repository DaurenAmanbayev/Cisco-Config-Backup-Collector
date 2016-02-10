using RemoteWork.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace RemoteWork.Managers
{
    public partial class Credential_Manager : Form
    {
        RconfigContext context = new RconfigContext();
        public Credential_Manager()
        {
            InitializeComponent();
            LoadData();
        }
        //подгружаем данные
        private async void LoadData()
        {
            var queryCredentials=await (from c in context.Credentials
                                 select c.CredentialName).ToListAsync();

            listBoxCredentials.DataSource = queryCredentials;
        }
        //добавление учетной записи
        private void addCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            Credential_Edit frm = new Credential_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }

        }
        //редактирование
        private void editCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxCredentials.SelectedItem != null)
            {
                string credential = listBoxCredentials.SelectedItem.ToString();
                Credential_Edit frm = new Credential_Edit(credential);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        //удаление
        private void deleteCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxCredentials.SelectedItem != null)
            {
                string credential = listBoxCredentials.SelectedItem.ToString();
                var queryCredential=(from c in context.Credentials
                                    where c.CredentialName==credential
                                    select c).FirstOrDefault();
                if (queryCredential != null)
                {
                    context.Credentials.Remove(queryCredential);
                    context.SaveChanges();
                }

            }
        }
    }
}
