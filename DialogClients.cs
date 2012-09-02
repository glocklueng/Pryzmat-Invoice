using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class DialogClients : Form
    {
        public string Name, Address, City, Zip, Nip;

        public DialogClients()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Name = this.textBox1.Text.ToString();
            this.Address = this.textBox2.Text.ToString();
            this.City = this.textBox4.Text.ToString();
            this.Zip = this.textBox3.Text.ToString();
            this.Nip = this.textBox5.Text.ToString();
            this.Close();
        }
    }
}
