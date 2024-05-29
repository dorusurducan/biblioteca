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
    public partial class ReturnBooks : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSearchUser.UniqueID;
            txtSearchUser.Focus();

            
        }

        protected void gridview_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    string id = ((Label)(row.FindControl("lblIdUser"))).Text;
                    string username = ((Label)(row.FindControl("lblUsername"))).Text;
                    string name = ((Label)(row.FindControl("lblName"))).Text;
                    string surname = ((Label)(row.FindControl("lblSurname"))).Text;
                    string clasa = ((Label)(row.FindControl("lblClasa"))).Text;

                    hfUser.Value = id;
                    lblUserBorrow.Text = "Utilizator: &nbsp" + username + " &nbsp Nume: " + name + "&nbsp Nume familie: " + surname + "&nbsp Clasa: " + clasa;
                    divUserChoosen.Visible = true;
                    divGridUsers.Visible = false;
                    divSearchUser.Visible = false;
                    divChangeUser.Visible = true;
                    divPenalizare.Visible = false;
                    ShowHistory();

                }
                else if(e.CommandName == "returnBook")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    string idbook = ((Label)(row.FindControl("idlivre"))).Text;
                    int idusr =Int32.Parse(hfUser.Value);
                   
                    SqlConnection conn = new SqlConnection(strConnection);
                    SqlCommand commande = new SqlCommand();
                    DataTable dt = new DataTable();

                    commande.Connection = conn;
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.CommandText = "updatereturnbook";

                    commande.Parameters.Add("@idlivre", SqlDbType.Int).Value = idbook;
                    commande.Parameters.Add("@iduser", SqlDbType.Int).Value = idusr;

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(commande);
                    da.Fill(dt);
                    commande.ExecuteReader();
                    conn.Close();
                    ShowHistory();
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Carte returnata!";
                }
                else if(e.CommandName == "PrelungesteImprumut")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    string idbook = ((Label)(row.FindControl("idlivre"))).Text;
                    int idusr = Int32.Parse(hfUser.Value);
                    int zileimprumut = int.Parse(((TextBox)(row.FindControl("txtZilePrelungire"))).Text);

                    prelungireImprumut(idbook,zileimprumut,idusr);
                    ShowHistory();
                    btnPrint.Visible = true;
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Imprumut prelungit!";
                }
              }
            catch(Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridViewBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAv = (Label)(e.Row.FindControl("lblAvailability"));
                if (lblAv.Text == "True")
                {
                    ((Label)(e.Row.FindControl("lblAvailability"))).Text = "Disponibila";
                }
                else
                {
                    ((Label)(e.Row.FindControl("lblAvailability"))).Text = "Imprumutata";

                    ((Label)(e.Row.FindControl("lblAvailability"))).ForeColor = System.Drawing.Color.Red;
                }

                Image img = ((Image)e.Row.FindControl("image"));
                if ((img == null) || (img.DescriptionUrl == ""))
                {
                    img.Visible = false;
                }
            }
        }

        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "searchuserborrow";

                DataTable dt = new DataTable();

                commande.Parameters.Add("@searchstring", SqlDbType.NVarChar).Value = txtSearchUser.Text;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();

                gridUsers.EmptyDataText = "Nicio inregistrare gasita";
                gridUsers.DataSource = dt;
                gridUsers.DataBind();
                divGridUsers.Visible = true;
                divPenalizare.Visible = false;
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnChangeUser_Click(object sender, EventArgs e)
        {
            try
            {
                divSearchUser.Visible = true;
                divChangeUser.Visible = false;
                divGridUsers.Visible = false;
                divUserChoosen.Visible = false;
                divBooksBorrowed.Visible = false;
                ((Label)(Master.FindControl("lblMessage"))).Text = "";
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private void ShowHistory()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getborrowedbook";

                commande.Parameters.Add("@iduser", SqlDbType.Int).Value = hfUser.Value;

                DataTable dt = new DataTable();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteNonQuery();
                conn.Close();

                gridReturnBooks.EmptyDataText = "Nicio carte gasita";
                gridReturnBooks.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                gridReturnBooks.DataSource = dt;
                gridReturnBooks.DataBind();
                divBooksBorrowed.Visible = true;
                divPenalizare.Visible = true;

                int penalty = getPenaltyValue(hfUser.Value);
                if (penalty == 0)
                {
                    lblPenalizare.Text = "0";
                    divPenalizare.Visible = false;
                }
                else
                {
                    lblPenalizare.Text = Convert.ToString(penalty);
                    divPenalizare.Visible = true;
                }
                
            }
            catch(Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private void prelungireImprumut(string idBook, int zileImprumut, int idUser)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "updateloanedbook";

                commande.Parameters.Add("@idlivre", SqlDbType.Int).Value = idBook;
                commande.Parameters.Add("@iduser", SqlDbType.Int).Value = idUser;
                commande.Parameters.Add("@days", SqlDbType.Int).Value = zileImprumut;
                commande.Parameters.Add("@realreturndate", SqlDbType.DateTime).Value = DBNull.Value;

                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();
                
            }
            catch(Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bon.aspx?Utilizator=" + hfUser.Value);
        }
    }
}