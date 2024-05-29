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
    public partial class RegisterBiblio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        string strConnection = Util.getConnectionString();

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.CommandText = "enregistreruser";
                commande.Parameters.Add("@username", SqlDbType.NVarChar).Value = username.Text;
                commande.Parameters.Add("@name", SqlDbType.NVarChar).Value = name.Text;
                commande.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname.Text;
                commande.Parameters.Add("@clasa", SqlDbType.NVarChar).Value = clasa.Text;

                if (password.Text != confirmpassword.Text)
                {
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Parola nu este identica ";
                   ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                }

                string pass = Util.EncryptPassword(password.Text);
                commande.Parameters.Add("@pwsd", SqlDbType.NVarChar).Value = pass;

                commande.Parameters.Add("@rol", SqlDbType.NVarChar).Value = "user";


                conn.Open();

                commande.ExecuteNonQuery();
                conn.Close();
               ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                ((Label)(Master.FindControl("lblMessage"))).Text = "Utilizator inregistrat cu succes !";

                username.Text = ""; name.Text = ""; surname.Text = "";clasa.Text = ""; password.Text = ""; confirmpassword.Text = "";


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = "utilizatorul nu a fost inregistrat !";
            }
            //if (!Session.SessionID.Equals(null))
            //{
            //    ((Label)(Master.FindControl("lblRole"))).Text = (Session["role"] != null ? Session["role"].ToString() : "");
            //    ((Label)(Master.FindControl("lblSession"))).Text = (Session["username"] != null ? Session["username"].ToString() : "");

            //}
        }
    }
    }
