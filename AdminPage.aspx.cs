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
    public partial class AdminPage : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Form.DefaultButton = this.btnSearch.UniqueID;
                search.Focus();
                if (!(Page.IsPostBack)){
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
                    gridUsers.EmptyDataText = "No Records Found";
                    gridUsers.DataSource = dt;
                    gridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_UserActivity_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }

#region DataGridView Manipulation

        protected void Btn_CreateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx", false);
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
                    DropDownList ddlRol = (DropDownList)gvr.FindControl("ddlRol");

                    UpdateUser(lblId.Text, txtUsername.Text, txtName.Text, txtSurname.Text, txtClasa.Text, ddlRol.SelectedValue);
                    gridUsers.EditIndex = -1;
                    GridViewBind();
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Utilizatorul a fost adaugat!";
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
                    { throw new Exception("Cele 2 parole trebuie sa fie identice"); }

                    //update in sql server
                    UpdatePassword(lblId.Text, txtPassword.Text);
                    gridUsers.EditIndex = -1;
                    GridViewBind();
                    //show message "the password was updated"
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Parola a fost modificata!";
                }
                else if (e.CommandName == "Delete")
                {
                    if (Session["role"].Equals("admin")) {
                        GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                        Label lblId = (Label)gvr.FindControl("lblIdUser");

                        DeleteUser(lblId.Text);
                        gridUsers.EditIndex = -1;
                        GridViewBind();
                        ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                        ((Label)(Master.FindControl("lblMessage"))).Text = "Utilizatorul a fost sters!";
                    }
                    else
                    {
                        ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                        ((Label)(Master.FindControl("lblMessage"))).Text = "Mu aveti voie sa stergeti user!";
                    }
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
        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
#endregion
#region Data functions

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


            gridUsers.EmptyDataText = "No Records Found";
            gridUsers.DataSource = dt;
            gridUsers.DataBind();
        }
        private void UpdateUser(string iduser,string username, string name, string surname, string clasa,string rol)
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
            commande.Parameters.Add("@rol", SqlDbType.NVarChar).Value = rol.ToString();

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

        private void DeleteUser(string iduser)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "deleteuser";

            commande.Parameters.Add("@iduser", SqlDbType.Int).Value = Convert.ToInt32(iduser).ToString();

            conn.Open();
            commande.ExecuteNonQuery();
            conn.Close();
        }
#endregion
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

                gridUsers.EmptyDataText = "No Records Found";
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


    }

}