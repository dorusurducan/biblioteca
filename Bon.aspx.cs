using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing.Printing;

namespace Bibliotheque
{
    public partial class Bon : System.Web.UI.Page
    {

        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["Utilizator"] != null)
                    {

                        string userID = Request.QueryString["Utilizator"];
                        DataTable dtUserBooks = new DataTable();
                        dtUserBooks = GetData_UserBooks(userID);
                        rptrBooks.DataSource = dtUserBooks;
                        rptrBooks.DataBind();
                       

                        DataTable dtUserInformation = new DataTable();
                        dtUserInformation = GetData_UserInformation(userID);

                        lblAdmin.Text =Session["username"].ToString();
                        lblInfoUder.Text =dtUserInformation.Rows[0][0].ToString() + "&nbsp;&nbsp; PRENUME: " + dtUserInformation.Rows[0][1].ToString() + "&nbsp;&nbsp; NUME: " + dtUserInformation.Rows[0][2].ToString() + "&nbsp;&nbsp; CLASA: " + dtUserInformation.Rows[0][3].ToString();
                        lblDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy", CultureInfo.CreateSpecificCulture("ro-RO"));
                        
                    }



                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }

        private DataTable GetData_UserBooks(string userid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand();
                DataTable dt = new DataTable();

                string sqlstring = "SELECT l.initialcode, l.title, l.author, b.creationdate, b.returndate FROM Livre l INNER JOIN Borrow b ON l.idlivre = b.idlivre  WHERE b.realreturndate is null AND b.iduser =" + userid;

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

        protected DataTable GetData_UserInformation(string userId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand();
                DataTable dt = new DataTable();

                string sqlstring = "SELECT username, name, surname, clasa FROM [User] WHERE iduser = "+ userId;

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sqlstring;
                command.Connection = conn;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private int getPenaltyValue(string idUser)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();


            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "getoverreturndays";

            commande.Parameters.Add("@iduser", SqlDbType.Int).Value = idUser.ToString();

            conn.Open();
            int cad = int.Parse(commande.ExecuteScalar().ToString());
            conn.Close();

            return cad;
        }

        protected void btnRevenire_Click(object sender, EventArgs e)
        {
            Response.Redirect("Loans.aspx");
        }

        protected void btnRevenireReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReturnBooks.aspx");
        }
    }
}