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
    public partial class NewBooks : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSearch.UniqueID;
            search.Focus();
            //for this page, if there isn't any user loged is, we still show the login
            if (Session["role"] == null) {
                (Master.FindControl("menuLogin")).Visible = true;
            }
            else {
                if (Session["role"].Equals("")) {
                    (Master.FindControl("menuLogin")).Visible = true;
                }
            }

            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getallnewbooks";
                DataTable dt = new DataTable();

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();

                grid1.DataSource = dt;
                grid1.DataBind();

                if (dt.Rows.Count == 0)
                {
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Nicio carte gasita";
                }
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "searchallnewbooks";
                commande.Connection = conn;
                DataTable dt = new DataTable();

                commande.Parameters.Add("@searchnewbook", SqlDbType.NVarChar).Value = search.Text.ToString();

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image img = ((Image)e.Row.FindControl("image"));
                if ((img == null) || (img.DescriptionUrl == ""))
                {
                    img.Visible = false;
                }
            }
        }
    }
}

