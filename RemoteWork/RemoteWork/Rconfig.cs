﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteWork.Access;
using System.Data.Entity;

namespace RemoteWork
{
    public partial class Rconfig : Form
    {
        RconfigContext context = new RconfigContext();
        public Rconfig()
        {
            InitializeComponent();
            LoadData();
        }

        public async void LoadData()
        {
            var query = await (from c in context.Favorites
                        select c).ToListAsync();
            dataGridView1.DataSource = query;
        }

    }
}
