﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteWork.Managers;

namespace RemoteWork
{
    public partial class Management : Form
    {
        //для блокировки открытия повторных окон
        bool isWindowLockCommand = false;
        bool isWindowLockCategory = false;
        bool isWindowLockLocation = false;
        bool isWindowLockCredential = false;
        public Management()
        {
            InitializeComponent();
        }

        #region WINDOWS LAYOUT
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void minimizeAllToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            // получаем все дочерние формы 
            Form[] forms = this.MdiChildren;

            // каждое дочернее окно минимизируем
            foreach (Form cf in forms) cf.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region CHILD WINDOWS
        private void categoryManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isWindowLockCategory)
            {
                isWindowLockCategory = true;
                Category_Manager frm = new Category_Manager();
                frm.MdiParent = this;
                frm.Show();
                frm.FormClosing += UnlockCategoryWindow;
            }
        }
        private void UnlockCategoryWindow(object sender, EventArgs e)
        {
            isWindowLockCategory = false;
        }

        private void commandManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isWindowLockCommand)
            {
                isWindowLockCommand = true;
                Command_Manager frm = new Command_Manager();
                frm.MdiParent = this;
                frm.Show();
                frm.FormClosing += UnlockCommandWindow;
            }
        }
        private void UnlockCommandWindow(object sender, EventArgs e)
        {
            isWindowLockCommand = false;
        }

        private void locationManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isWindowLockLocation)
            {
                isWindowLockLocation = true;
                Location_Manager frm = new Location_Manager();
                frm.MdiParent = this;
                frm.Show();
                frm.FormClosing += UnlockLocationWindow;
            }
        
        }
        private void UnlockLocationWindow(object sender, EventArgs e)
        {
            isWindowLockLocation = false;
        }

        private void credentialManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isWindowLockCredential)
            {
                isWindowLockCredential = true;
                Credential_Manager frm = new Credential_Manager();
                frm.MdiParent = this;
                frm.Show();
                frm.FormClosing += UnlockCredentialWindow; 
            }
        }
        private void UnlockCredentialWindow(object sender, EventArgs e)
        {
            isWindowLockCredential = false;
        }
        #endregion
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}