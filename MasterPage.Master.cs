using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bibliotheque
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Session.SessionID.Equals(null))
            {
                menuLogout.Visible = false;
                menuListbooks.Visible = false;
               
                menuBiblio.Visible = false;
                menuLoanBook.Visible = false;
                menuReturnBook.Visible = false;
                menuAdmin.Visible = false;
                menuBooks.Visible = false;
                menuUsers.Visible = false;
                menuCasareCarti.Visible = false;
                menuBackup.Visible = false;
                menuRaports.Visible = false;
                menuBarcodeGenerator.Visible = false;

                if (Session["username"] != null || Session["role"] != null)
                {
                    lblSession.Text = Session["role"].ToString() + ": " + Session["username"].ToString();

                    if (Session["role"].Equals("admin"))
                    {
                        menuLogin.Visible = false;
                        menuLogout.Visible = true;
                        menuListbooks.Visible = true;
                       
                        menuLoanBook.Visible = true;
                        menuReturnBook.Visible = true;
                        menuAdmin.Visible = true;
                        menuBackup.Visible = true;
                        menuRaports.Visible = true;
                        menuBooks.Visible = true;
                        menuBarcodeGenerator.Visible = true;
                        menuUsers.Visible = true;
                        menuCasareCarti.Visible = true;

                    }
                    else if (Session["role"].Equals("librarian"))
                    {
                        menuLogin.Visible = false;
                        menuLogout.Visible = true;
                        menuListbooks.Visible = true;
                        
                        menuLoanBook.Visible = true;
                        menuReturnBook.Visible = true;
                        menuBiblio.Visible = true;
                        menuBackup.Visible = true;
                        menuRaports.Visible = true;
                        menuBooks.Visible = true;
                        menuBarcodeGenerator.Visible = true;
                        menuUsers.Visible = true;
                        menuCasareCarti.Visible = true;
                    }
                    else if (Session["role"].Equals("user"))
                    {
                        menuLogin.Visible = false;
                        menuLogout.Visible = true;
                        menuListbooks.Visible = true;
                        
                        menuUsers.Visible = true;
                    }
                }
                else
                {
                    //we show these pages, even if isn't any user logged in
                    if (menuLogin.Visible)
                    { 
                        menuListbooks.Visible = true;
                        
                    }
                }
            }
        }
    }
}
    