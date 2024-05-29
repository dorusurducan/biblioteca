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
    public partial class Biblio : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(Page.IsPostBack))
                {
                   
                    GridViewBind();
                }

            }
            catch (Exception ex)
            {

                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Btn_CreateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterBiblio.aspx", false);
        }

        protected void Btn_AddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx", false);
        }

       

        protected void gridview_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName == "Modify") || (e.CommandName == "ResetPassword"))
                {
                    gridUsers.EditIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewBind();
                }
                else if (e.CommandName == "Update")
                {
                    //find your textboxes
                    GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                    Label lblId = (Label)gvr.FindControl("lblIdUserEdit");
                    TextBox txtName = (TextBox)gvr.FindControl("txtName");
                    TextBox txtUsername = (TextBox)gvr.FindControl("txtUsername");
                    TextBox txtSurname = (TextBox)gvr.FindControl("txtSurname");
                    TextBox txtClasa = (TextBox)gvr.FindControl("txtClasa");

                    UpdateUser(lblId.Text, txtUsername.Text, txtName.Text, txtSurname.Text, txtClasa.Text);
                }
                else if (e.CommandName == "CancelUpdate")
                {
                    gridUsers.EditIndex = -1;
                    GridViewBind();
                }
                else if (e.CommandName == "UpdatePassword")
                {
                    //get the new password 
                    GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                    Label lblId = (Label)gvr.FindControl("lblIdUserEdit");
                    TextBox txtPassword = (TextBox)gvr.FindControl("txtPassword");
                    TextBox txtConfirmPassword = (TextBox)gvr.FindControl("txtConfirmPassword");

                    //verify the confirmed pass
                    if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
                    { throw new Exception("Parola introdusa nu este aceeasi."); }

                    //update in sql server
                    UpdatePassword(lblId.Text, txtPassword.Text);

                    //show message "the password was updated"
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Parola a fost actualizata!";
                }
                else if(e.CommandName == "Delete")
                {
                    GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                    Label lblId = (Label)gvr.FindControl("lblIdUser");

                    deleteUser(lblId.Text);

                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Utilizator sters";

                    GridViewBind();
                }
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsers.PageIndex = e.NewPageIndex;
            GridViewBind();

        }

        protected void gridview_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
        protected void gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }
        protected void gridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void GridViewBind()
        {
            SqlConnection conn = new SqlConnection(strConnection);

            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            commande.CommandText = "getallusers";
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(dt);
            commande.ExecuteReader();
            conn.Close();


            gridUsers.EmptyDataText = "Nici un utilizator gasit";
            gridUsers.DataSource = dt;
            gridUsers.DataBind();
        }
        private void UpdateUser(string iduser, string username, string name, string surname, string clasa)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "updateuser";

            commande.Parameters.Add("@iduser", SqlDbType.Int).Value = Convert.ToInt32(iduser).ToString();
            commande.Parameters.Add("@username", SqlDbType.NVarChar).Value = username.ToString();
            commande.Parameters.Add("@name", SqlDbType.NVarChar).Value = name.ToString();
            commande.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname.ToString();
            commande.Parameters.Add("@clasa", SqlDbType.NVarChar).Value = clasa.ToString();

            conn.Open();
            commande.ExecuteNonQuery();
            conn.Close();
        }

        private void UpdatePassword(string iduser, string password)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "updatepassword";

            commande.Parameters.Add("@iduser", SqlDbType.Int).Value = Convert.ToInt32(iduser).ToString();
            commande.Parameters.Add("@password", SqlDbType.NVarChar).Value = Util.EncryptPassword(password.ToString());

            conn.Open();
            commande.ExecuteNonQuery();
            conn.Close();
        }

        #region Search 

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "searchusers";

                DataTable dt = new DataTable();

                commande.Parameters.Add("@searchstring", SqlDbType.NVarChar).Value = search.Text;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();


                gridUsers.EmptyDataText = "Nici un utilizator gasit";
                gridUsers.DataSource = dt;
                gridUsers.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        #endregion

        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void deleteUser(string idUser)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "deleteuser";

            commande.Parameters.Add("@iduser", SqlDbType.Int).Value = Convert.ToInt32(idUser).ToString();

            conn.Open();
            commande.ExecuteNonQuery();
            conn.Close();
        }

        protected void btn_UserActivity_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }
    }


}