using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bibliotheque
{
    public partial class CasareCarti : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["CarteCasata"] != null)
                    {
                        string idCarteCasata = Request.QueryString["CarteCasata"];
                        TextBox TextBoxidLivre = (TextBox)TableAddBook.FindControl("TextBoxidLivre");
                        TextBox TextBoxCodeISBN = (TextBox)TableAddBook.FindControl("TextBoxCodeISBN");
                        TextBox TextBoxInitialCode = (TextBox)TableAddBook.FindControl("TextBoxInitialCode");
                        TextBox TextBoxTitle = (TextBox)TableAddBook.FindControl("TextBoxTitle");
                        TextBox TextBoxAuthor = (TextBox)TableAddBook.FindControl("TextBoxAuthor");
                        TextBox TextBoxDescription = (TextBox)TableAddBook.FindControl("TextBoxDescription");
                        DropDownList bookcategory = (DropDownList)TableAddBook.FindControl("bookcategory");
                        TextBox TextBoxRegDate = (TextBox)TableAddBook.FindControl("TextBoxRegDate");
                        TextBox TextBoxExtraInfo = (TextBox)TableAddBook.FindControl("TextBoxExtraInfo");
                        TextBox TextBoxPrice = (TextBox)TableAddBook.FindControl("TextBoxPrice");
                        TextBox TextBoxDatacasare = (TextBox)TableAddBook.FindControl("txtDataCasare");

                        DataTable dtCartiCasate = new DataTable();
                        dtCartiCasate = GetData_CartiCasate(idCarteCasata);


                        foreach (DataRow row in dtCartiCasate.Rows)
                        {
                            TextBoxidLivre.Text = row["idlivre"].ToString();
                            TextBoxCodeISBN.Text = row["codeISBN"].ToString();
                            TextBoxInitialCode.Text = row["initialcode"].ToString();
                            TextBoxTitle.Text = row["Title"].ToString();
                            TextBoxAuthor.Text = row["Author"].ToString();
                            TextBoxDescription.Text = row["description"].ToString();
                            bookcategory.Text = row["category"].ToString();
                            TextBoxRegDate.Text = row["creationdate"].ToString();
                            TextBoxDatacasare.Text = row["creationdate"].ToString();
                            TextBoxExtraInfo.Text = row["extrainfo"].ToString();
                            TextBoxPrice.Text = row["price"].ToString();
                            if (TextBoxPrice.Text != null)
                            {

                                TextBoxPriceDen.Text = (Convert.ToDecimal(TextBoxPrice.Text) / 10000).ToString();
                            }
                            else
                            {
                                TextBoxPriceDen.Text = "";
                            }
                            TextBoxidLivre.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        private DataTable GetData_CartiCasate(string carticasateid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand();
                DataTable dt = new DataTable();

                string sqlstring = "Select idlivre, codeISBN, initialcode, author, title,description, category,creationdate, extrainfo, price FROM Livre WHERE idLivre = " + carticasateid;

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sqlstring;
                command.Connection = conn;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TextBoxPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal price;
                    if (TextBoxPrice.Text != null)
                    {
                    Decimal.TryParse(TextBoxPrice.Text, out price);
                    TextBoxPriceDen.Text = ( price / 10000).ToString();
                    }
                    else
                    {
                        TextBoxPriceDen.Text = "";
                    }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        protected void btn_casare_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand command = new SqlCommand();
            

            try
            {
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "casarecarti";

                string idlivre = TextBoxidLivre.Text; int idcarte;
                Int32.TryParse(idlivre, out idcarte);


                command.Parameters.Add("@idlivre", System.Data.SqlDbType.Int).Value = idcarte;
                command.Parameters.Add("@codeISBN", System.Data.SqlDbType.NVarChar).Value = TextBoxCodeISBN.Text;
                if(TextBoxInitialCode.Text == "") { throw new Exception("Codul inițial este obligatoriu"); }
                command.Parameters.Add("@initialCode", System.Data.SqlDbType.NVarChar).Value = TextBoxInitialCode.Text;
                command.Parameters.Add("@creationdate", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(TextBoxRegDate.Text.ToString());
                if(TextBoxTitle.Text == "") { throw new Exception("Titlul este obligatoriu"); }
                command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar).Value = TextBoxTitle.Text;
                if(TextBoxAuthor.Text == "") { throw new Exception("Autorul este obligatoriu"); }
                command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar).Value = TextBoxAuthor.Text;
                command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = TextBoxDescription.Text;
                command.Parameters.Add("@category", System.Data.SqlDbType.NVarChar).Value = bookcategory.SelectedItem.Value;
                command.Parameters.Add("@extrainfo", System.Data.SqlDbType.NVarChar).Value = TextBoxExtraInfo.Text;
                string txtPrice = TextBoxPrice.Text; decimal price;
                Decimal.TryParse(txtPrice, out price);
                command.Parameters.Add("@price", System.Data.SqlDbType.Decimal).Value = price;
                string txtDenPrice = TextBoxPriceDen.Text; decimal denprice;
                Decimal.TryParse(txtDenPrice, out denprice);
                Console.WriteLine(denprice);
                command.Parameters.Add("@denominativeprice", System.Data.SqlDbType.Decimal).Value = denprice;
                command.Parameters.Add("@datacasare", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(txtDataCasare.Text.ToString());

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = "Cartea a fost casată su succes";

                TextBoxidLivre.Text = "";
                TextBoxInitialCode.Text = "";
                TextBoxCodeISBN.Text = "";
                TextBoxTitle.Text = "";
                TextBoxAuthor.Text = "";
                TextBoxDescription.Text = "";
                TextBoxExtraInfo.Text = "";
                TextBoxPrice.Text = "";
                TextBoxPriceDen.Text = "";
                bookcategory.SelectedItem.Selected = bookcategory.SelectedIndex.Equals(0);
                txtDataCasare.Text = "";
                TextBoxRegDate.Text = "";

                
            }
            catch(Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
            

        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            TextBoxidLivre.Text = "";
            TextBoxInitialCode.Text = "";
            TextBoxCodeISBN.Text = "";
            TextBoxTitle.Text = "";
            TextBoxAuthor.Text = "";
            TextBoxDescription.Text = "";
            TextBoxExtraInfo.Text = "";
            TextBoxPrice.Text = "";
            TextBoxPriceDen.Text = "";
            bookcategory.SelectedItem.Selected = bookcategory.SelectedIndex.Equals(0); 
            txtDataCasare.Text = "";
            TextBoxRegDate.Text = "";
        }
    }
}