<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditQuestion.aspx.cs" Inherits="Rod.EditQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script defer src='<%= Page.ResolveUrl("~/js/home.js")%>' charset="utf-8"></script>
    <link href='<%= Page.ResolveUrl("~/css/editQuestion.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="selected" runat="server" NavigateUrl="~/">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/Tags.aspx">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/top/users">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>
    <section class="editContainer" dir="rtl">
        <label>العنوان</label>
   
        <asp:TextBox ID="titleText" CssClass="titleInput" runat="server"></asp:TextBox>
        <label>الموضوع</label>
         <asp:TextBox ID="bodyText" CssClass="bodyInput" runat="server"></asp:TextBox>
       
        <div>
            <p></p>
        </div>
        <label>الأقسام</label>
        <div class="tags">
            <span class="tagContainer">
            
                    <asp:DataList id="Tags" runat="server" onItemCommand ="Tags_ItemCommand" >
            <ItemTemplate>
                <span id="existingTags" runat="server">
                    <%# Eval("tagName") %>
                </span>
                <asp:HiddenField ID="tagNameHidden" runat="server" Value='<%# Eval("tagName") %>' />
                <span id="deleteButton" runat="server">
                <asp:LinkButton ID="deleteTag" runat="server" CommandName="DeleteTag"
                CommandArgument='<%# Eval("id") %>' ForeColor="DarkRed"><i class="fa-solid fa-x"></i>
              </asp:LinkButton>

                </span>
            </ItemTemplate>
                        </asp:DataList>
            </span>
            <asp:TextBox ID="tagText" runat="server"></asp:TextBox>
        </div>
        <asp:Label ID="debug" runat="server"></asp:Label>
        <div class="editButtons">
            <asp:Button ID="saveChanges" runat="server" Text="حفظ التغيرات" OnClick="SaveChanges" />
            
            <asp:Button ID="cancelChanges" runat="server" Text="الغاء" OnClick="CancelChanges" />

        </div>
    </section>
</asp:Content>
