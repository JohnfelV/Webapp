using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login
{
    public partial class Form7 : Form
    {
        private string connectionString = "server=localhost;database=user_db;username=root;password=;";
        private string currentUsername;

        public Form7(string username)
        {
            InitializeComponent();
            currentUsername = username;
            LoadTransactionHistory();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void LoadTransactionHistory()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM transactions WHERE username = @username AND transaction_type = 'Expense'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", currentUsername);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string category = reader["category"].ToString();
                            decimal amount = decimal.Parse(reader["amount"].ToString());
                            DateTime transactionDate = DateTime.Parse(reader["transaction_date"].ToString());

                            dataGridView2.Rows.Add("Expense", amount, category, transactionDate);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(currentUsername);
            form4.Show();
            this.Hide();
        }

        private void btnveiwexpense_Click(object sender, EventArgs e)
        {

        }
    }
}