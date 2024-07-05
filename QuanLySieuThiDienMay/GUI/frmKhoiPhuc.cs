using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmKhoiPhuc : Form
    {
        BLL_BackupRestore bk = new BLL_BackupRestore();

        public frmKhoiPhuc()
        {
            InitializeComponent();
            Load += FrmKhoiPhuc_Load;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
        }

        private async void FrmKhoiPhuc_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable servers = await bk.GetSqlServersAsync();
                foreach (DataRow row in servers.Rows)
                {
                    string serverName = row["ServerName"].ToString();
                    string instanceName = row["InstanceName"].ToString();

                    if (string.IsNullOrEmpty(instanceName))
                    {
                        cboLocal.Items.Add(serverName);
                    }
                    else
                    {
                        cboLocal.Items.Add($"{serverName}\\{instanceName}");
                    }
                }

                if (cboLocal.Items.Count > 0)
                {
                    cboLocal.SelectedIndex = 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi truy xuất máy chủ SQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cboLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboData.Items.Clear();

                string serverName = cboLocal.SelectedItem.ToString();
                string userId = txtUser.Text.Trim(); 
                string password = txtPass.Text.Trim(); 

                DataTable databases = await bk.GetDatabasesAsync(serverName, userId, password);
                foreach (DataRow row in databases.Rows)
                {
                    string databaseName = row["database_name"].ToString();
                    cboData.Items.Add(databaseName);
                }

                if (cboData.Items.Count > 0)
                {
                    cboData.SelectedIndex = 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi truy xuất cơ sở dữ liệu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKiemTra_CheckedChanged(object sender, EventArgs e)
        {
            if (btnKiemTra.Checked)
            {
                txtUser.Enabled = true;
                txtPass.Enabled = true;
            }
        }

        private async void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string serverName = cboLocal.SelectedItem.ToString();
                        string databaseName = cboData.SelectedItem.ToString();
                        string userId = txtUser.Text;
                        string password = txtPass.Text; ;

                        await bk.RestoreDatabaseAsync(openFileDialog.FileName, serverName, databaseName, userId, password);

                        MessageBox.Show("Cơ sở dữ liệu được khôi phục thành công.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtPass.Text = "";
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            btnKiemTra.Checked = false;
        }
    }
}
