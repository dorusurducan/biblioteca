using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Bibliotheque
{
    /// <summary>
    /// Description résumée de ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strConnection = Util.getConnectionString();
            string IdPhoto = context.Request.QueryString["IdLivre"];

            if (!string.IsNullOrEmpty(IdPhoto))
            {
                using (SqlConnection conn = new SqlConnection(strConnection))
                {
                    conn.Open();
                    SqlCommand commande = new SqlCommand("EXECUTE getPhoto " + IdPhoto, conn);
                    SqlDataReader dr = commande.ExecuteReader();
                    dr.Read();
                    if (!(dr[0].ToString() == "")) { context.Response.BinaryWrite((byte[])dr[0]);  }
                    conn.Close();
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}