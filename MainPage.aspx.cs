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
    public partial class MainPage : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                if (!(Page.IsPostBack))
                {
                    //bind also the categories used until now
                    ddlBookCategory.DataSource = GetUsedCategories();
                    ddlBookCategory.DataTextField = "category";
                    ddlBookCategory.DataValueField = "categoryValue";
                    ddlBookCategory.DataBind();
                }
                getallBooks();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBooks();
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

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid1.PageIndex = e.NewPageIndex;
            getallBooks();
        }

        private DataTable GetUsedCategories()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "getUsedCategories";
            commande.Connection = conn;

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(dt);
            conn.Close();

            return dt;
        }

        private void SearchBooks()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "searchlivre";
                commande.Connection = conn;
                DataTable dt = new DataTable();

                commande.Parameters.Add("@searchString", SqlDbType.NVarChar).Value = search.Text;
                int av = 0; // also for chkBorrowed.checked
                if (chkAvailable.Checked) { av = 1; }
                if (chkAvailable.Checked && chkBorrowed.Checked) { av = 2; }

                commande.Parameters.Add("@availability", SqlDbType.Int).Value = av;
                commande.Parameters.Add("@category", SqlDbType.NVarChar).Value = ddlBookCategory.SelectedValue;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                conn.Close();
                grid1.EmptyDataText = "Nicio carte gasita";
                grid1.DataSource = dt;
                grid1.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private void getallBooks()
        {
            if (ddlBookCategory.SelectedIndex != 0)
            {
                SearchBooks();
            }
            else { 
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getallbooks";
                DataTable dt = new DataTable();

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                commande.ExecuteReader();
                conn.Close();

                grid1.EmptyDataText = "Nicio carte gasita";
                grid1.PageSize = 7;
                grid1.DataSource = dt;
                grid1.DataBind();
            }
        }

    }
}
