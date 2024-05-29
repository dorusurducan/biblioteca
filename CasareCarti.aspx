<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CasareCarti.aspx.cs" Inherits="Bibliotheque.CasareCarti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Label ID="lblMessage" runat="server" />
    <asp:Table ID="TableAddBook" runat="server" HorizontalAlign="Center" >
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Lblidlivre" runat="server" Text="Label">ID book</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxidLivre" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelcodeISBN" runat="server" Text="Label">Registered code</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxCodeISBN" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelInitialCode" runat="server" Text="Label">Initial code</asp:Label>
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
                    <asp:Label ID="LabelTitle" runat="server" Text="Label">Title</asp:Label>
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
                    <asp:Label ID="LabelAuthor" runat="server" Text="Label">Author</asp:Label>
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
                    <asp:Label ID="LabelDescription" runat="server" Text="Label">Description</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
         <asp:TableRow>
             <asp:TableCell>
                 <asp:Label ID="LabelCategory" runat="server" Text="Label">Categoriy</asp:Label>
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
                    <asp:Label ID="LblRegDate" runat="server" Text="Label">Registration date</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxRegDate" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelExtraInfo" runat="server" Text="Label">Ed, Year</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxExtraInfo" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelPrice" runat="server" Text="Label">Price</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxPrice" runat="server" CssClass="form-control" ontextchanged="TextBoxPrice_TextChanged" AutoPostBack="true"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="LabelPriceDen" runat="server" Text="Label">Denominative price</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxPriceDen" runat="server" CssClass="form-control" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
            <asp:Label ID="lblDate" runat="server">Archive date:</asp:Label>
                </asp:TableCell>
                <asp:TableCell>
            <asp:TextBox ID="txtDataCasare" runat="server" Class="date-picker" ></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign ="Right">
                <asp:TableCell ColumnSpan="2">

                    <asp:Button id="btn_casare" runat="server" Text="Archive" OnClick="btn_casare_Click" cssclass="btn btn-primary"/>
               
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button id="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" cssclass="btn btn-primary"/>
                </asp:TableCell>
           </asp:TableRow>
        </asp:Table>
</asp:Content>
