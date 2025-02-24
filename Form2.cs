﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace login
{
    public partial class Form2 : Form
    {
        
        private string connectionString = "server=localhost;database=user_db;username=root;password=;";
        public static string username;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            string username = txtusername.Text;
            string password = txtpass.Text;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Login successful!");
                Form2.username = txtusername.Text;  // Set the static username field

                // Pass the username to Form4
                Form4 form4 = new Form4(Form2.username);
                form4.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }

            reader.Close();
            connection.Close();


        }


        private void ckbshow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbshow.Checked)
            {
                txtpass.PasswordChar = '\0';
            }
            else
            {
                txtpass.PasswordChar = '*';
            }
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txtpass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtpass.Text))
            {
                txtpass.Focus();
                errorProvider1.SetError(txtpass, "Username is required");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtpass, "");
            }
        }
    }
    
}
