using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace Bibliotheque
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnViewReport_Click(Object sender, EventArgs e)
        {
            try
            {
                rvCatalog.Visible = true;
                if (ddlReportType.SelectedValue == "RepCategories")
                {
                    rvCatalog.LocalReport.ReportPath = "CatalogCarti.rdlc";
                    rvCatalog.LocalReport.Refresh();
                    ViewBooksCatalog();
                }
                
                 else if (ddlReportType.SelectedValue == "RepOnCategories")
                    {
                        rvCatalog.LocalReport.ReportPath = "ReportCartiCategorie.rdlc";
                        rvCatalog.LocalReport.Refresh();
                        ViewBooksOnCategories();
                    }
                else if(ddlReportType.SelectedValue == "RepTheMostBorrowedBook")
                    {
                    rvCatalog.LocalReport.ReportPath = "ReportCarteaCeaMaiImprumutata.rdlc";
                    rvCatalog.LocalReport.Refresh();
                    ViewMostBorrowedBooks();

                }

                else if(ddlReportType.SelectedValue == "RepMTheMostActiveUser")
                {
                    rvCatalog.LocalReport.ReportPath = "ReportCelMaiActivUtilizator.rdlc";
                    rvCatalog.LocalReport.Refresh();
                    ViewMostActiveUser();

                }
                else if(ddlReportType.SelectedValue == "RepAllBorrowedBooksByUser")
                {
                    rvCatalog.LocalReport.ReportPath = "ReportToateCartileImprumutatePeUtilizator.rdlc";
                    rvCatalog.LocalReport.Refresh();
                    ViewAllBooksBorrowedByUser();
                }

                else if (ddlReportType.SelectedValue == "RepUserByClass")
                {
                    rvCatalog.LocalReport.ReportPath = "ReportUsersByClass.rdlc";
                    rvCatalog.LocalReport.Refresh();
                    ViewAllUserByClass();
                }

                 else if (ddlReportType.SelectedValue == "RepCartiCasate")
                {
                    rvCatalog.LocalReport.ReportPath = "Raport_CasareCarti.rdlc";
                    rvCatalog.LocalReport.Refresh();
                   ViewCartiCasate();
                }
                else { throw new Exception("Raportul selectat nu este disponibil."); }
        
    }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
            }
        }

       

        private void ViewBooksCatalog()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            ReportDataSource dsBooks = new ReportDataSource("DataSet_Books", LoadData("Books").Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsBooks);
            rvCatalog.LocalReport.Refresh();
        }


        private void ViewBooksOnCategories()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            string category = ddlBookCategory.SelectedValue.ToString();
            ReportDataSource dsBooks = new ReportDataSource("DataSetRaportPeCategorie", LoadData("Categories", category).Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsBooks);
            rvCatalog.LocalReport.Refresh();

            //ReportParameter[] reportParameterCollection = new ReportParameter[1];
            //reportParameterCollection[0] = new ReportParameter();
            //reportParameterCollection[0].Name = "category";
            //reportParameterCollection[0].Values.Add(category);
            //rvCatalog.LocalReport.SetParameters(reportParameterCollection);
            //rvCatalog.LocalReport.Refresh();
        }

        private void ViewMostBorrowedBooks()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            ReportDataSource dsBooks = new ReportDataSource("DataSetCeaMaiImprumutataCarte", LoadData("MostBorrowed").Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsBooks);
            rvCatalog.LocalReport.Refresh();
        }

        private void ViewMostActiveUser()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            ReportDataSource dsBooks = new ReportDataSource("ReportCelMaiActivUtilizator", LoadData("MostActive").Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsBooks);
            rvCatalog.LocalReport.Refresh();
        }

        private void ViewAllBooksBorrowedByUser()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            string numeutilizator = txtUsername.Text.ToString();
            ReportDataSource dsBooks = new ReportDataSource("DataSetToateCartileImprumutatePeUtilizator", LoadData("AllBorrowed", numeutilizator).Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsBooks);
            rvCatalog.LocalReport.Refresh();

            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "username";
            reportParameterCollection[0].Values.Add(numeutilizator);
            rvCatalog.LocalReport.SetParameters(reportParameterCollection);
            rvCatalog.LocalReport.Refresh();
        }
        private void ViewAllUserByClass()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            string userclass = txtUserClass.Text.ToString();
            ReportDataSource dsUserClass = new ReportDataSource("UserByClass", LoadData("UserByClass", userclass).Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsUserClass);
            rvCatalog.LocalReport.Refresh();

            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "clasa";
            reportParameterCollection[0].Values.Add(userclass);
            rvCatalog.LocalReport.SetParameters(reportParameterCollection);
            rvCatalog.LocalReport.Refresh();

        }
        private void ViewCartiCasate()
        {
            rvCatalog.Visible = true;
            rvCatalog.LocalReport.DataSources.Clear();
            string param = txtDataCasareFrom.Text.ToString();
            string param2 = txtDataCasareTo.Text.ToString();
            
            ReportDataSource dsCartiCasate = new ReportDataSource("DataSetCasare", LoadData("CartiCasate", param, param2).Tables[0]);
            rvCatalog.LocalReport.DataSources.Add(dsCartiCasate);
            rvCatalog.LocalReport.Refresh();

            ReportParameter[] reportParameterCollection = new ReportParameter[2];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "DataCasareFrom";
            reportParameterCollection[0].Values.Add(param);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "DataCasareTo";
            reportParameterCollection[1].Values.Add(param2);
            rvCatalog.LocalReport.SetParameters(reportParameterCollection);
            rvCatalog.LocalReport.Refresh();
        }
        private DataSet LoadData(string sqlType, string param = "", String param2 = "")
        {
            DataSet ds = new DataSet();
            string connString = Util.getConnectionString();
            string sqlString = "";
            switch (sqlType)
            {
                case "Books":
                    sqlString = "getbooks";
                    break;
                case "Borrows":
                    sqlString = "getbooksborrowedhistory";
                    break;
                case "Categories":
                    sqlString = "cat_getbooks '" + param + "'";
                    break;
                case "MostBorrowed":
                    sqlString = "rep_mostusedbook";
                    break;
                case "MostActive":
                    sqlString = "rep_mostactiveuser";
                    break;
                case "AllBorrowed":
                    sqlString = "rep_getallborrowedbooksbyusername '" + param + "'";
                    break;
                case "UserByClass":
                    sqlString = "rep_getbooksperclasa '" + param + "'";
                    break;
                case "DistinctCategories":
                    sqlString = "rep_getCategories";
                    break;
               case "CartiCasate":
                   sqlString = "rep_carticasate '" + param + "','" + param2 + "'";
                   break;
            }
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand (sqlString, conn);
                conn.Open();
                adapter.Fill(ds);
                conn.Close();
            }
            return ds;
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)(Master.FindControl("lblMessage"))).Text = "";
            rvCatalog.Visible = false;
            if (ddlReportType.SelectedValue == "RepOnCategories")
            {
                ddlBookCategory.Visible = true;
                ddlBookCategory.DataSource = LoadData("DistinctCategories").Tables[0];
                ddlBookCategory.DataValueField = "category";
                ddlBookCategory.DataTextField = "category";
                ddlBookCategory.DataBind();
            }
            else
            {
                ddlBookCategory.Visible = false;
            }

            if(ddlReportType.SelectedValue == "RepAllBorrowedBooksByUser")
            {
                lblUsername.Visible = true;
                txtUsername.Visible = true;
            }
            else {

                lblUsername.Visible = false;
                txtUsername.Visible = false;
            }

            if (ddlReportType.SelectedValue == "RepUserByClass")
            {
                lblUserClass.Visible = true;
                txtUserClass.Visible = true;
            }
            else
            {

                lblUserClass.Visible = false;
                txtUserClass.Visible = false;
            }
            if (ddlReportType.SelectedValue == "RepCartiCasate")
            {
                lblDataCasareFrom.Visible = true;
                txtDataCasareFrom.Visible = true;

                lblDataCasareTo.Visible = true;
                txtDataCasareTo.Visible = true;
            }
            else
            {
                lblDataCasareFrom.Visible = false;
                txtDataCasareFrom.Visible = false;

                lblDataCasareTo.Visible = false;
                txtDataCasareTo.Visible = false;
            }
        }
    }
}