using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;



namespace Bibliotheque
{
    public class Util
    {
        public static string getConnectionString()
        {

            System.Configuration.Configuration rootWebConfiguration =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Bibliotheque");
            ConnectionStringSettings connString = null;

            if (rootWebConfiguration.ConnectionStrings.ConnectionStrings.Count > 0)
            {

                connString = rootWebConfiguration.ConnectionStrings.ConnectionStrings["connectionbiblio"];

                if (connString != null)

                    Console.WriteLine("SQL connection string = \"{0}\"",
                    connString.ConnectionString);

                else
                    Console.WriteLine("No SQL connection string");

            }

            return connString.ConnectionString;
        }

        public static string EncryptPassword(string password)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytPassword = uEncode.GetBytes(password);
            SHA512Managed sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(bytPassword);
            return Convert.ToBase64String(hash);
        }

        public static void Logout()
        {
         HttpContext.Current.Session.Abandon();  
        }

        public static string RemoveID(string array, int id)
       {
            string arraySansId ="";
            string[] valArray = array.Split(',');
            foreach (string item in valArray) {
                if (item != id.ToString())
                {
                    arraySansId += item + ',';
                }
            }
            //remove last ,
            if (arraySansId.Contains(','))
            { arraySansId = arraySansId.Substring(0, arraySansId.Length - 1); }

            return arraySansId;
        }

        public static void generateBarcode(string text, PlaceHolder placeholder)
        {
            string barCode = text;
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 18);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*" , oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                placeholder.Controls.Add(imgBarCode);

            }
        }
    }
}