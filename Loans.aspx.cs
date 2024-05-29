                                                                                                                                                                using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Bibliotheque
{
    public partial class Loans : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSearchUser.UniqueID;
            txtSearchUser.Focus();

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

                gridUsers.EmptyDataText = "Nicio carte gasita";
                gridUsers.DataSource = dt;
                gridUsers.DataBind();
                divGridUsers.Visible = true;
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
                divSearchBooks.Visible = false;
                divChangeBooks.Visible = false;
                divGridBooks.Visible = false;
                divBorrowedBooks.Visible = false;
                
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnSearchBook_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "searchlivre";
                commande.Connection = conn;
                DataTable dt = new DataTable();

                commande.Parameters.Add("@searchString", SqlDbType.NVarChar).Value = txtSearchBooks.Text;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                conn.Close();
                gridBooks.EmptyDataText = "Nicio carte gasita";
                gridBooks.DataSource = dt;
                gridBooks.DataBind();
                divGridBooks.Visible = true;
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnChangeBooks_Click(object sender, EventArgs e)
        {
            try
            {
                divSearchBooks.Visible = true;
                btnSearchBooks.Focus();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
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
                    string name =((Label)(row.FindControl("lblName"))).Text;
                    string surname = ((Label)(row.FindControl("lblSurname"))).Text;
                    string clasa = ((Label)(row.FindControl("lblClasa"))).Text;

                    hfUser.Value = id;
                    lblUserBorrow.Text = "Utilizator: &nbsp" + username + " &nbsp Nume: " + name + "&nbsp Nume familie: " + surname + "&nbsp Clasa: " + clasa;
                    divChangeUser.Visible = true;
                    divUserChoosen.Visible = true;
                    divSearchUser.Visible = false;
                    divGridUsers.Visible = false;
                    divSearchBooks.Visible = true;
                    this.Form.DefaultButton = this.btnSearchBooks.UniqueID;
                    txtSearchBooks.Focus();
                }
                else if (e.CommandName == "Add")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    if (hfBooks.Value == "")
                    {
                        hfBooks.Value = ((Label)(row.FindControl("idlivre"))).Text;
                    }
                    else
                    {
                        hfBooks.Value = hfBooks.Value + ',' + ((Label)(row.FindControl("idlivre"))).Text;
                    }
                    DataTable dt = GetSelectedBooks(hfBooks.Value);
                    gridFinalize.DataSource = dt;
                    gridFinalize.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        divBorrowedBooks.Visible = true;
                        divChangeBooks.Visible = true;
                        divSearchBooks.Visible = false;
                        divGridBooks.Visible = false;
                        this.Form.DefaultButton = this.btnSearchBooks.UniqueID;
                        txtSearchBooks.Focus();

                    }
                }
                else if(e.CommandName == "Delete")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                   
                    int idBook = Convert.ToInt32(((Label)(row.FindControl("idlivre"))).Text);
                    hfBooks.Value= Util.RemoveID(hfBooks.Value , idBook);
                    DataTable dt = GetSelectedBooks(hfBooks.Value);
                    gridFinalize.DataSource =dt;
                    gridFinalize.DataBind();
                    if (dt.Rows.Count == 0)
                    {
                        divBorrowedBooks.Visible = false;
                    }
                    divChangeBooks.Visible = true;
                    divSearchBooks.Visible = false;
                    divGridBooks.Visible = false;
                }
            }
         
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
            
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
                    ((Button)(e.Row.FindControl("btnAdd"))).Enabled = false;
                    ((Label)(e.Row.FindControl("lblAvailability"))).ForeColor = System.Drawing.Color.Red;
                }
                Image img = ((Image)e.Row.FindControl("image"));
                if ((img == null) || (img.DescriptionUrl == ""))
                {
                    img.Visible = false;
                }
            }
        }

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBooks.PageIndex = e.NewPageIndex;
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getallbooks";
                DataTable dt = new DataTable();

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();


                gridBooks.EmptyDataText = "Nicio carte gasita";
                gridBooks.DataSource = dt;
                gridBooks.DataBind();

            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private DataTable GetSelectedBooks(string varBooks)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();
            DataTable dt = new DataTable();
            txtSearchBooks.Focus();
            try
            {
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getborrowedbooks";
                commande.Connection = conn;

                commande.Parameters.Add("@idlivres", SqlDbType.NVarChar).Value = varBooks.ToString();

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }

            return dt;
        }

        protected void gridFinalize_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnBorrowBooks_Click(object sender, EventArgs e)
        {
            try
            {
                divChangeUser.Visible = false;
                divSearchBooks.Visible = false;
                divGridBooks.Visible = false;
                
                foreach (GridViewRow row in gridFinalize.Rows)
                {
                    SqlConnection conn = new SqlConnection(strConnection);
                    SqlCommand commande = new SqlCommand();

                    commande.Connection = conn;
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.CommandText = "insertborrowedbook";

                    if (hfUser.Value == "") { throw new Exception("Te rog selecteaza un utilizator!"); }
                    commande.Parameters.Add("@iduser", SqlDbType.Int).Value = hfUser.Value;

                    string idlivre = ((Label)(row.FindControl("idlivre"))).Text;



                    if (idlivre != null)
                    {
                        commande.Parameters.Add("@idlivre", SqlDbType.NVarChar).Value = idlivre.ToString();

                        commande.Parameters.Add("@returndate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtReturnDate.Text.ToString());
                        commande.Parameters.Add("@realreturndate", SqlDbType.DateTime).Value = DBNull.Value;

                        conn.Open();
                        commande.ExecuteNonQuery();
                        conn.Close();
                        gridBooks.DataBind();
                    }

                }
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                ((Label)(Master.FindControl("lblMessage"))).Text = "Ai imprumutat cu succes !";
                btnBorrowBooks.Enabled = false;
               
                btnPrint.Visible = true;
               
            }
            catch(Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message; 
            }
        }



        protected void btnFinishTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Bon.aspx?Utilizator=" + hfUser.Value);
        }
    }
}
