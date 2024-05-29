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
    public partial class Users : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Form.DefaultButton = this.btnSearch.UniqueID;
                txtSearchUser.Focus();

                if (!(Page.IsPostBack))
                {
                    getAllRecords();
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
            SearchHistory();
        }

        private void SearchHistory()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "getbooksborrowedhistory";
                commande.Connection = conn;
                DataTable dt = new DataTable();

                commande.Parameters.Add("@iduser", SqlDbType.Int).Value = 0;
                commande.Parameters.Add("@searchUser", SqlDbType.NVarChar).Value = txtSearchUser.Text;
                commande.Parameters.Add("@searchBook", SqlDbType.NVarChar).Value = txtSearchBook.Text;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                conn.Close();
                gridBorrowActivity.EmptyDataText = "Nicio inregistrare gasita";
                gridBorrowActivity.DataSource = dt;
                gridBorrowActivity.DataBind();
                lblMessage.Text = "Nr. inregistrari: " + dt.Rows.Count;
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        protected void gridview_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBorrowActivity.PageIndex = e.NewPageIndex;
            getAllRecords();
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

        private void getAllRecords()
        {
            if ((txtSearchUser.Text != "") || (txtSearchBook.Text != ""))
            {
                SearchHistory();
            }
            else
            {
                if (Session["role"] != null)
                {
                    SqlConnection conn = new SqlConnection(strConnection);
                    SqlCommand commande = new SqlCommand();
                    DataTable dt = new DataTable();
                    commande.Connection = conn;
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.CommandText = "getbooksborrowedhistory";
                    if (Session["role"].ToString() == "user")
                    {
                        commande.Parameters.Add("@iduser", SqlDbType.Int).Value = Session["iduser"];
                        divSearch.Visible = false;
                    }
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(commande);
                    da.Fill(dt);
                    commande.ExecuteReader();
                    conn.Close();
                    gridBorrowActivity.EmptyDataText = "No records found";
                    gridBorrowActivity.DataSource = dt;
                    gridBorrowActivity.DataBind();
                    lblMessage.Text = "Nr. inregistrari: " + dt.Rows.Count;
                }
            }
        }

       
    }
}