<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BarcodeGenerator.aspx.cs" Inherits="Bibliotheque.BarcodeGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
     <%--<script type="text/javascript">
      function PrintDiv() {
          var divContents = document.getElementById("plBarCode").innerHTML;
            var printWindow = window.open('', '', 'height=100,width=50');
            printWindow.document.write(divContents);
            printWindow.document.close();
            printWindow.print();
        }
    </script>  --%>
    <div style="padding-left:200px">
        <asp:Label ID="lblBarcode" runat="server" Text="Textul pentru codul de bare"></asp:Label>
        <asp:TextBox ID="txtTextbarcode" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
        <%--<asp:Label ID="lblAdditionalText" runat="server" Text="Additional text"></asp:Label>
        <asp:TextBox ID="txtAdditional" runat="server" ></asp:TextBox>--%>
        <asp:Button ID="btnGenerate" runat="server" Text="Generează" onclick="btnGenerate_Click" CssClass="btn btn-primary"/>
        <asp:Button ID="btnClear" runat="server" Text="Șterge" onClick="btnClear_Click" CssClass="btn btn-primary" />
        <br />
        <%--<hr  aria-orientation="horizontal" style="align-content:inherit; width=20%;"  />--%>
        <asp:PlaceHolder ID="plBarCode" runat="server" />
        <%--<asp:Button ID="btnSaveBarcode" runat="server" Text="Save barcode" OnClick="btnSaveBarcode_Click"/>--%>
         <%-- <asp:Button ID="btnPrintCodeBar" runat="server" Text="Printează codul de bare" OnClientClick="PrintDiv()" CssClass="btn btn-primary" />--%>
    </div>
   
</asp:Content>
