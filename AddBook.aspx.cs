using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bibliotheque
{
    public partial class AddBook : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.ButtonAddBook.UniqueID;
            TextBoxCodeISBN.Focus();
        }

        protected void Btn_AddBook_Click(object sender, EventArgs e)
        {
            try
            { 
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();
               // SqlDataAdapter da = new SqlDataAdapter();

                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.CommandText = "insertBook";

                commande.Parameters.Add("@codeISBN", System.Data.SqlDbType.NVarChar).Value = TextBoxCodeISBN.Text;

                string txtInitialCode = TextBoxInitialCode.Text; int initialCode = 0;
                Int32.TryParse(txtInitialCode, out initialCode);
                if ((txtInitialCode == "") || (initialCode == 0)) { throw new Exception("The initial code is mandatory."); }
                commande.Parameters.Add("@initialCod", System.Data.SqlDbType.Int).Value = initialCode;

                if (TextBoxTitle.Text == "") { throw new Exception("The title is mandatory."); }
                commande.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = TextBoxTitle.Text;
                commande.Parameters.Add("@author", System.Data.SqlDbType.NVarChar).Value = TextBoxAuthor.Text;
                commande.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = TextBoxDescription.Text;
                commande.Parameters.Add("@category", System.Data.SqlDbType.NVarChar).Value = bookcategory.SelectedItem.Value;

                if (FileUploadBook.FileName != "")
                {
                    Stream fs = FileUploadBook.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    commande.Parameters.Add("@photo", System.Data.SqlDbType.VarBinary).Value = bytes;
                }
                else
                {
                    commande.Parameters.Add("@photo", System.Data.SqlDbType.VarBinary).Value = DBNull.Value;
                }

                commande.Parameters.Add("@extrainfo", System.Data.SqlDbType.NVarChar).Value = TextBoxExtraInfo.Text;

                if((TextBoxPrice.Text).Equals(""))
                {
                    commande.Parameters.Add("@price", System.Data.SqlDbType.Decimal).Value = 0;
                }
                else
                {
                    commande.Parameters.Add("@price", System.Data.SqlDbType.Decimal).Value = Convert.ToDecimal(TextBoxPrice.Text);
                }

                if ((TextBoxPages.Text).Equals(""))
                {
                    commande.Parameters.Add("@pages", System.Data.SqlDbType.Int).Value =0;
                }
                else
                {
                    commande.Parameters.Add("@pages", System.Data.SqlDbType.Int).Value = Convert.ToInt32(TextBoxPages.Text);
                }
                
                commande.Parameters.Add("@availability", System.Data.SqlDbType.Bit).Value = true;

                // DataTable dt = new DataTable();

                conn.Open();
                commande.ExecuteNonQuery();
                conn.Close();

                ((Label)(Master.FindControl("lblMessage"))).Text = "Book added";
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                TextBoxCodeISBN.Text = "";TextBoxInitialCode.Text = ""; TextBoxTitle.Text = ""; TextBoxAuthor.Text = "";
                TextBoxDescription.Text = "";TextBoxExtraInfo.Text = ""; TextBoxPrice.Text = ""; TextBoxPages.Text = "";
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).Text = "Book not added: " + ex.Message;
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
            }
            
        }

    }
}