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
using VersionControl.Entities;

namespace VersionControl
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource.FullName;
            btnAdd.Text = Resource.Add;
            btnWrite.Text = Resource.Write;
            btnDelete.Text = Resource.Delete;

            // listbox1
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

            var u = new User()
            {
                FullName = txtLastName.Text,
                
            };
            users.Add(u);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                foreach (var u in users)
                {
                    sw.Write(u.ID+"-"+ u.FullName);
                    sw.WriteLine();
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                var t = listUsers.SelectedItem;
                if (t != null)
                    users.Remove((User)t);
            
        }
    }
}
