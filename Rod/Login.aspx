<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Rod.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./css/login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="login">
        <div> <h1>رد</h1>   </div>
        <div class="loginContainer">
            <div class="invalidCredentialsContainer">

            <asp:Label ID="invalidCredentials" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <asp:Label ID="username" runat="server">اسم المستخدم</asp:Label>
            <asp:TextBox ID="usernameTxt" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="requsername" runat="server" ControlToValidate="usernameTxt" ErrorMessage="حقل اسم المستخدم فارغ" ForeColor="red"></asp:RequiredFieldValidator>

            <span>نسيت كلمة المرور</span>
            
            <asp:Label ID="password" runat="server">كلمة المرور</asp:Label>
            
             <asp:TextBox ID="passwordTxt" runat="server" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="reqpassword" runat="server" ControlToValidate="passwordTxt" ErrorMessage="حقل كلمة المرور فارغ" ForeColor="red"></asp:RequiredFieldValidator>
            <div>
            <asp:CheckBox ID="rememberMe" runat="server" Text="تذكرني" />
            </div>
            <asp:Button ID="loginBtn" CssClass="loginBtn" runat="server" Text="دخول" OnClick="Login_Click" />
            
        </div>
    </section>
</asp:Content>
