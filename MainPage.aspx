<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Bibliotheque.MainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div style="text-align:center">
        <h1>List of books</h1>
    </div>
    <div style="padding-left:200px">
        Search:<asp:Textbox ID="search" runat="server"></asp:Textbox>
        Availability:<asp:CheckBox ID="chkAvailable" runat="server" Checked="true" />
        Borrowed:<asp:CheckBox ID="chkBorrowed" runat="server" Checked="true" />
        &nbsp;
        Search by category:  
        <asp:DropDownList ID="ddlBookCategory" runat="server" class="selectpicker">
        </asp:DropDownList>
        <asp:LinkButton ID="btnSearch" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearch_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    <br />
    <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" 
            ForeColor="#333333" GridLines="None" CssClass="table table-condensed" Width="98%"
            OnRowDataBound="GridViewBooks_RowDataBound" AllowPaging="true" OnPageIndexChanging="gridview_PageIndexChanging"
        PagerSettings-FirstPageText="First Page" PagerSettings-NextPageText="Next Page&nbsp;" PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; "
         PagerSettings-LastPageText="&nbsp; Last Page &nbsp;" PagerSettings-Mode=" NextPreviousFirstLast">  
        <AlternatingRowStyle BackColor="White" />  
        <Columns>  
            <asp:TemplateField HeaderText="ID book">   
                <ItemTemplate>  
                    <asp:Label ID="idlivre" runat="server" Text='<%#Bind("idlivre") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="Registered code" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                         <asp:Label ID="lblCodeISBN" runat="server" Text='<%# Bind("codeISBN") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
            <asp:TemplateField HeaderText="Code book">   
                    <ItemTemplate>  
                        <asp:Label ID="idinitialcode" runat="server" Text='<%#Bind("initialcode") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
            <asp:TemplateField HeaderText="Title">  
                <ItemTemplate>  
                    <asp:Label ID="title" runat="server" Text='<%#Bind("title") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Author">
                <ItemTemplate>
                    <asp:Label ID="author" runat="server" Text='<%#Bind("author") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="description" runat="server"  Text='<%#Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:Label ID="category" runat="server" Text='<%#Bind("category") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="image" runat="server" AlternateText=" " 
                        ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>' 
                        Height="100px" Width="75px"
                        DescriptionUrl='<%# Eval("photo") %>'>
                    </asp:Image>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Availability">
                <ItemTemplate>
                    <asp:Label ID="lblAvailability" runat="server" Text='<%#Bind("availability") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>        
        </Columns>  
        <EditRowStyle BackColor="#2461BF" />  
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />  
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />  
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />  
        <RowStyle BackColor="#EFF3FB" />  
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />  
        <SortedAscendingCellStyle BackColor="#F5F7FB" />  
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />  
        <SortedDescendingCellStyle BackColor="#E9EBEF" />  
        <SortedDescendingHeaderStyle BackColor="#4870BE" />  
    </asp:GridView>  
</asp:Content>
