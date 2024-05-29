<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Bibliotheque.Reports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Button id="btnViewReport" runat="server" Text="Vizualizare" OnClick="btnViewReport_Click"/>

    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" 
        OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" >
        <asp:ListItem Value="RepCategories" Text="Books catalogue" Selected="True"></asp:ListItem>
        <asp:ListItem Value="RepOnCategories" Text="Report by categories"></asp:ListItem>
        <asp:ListItem Value="RepTheMostBorrowedBook" Text="Report by the most borrowed book"></asp:ListItem>
        <asp:ListItem Value="RepMTheMostActiveUser" Text="Report by the most active  user"></asp:ListItem>
        <asp:ListItem Value="RepAllBorrowedBooksByUser" Text="Report by the all book borrowed by the user"></asp:ListItem>
        <asp:ListItem Value="RepUserByClass" Text="report by the boorrowed book by class"></asp:ListItem>
        <asp:ListItem Value="RepCartiCasate" Text="Report by archived books" ></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlBookCategory" runat="server" class="selectpicker"  Visible="false">
    </asp:DropDownList>
    <asp:Label ID="lblUsername" runat="server" Visible="false">Username</asp:Label>
    <asp:TextBox ID="txtUsername" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lblUserClass" runat="server" Visible="false">Class</asp:Label>
    <asp:TextBox ID="txtUserClass" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lblDataCasareFrom" runat="server" Text="From"  Visible="false"></asp:Label>
    <asp:TextBox ID="txtDataCasareFrom" runat="server" placeholder="Year/Month/Day"  Visible="false"></asp:TextBox>
    <asp:Label ID="lblDataCasareTo" runat="server" Text="To" Visible="false" ></asp:Label>
    <asp:TextBox ID="txtDataCasareTo" runat="server" Visible="false" placeholder="Year/Month/Day" ></asp:TextBox>
    <rsweb:ReportViewer ID="rvCatalog" runat="server" Width="100%" Height="500px" Visible="false">
        <LocalReport ReportPath="Catalog.rdlc">
            <DataSources>
                <rsweb:ReportDataSource />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

</asp:Content>
