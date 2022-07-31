<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Rod.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script defer src='<%= Page.ResolveUrl("~/js/editProfile.js")%>' charset="utf-8"></script>
    <link href='<%= Page.ResolveUrl("~/css/editProfile.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="homePage" runat="server" NavigateUrl="~/Home.aspx">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="tags" runat="server" NavigateUrl="~/Tags.aspx">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="topUsers" runat="server" NavigateUrl="~/TopUsers.aspx">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>

      <section class="editProfileSection" dir="rtl">
      <div><h1 style="margin-top: 50px;">عدل صفحتك الشخصية</h1></div>
      <div class="profileContent">
        <div>
            <asp:Image ID="profileImageEdit" CssClass="profileImageEdit" runat="server" />
          <div>
            <label
              id="uploadImageLabel"
              class="inputFileMask"
              style="cursor: pointer"
              >غير صورتك</label
            >
              <asp:FileUpload ID="imageUpload" runat="server" CssClass="fileUpload" />
          </div>
        </div>
        <label>اسمك المعروض</label>
        <asp:TextBox ID="displayNameInput" runat="server" CssClass="normalInput" ></asp:TextBox>
           <label>تعليمك</label>
        <asp:TextBox ID="educationInput" runat="server" CssClass="normalInput" ></asp:TextBox>
        <label>موقعك</label>
        <asp:TextBox ID="locationInput" runat="server" CssClass="normalInput" ></asp:TextBox>
        <label>عنوانك</label>
       <asp:TextBox ID="titleInput" runat="server" CssClass="normalInput" ></asp:TextBox>
        <label>عنك</label>
        <asp:TextBox ID="aboutInput" runat="server" CssClass="normalInput" ></asp:TextBox>
        <div class="linkheader">
          <h1>الروابط</h1>
        </div>
        <div class="linksWrapper">
          <div>
            <label>تويتر</label>
             <div>
              <i class="fa-brands fa-twitter"></i>
      
            <asp:TextBox ID="twitterInput" runat="server"></asp:TextBox>

            </div>
          </div>
          <div>
            <label>قيت هب</label>

            <div>
              <i class="fa-brands fa-github"></i>
                 <asp:TextBox ID="githubInput" runat="server"></asp:TextBox>
            </div>

          </div>
          <div>
            <label>موقعك الشخصي</label>
            <div>
              <i class="fa-solid fa-link"></i>
                <asp:TextBox ID="websiteInput" runat="server"></asp:TextBox>
            </div>
          </div>
        </div>
        <section class="buttonSaveCancel">
            <asp:Button ID="saveEdit" runat="server" Text="احفظ" OnClick="SaveEdit" />
            <asp:Button ID="cancelEdit" runat="server" Text="الغاء" OnClick="CancelEdit" />
        </section>
      </div>
    </section>
</asp:Content>
