<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bibliotheque.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
     <asp:Table ID="TableLogin" runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Username: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="username" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                    <asp:Label runat="server" Text="Password: "></asp:Label>
            </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="password" TextMode="password" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>               
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                <asp:Button ID="ButtonLogin" runat="server" Text="Connect" OnClick="ButtonLogin_Click"  Cssclass="btn btn-primary" />
            </asp:TableCell>
        </asp:TableRow>
                            
    </asp:Table>
</asp:Content>


