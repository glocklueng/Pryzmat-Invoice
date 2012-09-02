using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Printing;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class DialogPrint : PrintDocument
    {
        public string contractNum;
        public DataRow clientDatarow;
        public string serviceDesc;
        public string paydate;
        public string typeofpayment;
        public Decimal serviceCost;
        public string selldate;
        public string servsymbol;
        public string infoaux;

        public DialogPrint()
            : base()
        {
            this.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            this.EndPrint += new PrintEventHandler(PrintDocument_EndPrint);
        }

        void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {

        }

        void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int offset = 65;
                ConvertPrism db = new ConvertPrism();
                decimal tax = Math.Round(serviceCost * 0.23m, 2);
                //Cntr num:
                e.Graphics.DrawString("Nr " + this.contractNum + "/" + DateTime.Now.ToShortDateString().Substring(5, 2) + "/" + DateTime.Now.ToShortDateString().Substring(0, 4), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(90, 63));
                e.Graphics.DrawString("Data sprzedaży: " + this.selldate, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(520, 63));
                e.Graphics.DrawString("Jelenia Góra, dn. " + this.selldate, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(520, 43));
                //Client: 
                string clientname = clientDatarow["name"].ToString();
                int firstpos = clientname.IndexOf(';');
                int finalpos = clientname.IndexOf(';', clientname.IndexOf(';') + 1);

                Console.Write(firstpos.ToString() + finalpos.ToString());

                if (clientname.IndexOf(';') != -1 && finalpos == -1)
                {
                    e.Graphics.DrawString(clientname.Substring(0, firstpos), new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 350, 400, 100));/* new PointF(90, 290 + 75)*/
                    e.Graphics.DrawString(clientname.Substring(firstpos + 1), new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 367, 400, 100));/* new PointF(90, 290 + 75)*/
                }
                else if (firstpos != -1 && finalpos != -1)
                {
                    e.Graphics.DrawString(clientname.Substring(0, firstpos), new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 350, 400, 100));/* new PointF(90, 290 + 75)*/
                    e.Graphics.DrawString(clientname.Substring(firstpos + 1, finalpos - firstpos - 1), new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 367, 400, 100));/* new PointF(90, 290 + 75)*/
                    e.Graphics.DrawString(clientname.Substring(finalpos + 1), new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 383, 400, 100));/* new PointF(90, 290 + 75)*/
                }
                else
                {
                    e.Graphics.DrawString(clientname, new Font("Trebuchet MS", 12), Brushes.Black, new RectangleF(90, 350, 400, 100));/* new PointF(90, 290 + 75)*/
                }

                e.Graphics.DrawString(clientDatarow["address"].ToString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(90, 304 + 102));
                e.Graphics.DrawString(clientDatarow["zip"].ToString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(90, 318 + 107));
                e.Graphics.DrawString(clientDatarow["city"].ToString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(152, 318 + 107));
                e.Graphics.DrawString("NIP: " + clientDatarow["nip"].ToString(), new Font("Trebuchet MS", 12), Brushes.Black, new PointF(520, 350));
                //Usluga:
                e.Graphics.DrawString(infoaux, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(500, 410));
                //
                Math.Round(serviceCost, 2);
                e.Graphics.DrawString(this.serviceCost.ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new RectangleF(367 + 2 * offset - 45, 474 + offset + 30, 300, 100));
                e.Graphics.DrawString(this.serviceCost.ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new RectangleF(367 + 2 * offset - 45, 566 + offset + 96, 300, 100));

                e.Graphics.DrawString(tax.ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new PointF(367 + 3 * offset + 20, 474 + offset + 30));
                e.Graphics.DrawString(tax.ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new PointF(367 + 3 * offset + 20, 566 + offset + 96));

                e.Graphics.DrawString(servsymbol, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(375, 474 + offset + 30));
                e.Graphics.DrawString((serviceCost + tax).ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new PointF(530 + 2 * offset + 10, 474 + offset + 30));
                e.Graphics.DrawString((serviceCost + tax).ToString(), new Font("Trebuchet MS", 11), Brushes.Black, new PointF(530 + 2 * offset + 10, 566 + offset + 96)); //y
                e.Graphics.DrawString("23%", new Font("Trebuchet MS", 11), Brushes.Black, new PointF(320 + 3 * offset + 33, 474 + offset + 30));
                e.Graphics.DrawString("23%", new Font("Trebuchet MS", 11), Brushes.Black, new PointF(320 + 3 * offset + 33, 566 + offset + 96));

                e.Graphics.DrawString(this.serviceDesc.ToString(), new Font("Trebuchet MS", 10), Brushes.Black, new RectangleF(57, 474 + offset + 30, 300, 100));
                //e.Graphics.DrawString(db.ChangeX(serviceCost.ToString()), new Font("Trebuchet MS", 11), Brushes.Blue, new PointF(100, 275));
                //e.Graphics.DrawString(db.ChangeX(tax.ToString()), new Font("Trebuchet MS", 11), Brushes.Blue, new PointF(100, 295)); 
                if (typeofpayment == "zapłacono gotówką") { ;}
                else
                {
                    e.Graphics.DrawString("Termin płatności: " + paydate, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(100, 696 + offset + 120));
                }
                e.Graphics.DrawString("Słownie: " + db.Convert((serviceCost + tax).ToString()), new Font("Trebuchet MS", 9), Brushes.Black, new PointF(100, 696 + offset + 60));
                e.Graphics.DrawString("Sposób płatności: " + typeofpayment, new Font("Trebuchet MS", 11), Brushes.Black, new PointF(100, 696 + offset + 90));
            }
            catch (Exception E)
            {
                DialogResult result=MessageBox.Show(E.ToString(), "Błąd drukowania!",MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes) {
                    Environment.Exit(1);
                }
            }
        }

        public void PrintPrepare(DataRow client, Decimal cost, String description, string PayDate, string payway, string selldate1, string auxinfo, string symbol)
        {
            this.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            this.clientDatarow = client;
            this.serviceCost = cost;
            this.serviceDesc = description;
            //this.contractNum = dbwerk.getMaxContractNumber(); replace this shit!
            this.selldate = selldate1;
            this.servsymbol = symbol;
            this.infoaux = auxinfo;
            paydate = PayDate;
            typeofpayment = payway;
        }

        public void PrintPrepareSpecial(DataRow client, Decimal cost, String description, string num, string typeofpaymn, string dateofpaymn, string selldate1, string auxinfo1, string symbol)
        {
            this.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            this.clientDatarow = client;
            this.serviceCost = cost;
            this.serviceDesc = description;
            this.contractNum = num;
            this.typeofpayment = typeofpaymn;
            this.paydate = dateofpaymn;
            this.selldate = selldate1;
            this.servsymbol = symbol;
            this.infoaux = auxinfo1;
        }

        public new void Print()
        { 
            this.Print(); 
        }
    }
}
