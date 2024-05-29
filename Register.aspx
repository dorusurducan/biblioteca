<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Bibliotheque.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Name"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="name" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Surname"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="surname" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Username"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="username" runat="server" CssClass="form-control" ></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Class"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="clasa" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Password"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="password" TextMode="password" runat="server" CssClass="form-control" ></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                     <asp:Label runat="server" Text="Confirm the password"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="confirmpassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Role"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="usersrols" runat="server" class="selectpicker">
                        <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                        <asp:ListItem Text="Librarian" Value="librarian"></asp:ListItem>
                        <asp:ListItem Text="User" Value="user" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:Button ID="register" runat="server" Text="Inregistreaza" CssClass="btn btn-primary"
                        OnClick="ButtonRegister_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</asp:Content>
