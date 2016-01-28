using System;
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
using RemoteWork.Data;

namespace RemoteWork
{
    public partial class Rconfig : Form
    {
        RconfigContext context = new RconfigContext();
        public Rconfig()
        {
            InitializeComponent();
            Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<RconfigContext, RemoteWork.Access.Migrations.Configuration>()); 
            LoadData();
        }
        //подгрузка данных для отображения
        public async void LoadData()
        {
            var queryCategory = await (from c in context.Categories
                                       select c).ToListAsync();
            foreach (Category category in queryCategory)
            {
                TreeNode node = new TreeNode();
                node.Text = category.CategoryName;
                //foreach (Favorite favorite in category.Favorites)
                //{
                //    TreeNode childNode = new TreeNode();
                //    childNode.Text = favorite.Hostname;
                //}
                treeViewFavorites.Nodes.Add(node);
            }
            LoadChildData();
        }
        //подгружаем дочерние данные избранные
        public void LoadChildData()
        {
            foreach (TreeNode node in treeViewFavorites.Nodes)
            {
                string nodeGroup = node.Text;
                var queryGroup = (from category in context.Categories
                                  where category.CategoryName == nodeGroup
                                  select category).FirstOrDefault();
                if (queryGroup != null)
                {
                    foreach (Favorite fav in queryGroup.Favorites)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = fav.Hostname;
                        node.Nodes.Add(childNode);
                    }
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
 
            }
        }
     

    }
}
