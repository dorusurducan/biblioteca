<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="Bibliotheque.Loans" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
   
    <div style="padding-left:200px" id="divSearchUser" runat="server">
        <asp:Label ID="lblSearchUser" runat="server" Width="150px">search user:</asp:Label>
        <asp:Textbox ID="txtSearchUser" runat="server"></asp:Textbox>
        <asp:LinkButton ID="btnSearchUser" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearchUser_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    <div id="divChangeUser" runat="server" style="padding-left:188px" visible="false">
        <asp:LinkButton id="btnChangeUser" runat="server" CssClass="btn btn-link" OnClick="btnChangeUser_Click">Change user</asp:LinkButton>
    </div>
    <div id="divGridUsers" runat="server" visible="false">
        <asp:GridView ID="gridUsers" runat="server"
            AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None"
            CssClass="table table-condensed" Width="98%"
            DataKeyNames="iduser"
            AllowPaging="True"
            HorizontalAlign="Center"
            OnPageIndexChanging="gridview_PageIndexChanging"
            OnRowCommand="gridview_OnRowCommand">
            <AlternatingRowStyle BackColor="White" /> 
            <Columns>
                <asp:TemplateField HeaderText="User ID">
                    <ItemTemplate>
                        <asp:Label ID="lblIdUser" runat="server" Text='<%# Bind("iduser") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Surname">
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text='<%# Bind("surname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class">
                    <ItemTemplate>
                        <asp:Label ID="lblClasa" runat="server" Text='<%# Bind("clasa") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:Button id="btnSelect" runat="server" Text="Select" 
                            CommandName="Select" 
                            CommandArgument="<%# Container.DataItemIndex %>"/>
                    </ItemTemplate>
                </asp:TemplateField>  
             </Columns>
             <AlternatingRowStyle BackColor="White" />
             <EditRowStyle BackColor="#c1c5cc" />
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
    </div>
    <div id="divUserChoosen" runat="server" style="padding-left:200px" visible="false">
        <h4><asp:Label ID="lblUserBorrow"  runat="server" ></asp:Label></h4>
    </div>
    <div id="divChangeBooks" runat="server" style="padding-left:188px" visible="false">
        <asp:LinkButton id="btnChangeBooks" runat="server" CssClass="btn btn-link" OnClick="btnChangeBooks_Click">Change/Add book</asp:LinkButton>
    </div>
    <script>
        function pressEnter(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=btnSearchBooks.UniqueID%>', "");
            }
        }
    </script>
    <div id="divSearchBooks" runat="server" style="padding-left:200px" visible="false">
        <asp:Label ID="lblSearchBook" runat="server" Width="150px">Caută carte:</asp:Label>
        <asp:Textbox ID="txtSearchBooks" runat="server" onkeypress="return pressEnter(event)"></asp:Textbox>
        <asp:LinkButton ID="btnSearchBooks" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearchBook_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    <div id="divGridBooks" runat="server" visible="false">
        <asp:GridView ID="gridBooks" runat="server" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" 
            ForeColor="#333333" GridLines="None" CssClass="table table-condensed" Width="98%" 
            OnRowDataBound="GridViewBooks_RowDataBound" AllowPaging="true" OnPageIndexChanging="gridview_PageIndexChanging"
            OnRowCommand="gridview_OnRowCommand" PagerSettings-FirstPageText="First Page" PagerSettings-NextPageText="Next Page&nbsp;" PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; " PagerSettings-LastPageText="&nbsp; Last Page &nbsp;" PagerSettings-Mode=" NextPreviousFirstLast">  
            <AlternatingRowStyle BackColor="White" />  
            <Columns>  
                <asp:TemplateField HeaderText="ID book">   
                    <ItemTemplate>  
                        <asp:Label ID="idlivre" runat="server" Text='<%#Bind("idlivre") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Registered code">   
                    <ItemTemplate>  
                        <asp:Label ID="idlcodeISBN" runat="server" Text='<%#Bind("codeISBN") %>'></asp:Label>  
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
                        <asp:Label ID="category" runat="server"  Text='<%#Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="image" runat="server" AlternateText=" " 
                            ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>' Height="100px" Width="75px"
                            DescriptionUrl='<%# Eval("photo") %>'>
                        </asp:Image>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Availability">
                    <ItemTemplate>
                        <asp:Label ID="lblAvailability" runat="server" Text='<%#Bind("availability") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                CommandName="Add"
                                CommandArgument="<%# Container.DataItemIndex %>" />
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
        </div>
    <div id="divBorrowedBooks" runat="server" visible="false">
        <h4 style="padding-left: 200px">
            <asp:Label ID="lblBorrowedBooks" Text="Book to borrow:" runat="server"></asp:Label>
        </h4>
    
        <asp:GridView ID="gridFinalize" runat="server" AutoGenerateColumns="False" CellPadding="4" 
            HorizontalAlign="Center" 
            ForeColor="#333333" GridLines="None" CssClass="table table-condensed" Width="70%" 
            OnRowCommand="gridview_OnRowCommand" 
            OnRowDataBound="GridViewBooks_RowDataBound" AllowPaging="true" OnPageIndexChanging="gridview_PageIndexChanging"
            DataKeyNames="idlivre" OnRowDeleting="gridFinalize_RowDeleting" >  
            <AlternatingRowStyle BackColor="White" />  
            <Columns>  
                <asp:TemplateField HeaderText="ID book">   
                    <ItemTemplate>  
                        <asp:Label ID="idlivre" runat="server" Text='<%#Bind("idlivre") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Registered code">   
                    <ItemTemplate>  
                        <asp:Label ID="idlcodeISBN" runat="server" Text='<%#Bind("codeISBN") %>'></asp:Label>  
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
                        <asp:Label ID="category" runat="server"  Text='<%#Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="image" runat="server" AlternateText=" " 
                            ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>' Height="100px" Width="75px"
                            DescriptionUrl='<%# Eval("photo") %>'>
                        </asp:Image>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Availability">
                    <ItemTemplate>
                        <asp:Label ID="lblAvailability" runat="server" Text='<%#Bind("availability") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" 
                                CommandArgument="<%# Container.DataItemIndex %>"
                                CommandName="Delete"
                                Text="Delete" />
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

        <div style="padding-left: 200px">
            <asp:Label ID="lblDate" runat="server">Return date:</asp:Label>
            <asp:TextBox ID="txtReturnDate" runat="server" Width="100px" ClientIDMode="Static"/>
            <script language="javascript" type="text/javascript">
                $('#txtReturnDate').datepicker({ dateFormat: 'dd/MM/yy' }).val();
                $('#txtReturnDate').datepicker({ changeMonth: true, changeYear: true, numberOfMonths: 2 });
                $('#txtReturnDate').datepicker("setDate", "+3w");
                
            </script>
            <br />
            <asp:Button ID="btnBorrowBooks" runat="server" Text="Borrow these books"
                CssClass="btn btn-primary"
                OnClick="btnBorrowBooks_Click"/>
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" Visible="false" OnClick="btnPrint_Click" />
        </div>
    </div>
    
    <asp:HiddenField ID="hfUser" runat="server" />
    <asp:HiddenField ID="hfBooks" runat="server" />
   
</asp:Content>
