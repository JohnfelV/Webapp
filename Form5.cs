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
    public partial class Form5 : Form
    {
        private string connectionString = "server=localhost;database=user_db;username=root;password=;";
        private string currentUsername;
        public Form5(string username)
        {
            InitializeComponent();
            currentUsername = username;
            LoadTransactionHistory();
        }

        private void TransactionHistoryForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadTransactionHistory()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM transactions WHERE username = @username AND transaction_type = 'Income'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", currentUsername);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            decimal amount = decimal.Parse(reader["amount"].ToString());
                            DateTime transactionDate = DateTime.Parse(reader["transaction_date"].ToString());

                            dataGridView1.Rows.Add("Income", amount, "", transactionDate);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            // Retrieve the total income and expenses from the database
            decimal totalIncome = GetTotalIncome(currentUsername);
            decimal totalExpenses = GetTotalExpenses(currentUsername);

            // Navigate to the dashboard
            Form4 form4 = new Form4(currentUsername, totalIncome, totalExpenses);
            form4.Show();
            this.Hide();
        }

        private decimal GetTotalIncome(string username)
        {
            decimal totalIncome = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(amount) FROM transactions WHERE username = @username AND transaction_type = 'Income'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        decimal income = (decimal)cmd.ExecuteScalar();
                        totalIncome = income;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }

            return totalIncome;
        }

        private decimal GetTotalExpenses(string username)
        {
            decimal totalExpenses = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(amount) FROM transactions WHERE username = @username AND transaction_type = 'Expense'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        decimal expenses = (decimal)cmd.ExecuteScalar();
                        totalExpenses = expenses;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }

            return totalExpenses;
        }

        private void btnViewTransactions_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
