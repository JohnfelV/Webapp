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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace login
{
    public partial class Form3 : Form
    {
        private string connectionString = "server=localhost;database=user_db;username=root;password=;";
        private decimal initialBalance = 0;

        public Form3()
        {
            InitializeComponent();
            LoadInitialBalance(); // Load the initial balance when the form initializes  
        }

        private void LoadInitialBalance()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT initial_balance FROM Wallet LIMIT 1"; // Adjust as needed  
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        // Check if result is DBNull or null  
                        if (result != null && result != DBNull.Value)
                        {
                            initialBalance = Convert.ToDecimal(result);
                        }
                        else
                        {
                            // Set a default initial balance if none exists  
                            initialBalance = 0; // or any other default amount  
                        }
                    }
                }
                catch (MySqlException ex)
                {
                
                }
            }

            // Display the initial balance in a TextBox  
            txtbalance.Text = "₱" + initialBalance.ToString("N2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal amount;

            // Validate the entered amount  
            if (!decimal.TryParse(txtamount.Text, out amount) || amount < 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            string category = cmbcategory.Text;
            string transactionType = "";

            // Check the transaction type based on the RadioButton selection
            if (radioButton1.Checked)
            {
                transactionType = "Income";
            }
            else if (radioButton2.Checked)
            {
                transactionType = "Expense";
            }
            else
            {
                MessageBox.Show("Please select a valid transaction type.");
                return;
            }

            DateTime transactionDate = dateTimePicker1.Value;

            // Check the transaction type and update balance accordingly  
            if (transactionType == "Expense") // Subtract for expenses  
            {
                initialBalance -= amount;
            }
            else if (transactionType == "Income") // Add for income  
            {
                initialBalance += amount;
            }

            // Update the displayed balance  
            txtbalance.Text = "₱" + initialBalance.ToString("N2");

            // Insert the transaction into the database  
            InsertTransaction(amount, category, transactionType, transactionDate, Form2.username); // Pass the current username value

            this.Hide();
            Form4 form4 = new Form4(Form2.username);  // Pass the username here to Form4 constructor
            form4.Show();
        }


        private void InsertTransaction(decimal amount, string category, string transactionType, DateTime transactionDate, string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO transactions (username, amount, category, transaction_type, transaction_date) VALUES (@username, @amount, @category, @transactionType, @transactionDate)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        // Add parameters  
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@transactionType", transactionType);
                        cmd.Parameters.AddWithValue("@transactionDate", transactionDate);

                        // Execute the command  
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Transaction added successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Error adding transaction.");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Add categories to the ComboBox
            cmbcategory.Items.Add("Salary");
            cmbcategory.Items.Add("Investments");
            cmbcategory.Items.Add("Other");

            cmbcategory.Items.Add("Rent");
            cmbcategory.Items.Add("Utilities");
            cmbcategory.Items.Add("Food");
            cmbcategory.Items.Add("Transportation");
            cmbcategory.Items.Add("Other");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                cmbcategory.Items.Clear();
                cmbcategory.Items.Add("Salary");
                cmbcategory.Items.Add("Investments");
                cmbcategory.Items.Add("Other");
                cmbcategory.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                cmbcategory.Items.Clear();
                cmbcategory.Items.Add("Rent");
                cmbcategory.Items.Add("Utilities");
                cmbcategory.Items.Add("Food");
                cmbcategory.Items.Add("Transportation");
                cmbcategory.Items.Add("Other");
                cmbcategory.Visible = true;
            }
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(Form2.username);
            form4.Show();
        }
    }
}
