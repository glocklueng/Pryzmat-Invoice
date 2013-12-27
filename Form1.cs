using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

//TODO:
//Automatic date saving and DataGridView stylesheet
//Dialog to add clients
//Dialog to add invoices!
//Final: Combine with printing

namespace WindowsFormsApplication1
{
  public partial class Form1 : Form
  {
    /// <summary>
    ///   Application configuration
    /// </summary>
    XmlDocument ConfigurationDocument=new XmlDocument();
    public static Dictionary<string,Decimal> ConfigurationTax=new Dictionary<string,Decimal>();
    public static Dictionary<string,string> ConfigurationApp=new Dictionary<string,string>();

    /// <summary>
    ///   Database Connectivity
    /// </summary>
    const int MarginHorizontal = 60;
    const int MarginVertical = 120;

    string connectionstring,selcmdinvoice,selcmdclients;

    SqlConnection Connection=new SqlConnection();
    SqlCommandBuilder BuildInvoices=new SqlCommandBuilder(),BuildClients=new SqlCommandBuilder();
    SqlDataAdapter AdapterInvoices=new SqlDataAdapter(), AdapterClients=new SqlDataAdapter();
    BindingSource SourceInvoices=new BindingSource(), SourceClients=new BindingSource();
    DataTable TableInvoices=new DataTable(),TableClients=new DataTable();

    /// <summary>
    ///   Printing framework
    /// </summary>
    DialogPrint PrintingDialog=new DialogPrint();

    /// <summary>
    ///   Prism specific - conversion routine
    /// </summary>
    ConvertPrism Converter=new ConvertPrism();

    public Form1()
    {
      InitializeComponent();

#if true
      try
      {
          /// <summary>
          ///   Configuration Loading
          /// </summary>
          ConfigurationDocument = new XmlDocument();
          ConfigurationDocument.Load("PrismConfig.xml");
          Decimal TaxValue = ConfigurationRetrieve<Decimal>(ConfigurationDocument, "//gov/tax/vat/text()");
          ConfigurationTax.Add("vat", TaxValue);
          Decimal TaxValue8 = ConfigurationRetrieve<Decimal>(ConfigurationDocument, "//gov/tax/other/text()");
          ConfigurationTax.Add("other", TaxValue8);

          string Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/company/name/text()");
          ConfigurationApp.Add("name", Temporary);
          Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/company/address/text()");
          ConfigurationApp.Add("address", Temporary);
          Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/company/city/text()");
          ConfigurationApp.Add("city", Temporary);
          Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/company/zip/text()");
          ConfigurationApp.Add("zip", Temporary);
          Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/company/country/text()");
          ConfigurationApp.Add("country", Temporary);
          Temporary = ConfigurationRetrieve<string>(ConfigurationDocument, "//app/server/servername/text()");
          ConfigurationApp.Add("servername", Temporary);

          this.connectionstring = "Server=" + ConfigurationApp["servername"] + ";Database=Pryzmat_2013;User=Pryzmat1;Trusted_Connection=true;";
          this.selcmdinvoice = "Select * from Invoices;";
          this.selcmdclients = "Select * from Clients;";
          /// <summary>
          ///   Mysql Connecting
          /// </summary>
          Connection = new SqlConnection(connectionstring);
          Connection.Open();

          if (Connection.State == ConnectionState.Open)
          {
              StatusSet("Połączenie z bazą danych ustanowione");
          }

          AdapterInvoices = new SqlDataAdapter(selcmdinvoice, Connection);
          AdapterClients = new SqlDataAdapter(selcmdclients, Connection);

          BuildInvoices = new SqlCommandBuilder(AdapterInvoices);
          BuildClients = new SqlCommandBuilder(AdapterClients);

          AdapterInvoices.Fill(TableInvoices);
          AdapterClients.Fill(TableClients);

          SourceInvoices.DataSource = TableInvoices;
          SourceClients.DataSource = TableClients;

          this.dataGridView1.DataSource = SourceInvoices;
          this.dataGridView2.DataSource = SourceClients;

      }
      catch (Exception E)
      {
          this.StatusReport(E.ToString());
      } 
#endif
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      this.tabControl1.Width = this.Width-MarginHorizontal;
      this.tabControl1.Height = this.Height-MarginVertical;
      this.StatusSet("Zmiana rozmiaru - Szerokość: " + this.tabControl1.Width + "px , Wysokość: " + this.tabControl1.Height+ "px");
    }
        
