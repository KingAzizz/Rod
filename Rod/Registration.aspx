<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Rod.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/registration.css")%>'rel="stylesheet" type="text/css">
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

             <asp:Label ID="emaillbl" runat="server">بريدك الإلكتروني</asp:Label>

             <asp:TextBox ID="EmailTxt" runat="server" TextMode="Email"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailTxt"   
                ErrorMessage="الرجاء ادخال بريد الإلكتروني صحيح!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                </asp:RegularExpressionValidator>  
             <asp:RequiredFieldValidator  ID="EmailVal" ControlToValidate="EmailTxt" runat="server" ErrorMessage="ss"></asp:RequiredFieldValidator>
            
            <asp:Label ID="password" runat="server">كلمة المرور</asp:Label>
            
             <asp:TextBox ID="passwordTxt" runat="server" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="reqpassword" runat="server" ControlToValidate="passwordTxt" ErrorMessage="حقل كلمة المرور فارغ" ForeColor="red"></asp:RequiredFieldValidator>

             <asp:Label ID="Label4" runat="server">تأكيد كلمة المرور</asp:Label>

              <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="حقل كلمة المرور فارغ" ForeColor="red"></asp:RequiredFieldValidator>
             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                 ControlToValidate="ConfirmPassword"
                 CssClass="ValidationError"
                 ControlToCompare="passwordTxt"
                 ForeColor="Red"
                 ErrorMessage="كلمة المرور غير متطابقة " />

         
            <asp:Button ID="loginBtn" CssClass="loginBtn" runat="server" Text="سجل" OnClick="Reg" />
            
        </div>
    </section>
</asp:Content>
