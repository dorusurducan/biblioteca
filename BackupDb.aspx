<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BackupDb.aspx.cs" Inherits="Bibliotheque.BackupDb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div style="text-align:center">
        <label>Puteți face backup la baza de date apăsând acest buton</label>
        <asp:Button ID="btnBackup" Text="Backup" runat="server" OnClick="btnBackup_Click" CssClass="btn btn-primary"/>
    </div>
</asp:Content>
