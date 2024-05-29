using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Bibliotheque
{
    public partial class Books : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (!(Page.IsPostBack))
                {
                    getBooks();
                    //bind also the categories used until now
                    ddlBookCategory.DataSource = GetUsedCategories();
                    ddlBookCategory.DataTextField = "category";
                    ddlBookCategory.DataValueField = "categoryValue";
                    ddlBookCategory.DataBind();
                }
                this.Form.DefaultButton = this.btnSearch.UniqueID;
                search.Focus();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

#region Controls Functions

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBooks();
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

                if(av.Equals("true"))
                commande.Parameters.Add("@availability", SqlDbType.Int).Value = av;
                commande.Parameters.Add("@category", SqlDbType.NVarChar).Value = ddlBookCategory.SelectedValue;

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(dt);
                conn.Close();
                gridBooks.EmptyDataText = "Nicio carte gasita";
                gridBooks.DataSource = dt;
                gridBooks.DataBind();
                lblMessage.Text = "Nr. carti: " + dt.Rows.Count;
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
                if (e.CommandName == "Modify")
                {
                    gridBooks.EditIndex = Convert.ToInt32(e.CommandArgument);
                    if (search.Text != "")
                    {
                        SearchBooks();
                    }
                    else
                    {
                        getBooks();
                    }
                }
                else if (e.CommandName == "Update")
                {
                    GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    Label lblIdBook = (Label)gvr.FindControl("lblIdBook");
                    TextBox txtCodeISBN = (TextBox)gvr.FindControl("txtcodeISBNEdit");
                    TextBox txtInitialCod = (TextBox)gvr.FindControl("txtInitialCode");
                    TextBox txtTitle = (TextBox)gvr.FindControl("txtTitle");
                    TextBox txtAuthor = (TextBox)gvr.FindControl("txtAuthor");
                    TextBox txtDescription = (TextBox)gvr.FindControl("txtDescription");
                    DropDownList ddlCategory = (DropDownList)gvr.FindControl("ddlCategory");
                    TextBox txtExtraInfo = (TextBox)gvr.FindControl("txtExtrainfo");
                    TextBox txtPrice = (TextBox)gvr.FindControl("txtPrice");
                    TextBox txtPages = (TextBox)gvr.FindControl("txtPages");
                    TextBox txtAvailability = (TextBox)gvr.FindControl("txtAvailability");

                    updateBook(lblIdBook.Text, txtCodeISBN.Text,
                        txtInitialCod.Text, txtTitle.Text, txtAuthor.Text, txtDescription.Text,
                        ddlCategory.SelectedValue.ToString(), txtExtraInfo.Text, txtPrice.Text, txtPages.Text);

                    SqlConnection conn = new SqlConnection(strConnection);
                    SqlCommand commande = new SqlCommand();

                    commande.Connection = conn;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;
                    commande.CommandText = "updateImageBook";

                    commande.Parameters.Add("@idlivre", SqlDbType.Int).Value = Convert.ToInt32(lblIdBook.Text).ToString();
                    FileUpload fuBook = (FileUpload)gvr.FindControl("FileUpload");
                    if (fuBook.FileName != "")
                    {
                        Stream fs = fuBook.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        commande.Parameters.Add("@photo", System.Data.SqlDbType.VarBinary).Value = bytes;

                        conn.Open();
                        commande.ExecuteNonQuery();
                        conn.Close();
                    }
                    gridBooks.EditIndex = -1;
                    if (search.Text != "")
                    {
                        SearchBooks();
                    }
                    else
                    {
                        getBooks();
                    }
                    ((Label)(Master.FindControl("lblMessage"))).Text = "Carte actualizata";
                    ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                }
                else if (e.CommandName == "CancelUpdate")
                {
                    gridBooks.EditIndex = -1;
                    if (search.Text != "")
                    {
                        SearchBooks();
                    }
                    else
                    {
                        getBooks();
                    }
                }

                else if (e.CommandName == "SendToCasare")
                {
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    if (hfCarteCasata.Value == "")
                    {
                        hfCarteCasata.Value = ((Label)(row.FindControl("lblIdBook"))).Text;
                        Response.Redirect("CasareCarti.aspx?CarteCasata=" + hfCarteCasata.Value);
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
            gridBooks.PageIndex = e.NewPageIndex;
            getBooks();

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

        protected void gridBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                String idlivre = ((Label)gridBooks.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
                deleteBook(Convert.ToInt32(idlivre));
                if (search.Text != "")
                {
                    SearchBooks();
                }
                else
                {
                    getBooks();
                }
                ((Label)(Master.FindControl("lblMessage"))).Text = "Carte stearsa (" + idlivre + ")";
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private void updateBook(string codBook, string codISBN, string codeInitial, string title, string author, string description, string category, string extrainfo, string price, string pages)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            try
            {
                commande.Connection = conn;
                commande.CommandType = CommandType.StoredProcedure;
                commande.CommandText = "updatebook";

                commande.Parameters.Add("@idlivre", SqlDbType.Int).Value = Convert.ToInt32(codBook).ToString();
                commande.Parameters.Add("@codeISBN", SqlDbType.NVarChar).Value = codISBN.ToString();
                commande.Parameters.Add("@initialcode", SqlDbType.Int).Value = Convert.ToInt32(codeInitial).ToString();
                commande.Parameters.Add("@title", SqlDbType.NVarChar).Value = title.ToString();
                commande.Parameters.Add("@author", SqlDbType.NVarChar).Value = author.ToString();
                commande.Parameters.Add("@description", SqlDbType.NVarChar).Value = description.ToString();
                commande.Parameters.Add("@category", SqlDbType.NVarChar).Value = category.ToString();
                commande.Parameters.Add("@extrainfo", SqlDbType.NVarChar).Value = extrainfo.ToString();
                commande.Parameters.Add("@price", SqlDbType.Decimal).Value = Convert.ToDecimal(price).ToString();
                commande.Parameters.Add("@pages", SqlDbType.Int).Value = Convert.ToInt32(pages).ToString();

                conn.Open();
                commande.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }

        }

        #endregion

        #region Data Functions

        private void getBooks()
        {
            try
            {
                if (ddlBookCategory.SelectedIndex != 0) {
                    SearchBooks();
                }
                else
                { 
                    SqlConnection conn = new SqlConnection(strConnection);
                    SqlCommand commande = new SqlCommand();

                    commande.Connection = conn;
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.CommandText = "getbooks";
                    DataTable dt = new DataTable();

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(commande);
                    da.Fill(dt);
                    commande.ExecuteReader();
                    conn.Close();

                    gridBooks.EmptyDataText = "Nicio carte gasita";
                    gridBooks.PageSize = 7;
                    gridBooks.DataSource = dt;
                    gridBooks.DataBind();
                    lblMessage.Text = "Nr. carti: " + dt.Rows.Count;

                }
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
        }

        private void deleteBook(int idbook)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();

            commande.Connection = conn;
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "deletebook";

            commande.Parameters.Add("@idlivre", SqlDbType.Int).Value = Convert.ToInt32(idbook).ToString();

            conn.Open();
            commande.ExecuteNonQuery();
            conn.Close();
        }

        protected void gridBooks_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (img != null)
                {
                    if (img.DescriptionUrl == "")
                    {
                        img.Visible = false;
                    }
                }
            }
        }

        private DataTable GetUsedCategories(bool withAll = true)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand commande = new SqlCommand();
            commande.CommandType = CommandType.StoredProcedure;
            commande.CommandText = "getUsedCategories";
            commande.Connection = conn;

            commande.Parameters.Add("@withAll", System.Data.SqlDbType.Bit).Value = withAll;

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(dt);
            conn.Close();

            return dt;
        }

        #endregion

    }
}