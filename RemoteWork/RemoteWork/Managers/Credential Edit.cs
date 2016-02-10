using RemoteWork.Access;
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
using System.Data.Entity;

namespace RemoteWork.Managers
{
    //проверка данных учетной записи
    enum CredentialInputValidate
    {
        CredentialNameEmpty,
        CredentialNotUnique,
        UsernameEmpty,
        PasswordEmpty
    }
    public partial class Credential_Edit : Form
    {
        RconfigContext context = new RconfigContext();
        CredentialInputValidate validateInput = CredentialInputValidate.CredentialNameEmpty;
        WindowsMode mode = WindowsMode.ADD;
        Credential currentCredential;
        string prevCredential;
        public Credential_Edit()
        {
            InitializeComponent();
        }
        public Credential_Edit(string credential)
        {
            InitializeComponent();
            mode = WindowsMode.EDIT;
            prevCredential = credential;
            textBoxCredName.Text = credential;
            LoadData();
        }
        //подгружаем данные
        private async void LoadData()
        {
            var queryCredential=await (from c in context.Credentials
                                where c.CredentialName==prevCredential
                                select c).FirstOrDefaultAsync();
            currentCredential = queryCredential;
            //подгружаем данные 
            textBoxUsername.Text = queryCredential.Username;
            textBoxDomain.Text = queryCredential.Domain;
            textBoxPaassword.Text = queryCredential.Password;
        }
        //проверка на уникальность
        private bool isUnique(string credential)
        {
            if (mode == WindowsMode.EDIT && prevCredential == textBoxCredName.Text.Trim())
            {
                return true;
            }
            bool uniqueCommand = true;
            context.Credentials.ToList().ForEach(loc =>
            {
                if (loc.CredentialName == credential)
                {
                    uniqueCommand = false;
                    validateInput = CredentialInputValidate.CredentialNotUnique;
                }

            });

            return uniqueCommand;
        }
        //проверка данных пользователя
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxCredName.Text.Trim()))
            {
                validateInput = CredentialInputValidate.CredentialNameEmpty;
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text.Trim()))
            {
                validateInput = CredentialInputValidate.UsernameEmpty;
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPaassword.Text.Trim()))
            {
                validateInput = CredentialInputValidate.PasswordEmpty;
                return false;
            }
            return isUnique(textBoxCredName.Text.Trim());
        }
        //подтверждение операции
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                switch (mode)
                {
                    case WindowsMode.ADD:
                        currentCredential = new Credential();
                        currentCredential.CredentialName = textBoxCredName.Text.Trim();
                        currentCredential.Username = textBoxUsername.Text.Trim();
                        currentCredential.Domain = textBoxDomain.Text.Trim();
                        currentCredential.Password = textBoxPaassword.Text.Trim();
                        context.Credentials.Add(currentCredential);
                        context.SaveChanges();
                        break;
                    case WindowsMode.EDIT:
                        currentCredential.CredentialName = textBoxCredName.Text.Trim();
                        currentCredential.Username = textBoxUsername.Text.Trim();
                        currentCredential.Domain = textBoxDomain.Text.Trim();
                        currentCredential.Password = textBoxPaassword.Text.Trim();
                        context.Entry(currentCredential).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        break;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validateInput)
                {
                    case CredentialInputValidate.CredentialNameEmpty: NotifyWarning("Credential name is empty!");  break;
                    case CredentialInputValidate.CredentialNotUnique: NotifyWarning("Credential name is already exist!"); break;
                    case CredentialInputValidate.UsernameEmpty: NotifyWarning("Username is empty!"); break;
                    case CredentialInputValidate.PasswordEmpty: NotifyWarning("Password is empty!"); break;
                }
            }
        }
        //отмена операции
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //сменить видимость пароля
        private void checkBoxPassword_CheckStateChanged(object sender, EventArgs e)
        {
            CheckState state = checkBoxPassword.CheckState;
            switch (state)
            {
                case CheckState.Checked: textBoxPaassword.UseSystemPasswordChar = false; break;
                case CheckState.Unchecked: textBoxPaassword.UseSystemPasswordChar = true; break;
            }
        }
        //уведомление 
        private void NotifyWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
