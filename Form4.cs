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
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace login
{
    public partial class Form4 : Form
    {
        public string income { get; set; }
        public string expense { get; set; }
        public string wallet { get; set; }
        private string connectionString = "server=localhost;database=user_db;username=root;password=;";
        private string currentUsername; // Store the current username

        public Form4(string username, decimal totalIncome = 0, decimal totalExpenses = 0)
        {
            InitializeComponent();
            currentUsername = username; // Assign the current username
            LoadDashboardData(); // Call the LoadDashboardData method
        }

        // Method to load dashboard data like income, expenses, and wallet balance
        private void LoadDashboardData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to get income and expenses for the current user
                    string balanceQuery = "SELECT SUM(CASE WHEN transaction_type = 'Income' THEN amount ELSE 0 END) AS TotalIncome, " +
                                          "SUM(CASE WHEN transaction_type = 'Expense' THEN amount ELSE 0 END) AS TotalExpenses " +
                                          "FROM transactions WHERE username = @username"; // Filter by username

                    using (MySqlCommand cmd = new MySqlCommand(balanceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", currentUsername); // Pass the username parameter

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve and format total income and expenses
                                decimal totalIncome = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                                decimal totalExpenses = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);

                                // Update labels with the fetched values
                                lblincome.Text = "₱" + totalIncome.ToString("N2");
                                lblexpense.Text = "₱" + totalExpenses.ToString("N2");
                                lblwallet.Text = "₱" + (totalIncome - totalExpenses).ToString("N2"); // Calculate and show wallet balance
                            }
                            else
                            {
                                MessageBox.Show("No data found for the username: " + currentUsername);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message); // Handle database errors
                }
            }
        }

        // Button event for logging out
        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        // Button event for navigating to Form3 (transaction form)
        private void btntransact_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide(); // Hide current form on transaction button click  
        }

        private void btnincome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5(currentUsername);
            form5.Show();
        }

        private void btnexpense_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 form7 = new Form7(currentUsername);
            form7.Show();
        }

        private void btncontact_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6();
            form6.Show();

        }
    }
}