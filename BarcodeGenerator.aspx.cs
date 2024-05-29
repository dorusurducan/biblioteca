using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Bibliotheque
{
    public partial class BarcodeGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnGenerate.UniqueID;
            txtTextbarcode.Focus();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Util.generateBarcode(txtTextbarcode.Text, plBarCode);
            }
            catch(Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
             
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTextbarcode.Text = ""; 
        }
    }
}