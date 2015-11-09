using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Net.Http;

namespace hb_desktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void MakePanelsNonvisible()
        {
            this.InventoryPanel.Visible = false;
            this.ClientPanel.Visible = false;
        }

        private void InventoryButton_Click(object sender, EventArgs e)
        {
            MakePanelsNonvisible();
            this.InventoryPanel.BringToFront();
            this.InventoryPanel.Visible = true;

            this.toolStripStatusLabel1.Text = "Retrieving Product Information...";
            GetData("products");
            this.toolStripStatusLabel1.Text = "Done.";
        }

        private void ClientsButton_Click(object sender, EventArgs e)
        {
            MakePanelsNonvisible();
            this.ClientPanel.BringToFront();
            this.ClientPanel.Visible = true;
        }

        private void GetData(String table)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server=Localhost;Port=5432;" +
                    "User Id=postgres;Password=default;Database=testdb;");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT * FROM " + table;
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                dataGridView1.DataSource = dt;
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString());
                //throw;
            }
        }

        private async Task<JsonObject> GetAsync(String table)
        {
            //TODO: Fix this (Ben)
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync()
        }
    }
        
}
