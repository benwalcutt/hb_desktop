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
using Newtonsoft.Json;
using System.Net.Http.Headers;

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

            GetRemoteData();
        }

        private void GetData(String table)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                string connstring = String.Format("Server=Localhost;Port=5432;" +
                    "User Id=postgres;Password=default;Database=testdb;");
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT * FROM " + table;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                //throw;
            }
        }

        private async Task GetRemoteData()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://54.187.159.168:8080/hb_server/api0/clients/c19f32bb-19f5-47a2-9774-11bafd20258e");
            String content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ClientModel root = JsonConvert.DeserializeObject<ClientModel>(content);
                Console.WriteLine(root.ToString());
            }
            else
            {
                Console.WriteLine("Error.");
            }
        }       
    }

    public class RootObject
    {
        public List<ClientModel> ClientList { get; set; }
    }

        
}
