<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bon.aspx.cs" Inherits="Bibliotheque.Bon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>BON DE ÎMPRUMUT</title>
    <link rel="stylesheet" type="text/css" href="css/print.css">
</head>
<body>
    <div id="divMessage" style="text-align:center; color:red;" runat="server">
        <label id="lblMessage" runat="server" ></label>
    </div>
    <div id="divUserInformation" style="font-size:smaller;" runat="server">
       
     <div style="text-align:left; padding-left:2em;" runat="server">
        <asp:Label runat="server" Text="BON DE ÎMPRUMUT" CssClass="" Font-Bold="True" Font-Size="Medium"></asp:Label> <br /> 
       <b>Bibliotecar: </b><asp:Label ID="lblAdmin" runat="server"></asp:Label><br />
       <b>Cititor: </b><asp:Label ID="lblInfoUder" runat="server"></asp:Label><br />
       <b>Data Curentă: </b><asp:Label ID="lblDate" runat="server"></asp:Label><br />
       
       
     </div>  
  
    
    <div id="divBorrowedBooks" runat="server" style="text-align:left;  padding-left:2em;">
        <asp:Repeater ID="rptrBooks" runat="server">
            <HeaderTemplate>
        <table cellspacing="0" rules="all" border="0" >
            <tr>
                 <th scope="col" style="width: 140px;padding-left:12px;">
                    Cod Carte
                </th>
                <th scope="col" style="width: 380px;padding-left:12px;">
                    Titlul
                </th>
                <th scope="col" style="width: 220px; padding-left:12px;">
                    Autor
                </th>
                <th scope="col" style="width: 140px; padding-left:12px;">
                    Data împrumutului
                </th>
                <th scope="col" style="width: 140px; padding-left:12px;">
                    Data returnării
                </th>
            </tr>
    </HeaderTemplate>
            <ItemTemplate>
        <tr >
            <td style="padding-left:12px;">
                <asp:Label ID="lblCodeCarte" runat="server" Text='<%# Eval("initialcode") %>' />
            </td>
            <td style="padding-left:12px;">
                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>' />
            </td>
            <td style="padding-left:12px;">
                <asp:Label ID="lblAutor" runat="server" Text='<%# Eval("author") %>' />
            </td>
            <td style="padding-left:12px;">
                <asp:Label ID="lblborrowdate" runat="server" Text='<%# Eval("creationdate","{0:dd/MMMM/yyyy}") %>' />
            </td>
            <td style="padding-left:12px;">
                <asp:Label ID="lbldatareturnarii" runat="server" Text='<%# Eval("returndate","{0:dd/MMMM/yyyy}") %>' />
            </td>
        </tr>
    </ItemTemplate>
        </asp:Repeater>
        </div>
    </div>
    <br />
    <div id="divButtons" style="padding-left: 20px" runat="server">
        <form id="form1" runat="server">
            <asp:Button ID="btnPrint" CssClass="btn btn-primary " runat="server" OnClientClick=" Hidebuttons(); PrintDiv() " Text="Print"/>
            <asp:Button ID="btnRevenire" CssClass="btn btn-primary" OnClick="btnRevenire_Click" Visible="true" runat="server" Text="Revenire la pagina de imprumut carti"/>
            <asp:Button ID="btnRevenireReturn" CssClass="btn btn-primary" OnClick="btnRevenireReturn_Click" Visible="true" runat="server" Text="Revenire la pagina de returnare carti"/>
        </form>
    </div>
    
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("divUserInformation").innerHTML;
            var printWindow = window.open('', '', 'height=450,width=1000');
            printWindow.document.write('<html><head><title>Bon de împrumut</title>');
            printWindow.document.write('<link rel="stylesheet" type="text/css" href="css/print.css">');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
    <script type="text/javascript">
        function Hidebuttons() {
            document.getElementById("divButtons").style.visibility = "hidden";
        }
    </script>

</body>
</html>
 