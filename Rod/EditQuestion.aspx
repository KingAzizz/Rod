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
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/tags">الأقسام</asp:HyperLink>
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
        <div style="display:flex; justify-content:center; align-items:center; flex-direction:row;">
            <span> القسم المختار حاليا:&nbsp  </span> 
            <asp:Label ID="currentTag" runat="server"></asp:Label>
        </div>
        <div class="tags">
          <asp:DropDownList ID="tagseditDropdownlist" runat="server" CssClass="dropdownlistTags">
                    <asp:ListItem Value="0">اختر قسم</asp:ListItem>
                     <asp:ListItem Value="برمجة الحاسب الآلي">برمجة الحاسب الآلي</asp:ListItem>
                     <asp:ListItem Value="شبكات الحاسب الآلي">شبكات الحاسب الآلي</asp:ListItem>
                      <asp:ListItem Value="التسويق و المبيعات">التسويق و المبيعات</asp:ListItem>
                      <asp:ListItem Value="إدارة سلاسل الإمداد">إدارة سلاسل الإمداد</asp:ListItem>
                      <asp:ListItem Value="الأعمال البنكية">الأعمال البنكية</asp:ListItem>
                      <asp:ListItem Value="إدارة المستشفيات">إدارة المستشفيات</asp:ListItem>
                      <asp:ListItem Value="المحاسبة">المحاسبة</asp:ListItem>
                      <asp:ListItem Value="السكرتير التنفيذي">السكرتير التنفيذي</asp:ListItem>
                      <asp:ListItem Value="إدارة الموارد البشرية">إدارة الموارد البشرية</asp:ListItem>
                      <asp:ListItem Value="خدمة العملاء">خدمة العملاء</asp:ListItem>
          </asp:DropDownList>
        </div>
        <asp:Label ID="debug" runat="server"></asp:Label>
        <div class="editButtons">
            <asp:Button ID="saveChanges" runat="server" Text="حفظ التغيرات" OnClick="SaveChanges" />
            
            <asp:Button ID="cancelChanges" runat="server" Text="الغاء" OnClick="CancelChanges" />

        </div>
    </section>
</asp:Content>
