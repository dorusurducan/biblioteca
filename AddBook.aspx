<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="Bibliotheque.AddBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
     <asp:Table ID="TableAddBook" runat="server" HorizontalAlign="Center" >
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelcodeISBN" runat="server" Text="Label">Cod înregistrare</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxCodeISBN" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelInitialCode" runat="server" Text="Label">Cod initial</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxInitialCode" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    *
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelTitle" runat="server" Text="Label">Titlu</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    *
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelAuthor" runat="server" Text="Label">Autor</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxAuthor" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    *
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelDescription" runat="server" Text="Label">Descriere</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
         <asp:TableRow>
             <asp:TableCell>
                 <asp:Label ID="LabelCategory" runat="server" Text="Label">Categorie</asp:Label>
             </asp:TableCell>
             <asp:TableCell>
                 <asp:DropDownList class="selectpicker" ID="bookcategory" runat="server" >
                     <asp:ListItem Text="Atlase" Value="atlase" Enabled="true"></asp:ListItem>
                     <asp:ListItem Text="Beletristica (romane)" Value="beletristica"></asp:ListItem>
                     <asp:ListItem Text="Carte veche" Value="carteveche" ></asp:ListItem>
                     <asp:ListItem Text="Culegeri" Value="culegeri"></asp:ListItem>
                     <asp:ListItem Text="Dictionare" Value="dictionare"></asp:ListItem>
                     <asp:ListItem Text="Eminescu" Value="eminescu"></asp:ListItem>
                     <asp:ListItem Text="Istorie" Value="istorie"></asp:ListItem>
		             <asp:ListItem Text="Literatura pentru copii" Value="literaturapentrucopii"></asp:ListItem>
                     <asp:ListItem Text="Medicina" Value="medicina"></asp:ListItem>
                     <asp:ListItem Text="Metodice" Value="metodice"></asp:ListItem>
                     <asp:ListItem Text="Politica" Value="politica"></asp:ListItem>
			         <asp:ListItem Text="Religie" Value="religie"></asp:ListItem>
		             <asp:ListItem Text="Reviste" Value="reviste"></asp:ListItem>
                     <asp:ListItem Text="Sport" Value="sport"></asp:ListItem>
                 </asp:DropDownList>
             </asp:TableCell>
         </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelPhoto" runat="server" Text="Label">Imagine</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:FileUpload ID="FileUploadBook" runat="server"/>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelExtraInfo" runat="server" Text="Label">Editura, anul</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxExtraInfo" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelPrice" runat="server" Text="Label">Pret</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxPrice" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelPages" runat="server" Text="Label">Pagini</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxPages" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="3">
                    <asp:Button ID="ButtonAddBook" runat="server" Text="Adaugare carte" CssClass="btn btn-primary"
                        OnClick="Btn_AddBook_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</asp:Content>
