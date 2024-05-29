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
    public partial class Login : System.Web.UI.Page
    {
        string connectionStr = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.ButtonLogin.UniqueID;
            username.Focus();
            //for this page, if there isn't any user loged is, we still show the login
            if (Session["role"] == null) {
                (Master.FindControl("menuLogin")).Visible = true;
            }
            else {
                if (Session["role"].Equals("")) {
                    (Master.FindControl("menuLogin")).Visible = true;
                }
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.CommandText = "loginUser";
                commande.Connection = conn;
                commande.Parameters.Add("@username", SqlDbType.NVarChar).Value = username.Text;

                string pass = Util.EncryptPassword(password.Text);

                commande.Parameters.Add("@password", SqlDbType.NVarChar).Value = pass;

                conn.Open();

                string role = (string)commande.ExecuteScalar();

                conn.Close();

                Session["username"] = username.Text;
                Session["role"] = role;
                Session["iduser"] = getUserId(username.Text);

                ((Label)(Master.FindControl("lblMessage"))).Text = "Nu esti conectat";
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                if ((password.Text == "") || (username.Text == ""))
                {
                    Session.Remove("username");
                    Session.Remove("role");
                    Session.Remove("iduser");
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Introduceti datele!";
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                }else
                {
                    if (role.Equals("admin"))
                    {
                        Response.Redirect("AdminPage.aspx", false);
                    }
                    else if (role.Equals("librarian"))
                    {
                        Response.Redirect("Biblio.aspx", false);
                    }
                    else if (role.Equals("user"))
                    {
                        Response.Redirect("Users.aspx", false);
                    }
                    else
                    {
                        ((Label)(Master.FindControl("lblMessage"))).Text = "Conectare nereusita";
                        ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                        Response.Redirect("Login.aspx", false);
                    }
                }
               
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).Text = "Utilizator inexistent (" + ex.Message + ")";
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
            }
        }

        protected int getUserId(string username)
        {

            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "getuserid";
            SqlDataAdapter da = new SqlDataAdapter(commande);

            commande.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

            conn.Open();
            int userid = Convert.ToInt32(commande.ExecuteScalar());
            conn.Close();

            return userid;
        }
    }
}