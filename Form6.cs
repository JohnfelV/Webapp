using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(Form2.username);
            form4.Show();
            this.Hide();
        }

        private void btnveiwexpense_Click(object sender, EventArgs e)
        {

        }
    }
}