    private void wyjdźToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      try
	{
	  AdapterInvoices.Update(((DataTable)SourceInvoices.DataSource));
	  AdapterClients.Update(((DataTable)SourceClients.DataSource));

	  DialogResult res=MessageBox.Show("Czy na pewno opuścić program?","Kończę...",MessageBoxButtons.YesNo);
	  if(res==System.Windows.Forms.DialogResult.Yes)
	    {
	      Application.Exit();
	    }
	}
      catch(Exception E)
	{
	  StatusReport(E.ToString()); 
	}
    }

    private void zapiszDaneToolStripMenuItem2_Click(object sender, EventArgs e)
    {

    }

    private void dodajKlientaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
	{
	  DialogClients ClientDialog=new DialogClients();
	  DialogResult Res=ClientDialog.ShowDialog();

      string[] Row=new string[]{null,ClientDialog.Name,ClientDialog.City,ClientDialog.Address,ClientDialog.Nip,ClientDialog.Zip};

      if (Res == DialogResult.Yes)
      {
          ((DataTable)SourceClients.DataSource).Rows.Add(Row);
          AdapterClients.Update((DataTable)SourceClients.DataSource);
      }

      ClientDialog.Dispose();
      }catch(Exception E)
	{
	  StatusReport(E.ToString());
	}
    }

    private void dodajFakturęToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      //Dodaj fakturę z menu górnego
      try
	{
	  Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
	  Int32 RowCount=dataGridView1.RowCount;
	  string netto;

	  DataGridViewRow row=dataGridView2.SelectedRows[0]; //Wybieramy pierwszą wybraną osobe z bazy

	  DialogInvoice InvoiceDialog=new DialogInvoice(row.Cells["name"].Value.ToString());
	  DialogResult Res=InvoiceDialog.ShowDialog(this);

	  if(InvoiceDialog.Brutto=="True"){
	    Decimal boolhup=Convert.ToDecimal(InvoiceDialog.Netto);
	    boolhup/=1+((Decimal)ConfigurationTax["vat"]/(Decimal)100);
	    boolhup=Decimal.Round(boolhup,2);
	    netto=boolhup.ToString();
	  }else{
	    netto=InvoiceDialog.Netto;
	  } 

	  string[] Row= new string[]{null, row.Cells["id"].Value.ToString(), netto, InvoiceDialog.Description, InvoiceDialog.DateSell, InvoiceDialog.DatePay, InvoiceDialog.TypeOfPay, InvoiceDialog.Symbol, RowCount.ToString() };

	  if(Res==DialogResult.Yes){
	    ((DataTable)SourceInvoices.DataSource).Rows.Add(Row);
	    AdapterInvoices.Update((DataTable)SourceInvoices.DataSource);
	  }

	  row.Dispose();
	  InvoiceDialog.Dispose(); 
	}catch(Exception E)
	{
	  StatusReport(E.ToString());
	}
    }

    private void wyjdźToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //ContextToolStrip
      DialogResult res=MessageBox.Show("Czy na pewno opuścić program?","Kończę...",MessageBoxButtons.YesNo);
      AdapterInvoices.Update((DataTable)SourceInvoices.DataSource);
      AdapterClients.Update((DataTable)SourceClients.DataSource);
      if(res==System.Windows.Forms.DialogResult.Yes)
	{
	  Application.Exit();
	}
    }

    private void zapiszDaneToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      try
	{
	  //ContextToolStrip
	  if(this.tabControl1.SelectedTab==tabPage1)
	    {
	      StatusSet("Zapisano tabelę faktur");
	      AdapterInvoices.Update(((DataTable)SourceInvoices.DataSource));
	      ((DataTable)SourceInvoices.DataSource).Clear();
	      AdapterInvoices.Fill((DataTable)SourceInvoices.DataSource);
	      dataGridView1.DataSource=SourceInvoices;
	      dataGridView1.Refresh();
	    }
	  else if(this.tabControl1.SelectedTab==tabPage2)
	    {
	      StatusSet("Zapisano tabelę klientów ");
	      AdapterClients.Update(((DataTable)SourceClients.DataSource));
	      ((DataTable)SourceClients.DataSource).Clear();
	      AdapterClients.Fill((DataTable)SourceClients.DataSource);
	      dataGridView2.DataSource=SourceClients;
	      dataGridView2.Refresh();
	    }
	}catch(Exception E)
	{
	  StatusReport(E.ToString());
	}
    }

    private void fakturaZBazyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
	{
	  //Main ToolStrip
	  if(this.tabControl1.SelectedTab==tabPage1)
	    {
	      StatusSet("Przygotowywanie wydruku");
	      Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
	      
	      if(selectedRowCount==1)
		{
		  DataGridViewRow PrintedInvoice=this.dataGridView1.SelectedRows[0];
		  DataRow Client=SelectClient(Convert.ToInt32(PrintedInvoice.Cells["clientid"].Value.ToString()));
		  PrintingDialog=new DialogPrint();
		  DataGridViewRow SelectedInvoice=dataGridView1.SelectedRows[0];
		  
		  PrintingDialog.contractNum=SelectedInvoice.Cells["number"].Value.ToString();
		  PrintingDialog.clientDatarow=Client;
		  PrintingDialog.serviceDesc=SelectedInvoice.Cells["description"].Value.ToString();
		  PrintingDialog.paydate=SelectedInvoice.Cells["datepay"].Value.ToString();
		  PrintingDialog.selldate=SelectedInvoice.Cells["datesell"].Value.ToString();
		  PrintingDialog.typeofpayment=SelectedInvoice.Cells["typeofpay"].Value.ToString();
		  PrintingDialog.servsymbol=SelectedInvoice.Cells["symbol"].Value.ToString();
		  PrintingDialog.serviceCost=Convert.ToDecimal(SelectedInvoice.Cells["netto"].Value.ToString());
		  
		  PrintPreviewDialog dlg=new PrintPreviewDialog();
		  dlg.Document=PrintingDialog;
		  if(dlg.ShowDialog()==DialogResult.OK)
		    {
		      PrintingDialog.Print();
		    }
		}else
		   {
		     MessageBox.Show("Zaznaczono więcej niż jedną fakturę do wydruku");
		   }
	    }
	  else if(this.tabControl1.SelectedTab==tabPage2)
	    {
	      StatusSet("Klientów nie drukuję!");
	    }     
	}catch(Exception E)
	{
	  StatusReport(E.ToString());
	}
    }

    private void FakturęToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
	{
	  //Main ToolStrip
	  if(this.tabControl1.SelectedTab==tabPage1)
	    {
	      StatusSet("Przygotowywanie wydruku");
	      Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
	      
	      if(selectedRowCount==1)
		{
		  DataGridViewRow PrintedInvoice=this.dataGridView1.SelectedRows[0];
		  DataRow Client=SelectClient(Convert.ToInt32(PrintedInvoice.Cells["clientid"].Value.ToString()));
		  PrintingDialog=new DialogPrint();
		  DataGridViewRow SelectedInvoice=dataGridView1.SelectedRows[0];
		  		  
		  PrintingDialog.contractNum=SelectedInvoice.Cells["number"].Value.ToString();
		  PrintingDialog.clientDatarow=Client;
		  PrintingDialog.serviceDesc=SelectedInvoice.Cells["description"].Value.ToString();
		  PrintingDialog.paydate=SelectedInvoice.Cells["datepay"].Value.ToString();
		  PrintingDialog.selldate=SelectedInvoice.Cells["datesell"].Value.ToString();
		  PrintingDialog.typeofpayment=SelectedInvoice.Cells["typeofpay"].Value.ToString();
		  PrintingDialog.servsymbol=SelectedInvoice.Cells["symbol"].Value.ToString();
		  PrintingDialog.serviceCost=Convert.ToDecimal(SelectedInvoice.Cells["netto"].Value.ToString());
		  
		  PrintPreviewDialog dlg=new PrintPreviewDialog();
		  dlg.Document=PrintingDialog;
		  if(dlg.ShowDialog()==DialogResult.OK)
		    {
		      PrintingDialog.Print();
		    }
		}else
		   {
		     MessageBox.Show("Zaznaczono więcej niż jedną fakturę do wydruku");
		   }
	    }
	  else if(this.tabControl1.SelectedTab==tabPage2)
	    {
	      StatusSet("Klientów nie drukuję!");
	    }     
	}catch(Exception E)
	{
	  StatusReport(E.ToString());
	}
    }
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
    
    private void contextMenuClick_Save(object sender, EventArgs e)
    {
      
    }

    private void contextMenuClick_Exit(object sender, EventArgs e)
    {
      
    }

    private void contextMenuClick_Print(object sender, EventArgs e)
    {
      
    }

    /// <summary>
    ///   Status Reporting:
    /// </summary>
    private void StatusSet(string statustext)
    {
      this.toolStripStatusLabel1.Text=statustext;
    }

    private void StatusReport(string statustext)
    {
      string message = "Raportowanie błędu:\n"+statustext;
      string caption = "Czy zakończyć aplikację?";
      MessageBoxButtons buttons = MessageBoxButtons.YesNo;
      DialogResult result;

      result=MessageBox.Show(message,caption,buttons);

      if(result==System.Windows.Forms.DialogResult.Yes)
	{
	  Environment.Exit(1);
	}
    }

    private T ConfigurationRetrieve<T>(XmlDocument root,string position)
    {
      //This method returns a specified typed configuration value
      //from an xml file
      try
	{
	  XmlNode Node=root.SelectSingleNode(position);
	  return (T)Convert.ChangeType(Node.Value, typeof(T)); 
	}catch(Exception E)
	{
	  StatusReport(E.ToString());
	  return (T)Convert.ChangeType("0",typeof(T));
	}
    }

    public DataRow SelectClient(int id)
    {
      DataTable CurrTable = TableClients;
      try
	{
	  SqlConnection TemporaryConnection=new SqlConnection(this.connectionstring);
	  SqlCommand SelectNipName=new SqlCommand("SELECT * FROM Clients WHERE Clients.id=\'"+id.ToString()+"\'",TemporaryConnection);
	  TemporaryConnection.Open();
	  SqlDataReader Reader=SelectNipName.ExecuteReader();
	  if(Reader.HasRows==true)
	    {
	      Reader.Read(); //Zawsze pobieramy pojedyńczy element

	      DataRow ReturnRow=CurrTable.NewRow();
	      ReturnRow["name"]=Reader["name"];
	      ReturnRow["address"]=Reader["address"];
	      ReturnRow["city"]=Reader["city"];
	      ReturnRow["zip"]=Reader["zip"];
	      ReturnRow["nip"]=Reader["nip"];

	      Reader.Close();
	      return ReturnRow;
	    }else
	       {
		 DataRow Failure = CurrTable.NewRow();
		 return Failure;		 
	       }
	}
      catch(Exception E)
	{
	  StatusReport(E.ToString());
	  DataRow Failure=CurrTable.NewRow();
	  return Failure;
	}
    }

    private void utwórzFakturęDlaTegoKlientaToolStripMenuItem_Click(object sender, EventArgs e)
    {//Dodaj fakture z menu podrecznego
      try
	{
	  if(this.tabControl1.SelectedTab==tabPage2){
	    Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
	    DialogInvoice InvoiceDialog = new DialogInvoice(row.Cells["name"].Value.ToString());
	    DialogResult Res = InvoiceDialog.ShowDialog(this);
 
	    string year = InvoiceDialog.YearSell;
	    
	    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Invoices WHERE datesell like '".year."')", Connection);
	    Connection.Open();
	    int yrcount = cmd.ExecuteScalar();
	    Conneciton.Close();
	    
	    Int32 RowCount = dataGridView1.RowCount;
	    string netto;

	    DataGridViewRow row = dataGridView2.SelectedRows[0]; //Wybieramy pierwszą wybraną osobe z bazy

	    if (InvoiceDialog.Brutto == "True")
	      {
		Decimal boolhup = Convert.ToDecimal(InvoiceDialog.Netto);
		boolhup /= 1 + ((Decimal)ConfigurationTax["vat"] / (Decimal)100);
		boolhup = Decimal.Round(boolhup, 2);
		netto = boolhup.ToString();
	      }
	    else
	      {
		netto = InvoiceDialog.Netto;
	      }

	    string[] Row = new string[] { null, row.Cells["id"].Value.ToString(), netto, InvoiceDialog.Description, InvoiceDialog.DateSell, InvoiceDialog.DatePay, InvoiceDialog.TypeOfPay, InvoiceDialog.Symbol, yrcount };

	    if(Res==DialogResult.Yes){
	      ((DataTable)SourceInvoices.DataSource).Rows.Add(Row);
	      AdapterInvoices.Update((DataTable)SourceInvoices.DataSource);
	    }

	    row.Dispose();
	    InvoiceDialog.Dispose();
	  }else
	     {
	       StatusSet("Nie dodaję faktur do faktur. Wybierz klienta, któremu chcesz dodać fakturę");
	     }
	}
      catch (Exception E)
	{
	  StatusReport(E.ToString());
	}
    }
  }
}
