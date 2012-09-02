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
  public partial class DialogInvoice : Form
  {
    public string ClientName,Netto,Description,DatePay,DateSell,TypeOfPay,Symbol,Brutto;

    public DialogInvoice()
    {
      InitializeComponent(); 
      ///Inits:
    }

    public DialogInvoice(string clientName)
    {
      InitializeComponent();
      ///Inits:
      this.ClientName=clientName;
      this.label7.Text="Faktura wystawiana dla: "+this.ClientName;
      this.Refresh();
    }

    private void DialogInvoice_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Netto = textBox1.Text.ToString();
      this.Description = richTextBox1.Text.ToString();

      string month1=null, month2=null;

      if (Convert.ToInt32(dateTimePicker1.Value.Date.Month.ToString()) >= 10)
      {
          month1 = dateTimePicker1.Value.Date.Month.ToString();
      }

      if (Convert.ToInt32(dateTimePicker2.Value.Date.Month.ToString()) >= 10)
      {
          month2= dateTimePicker2.Value.Date.Month.ToString();
      }
      
      if (Convert.ToInt32(dateTimePicker1.Value.Date.Month.ToString()) < 10)
          {
              month1 += "0" + dateTimePicker1.Value.Date.Month.ToString();
          }

          if (Convert.ToInt32(dateTimePicker2.Value.Date.Month.ToString()) < 10)
          {
              month2 += "0" + dateTimePicker2.Value.Date.Month.ToString();
          }
      

      this.DatePay = dateTimePicker1.Value.Date.Day.ToString()+"."+month1+"."+dateTimePicker1.Value.Date.Year.ToString();
      this.DateSell =dateTimePicker2.Value.Date.Day.ToString()+"."+month2+"."+dateTimePicker2.Value.Date.Year.ToString();
      this.TypeOfPay = listBox2.SelectedItem.ToString();
      this.Symbol = listBox1.SelectedItem.ToString();
      this.Brutto = radioButton1.Checked.ToString();
      this.Close();
    }
  }
}
