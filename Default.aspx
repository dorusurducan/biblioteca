<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bibliotheque.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="jumbotron" >
        <div class="container text-center" >
            <div class="row row-centered">
                <h3>Library</h3>
                <div>
                    <img src="images\biblio2.jpg" />

                </div>    
            </div>
        </div>
    </div>
    <div class="container text-center">
        <div class="row row-centered">
            <asp:Button ID="btnEnter" runat="server" OnClick="btnEnter_Click" Text="ENTER" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
