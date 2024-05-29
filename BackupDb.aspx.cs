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
    public partial class BackupDb : System.Web.UI.Page
    {
        string strConnection = Util.getConnectionString();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnection);
                SqlCommand commande = new SqlCommand();

                string fileName = DateTime.Now.ToString("BackupBiblio_dd/MMMM/yyyy") + "_" + DateTime.Now.ToString("HHmm");
                string directory =HttpContext.Current.Server.MapPath("~/") + "backups\\";
                string filePath = directory + fileName + ".bak";

                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.CommandText = "backupDatabase";
                commande.Parameters.Add("@path", SqlDbType.NVarChar).Value = filePath;

                conn.Open();
                commande.ExecuteNonQuery();
                conn.Close();
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Blue;
                ((Label)(Master.FindControl("lblMessage"))).Text = "The file generated is: " + filePath;
            }
            catch (Exception ex)
            {
                ((Label)(Master.FindControl("lblMessage"))).ForeColor = System.Drawing.Color.Red;
                ((Label)(Master.FindControl("lblMessage"))).Text = ex.Message;
            }
            
        }
    }
}