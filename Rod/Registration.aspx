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
            <asp:TextBox ID="usernameTxt" runat="server" CssClass="grayInput"></asp:TextBox>

            <asp:RegularExpressionValidator ID="rgxUsername" runat="server" ForeColor="Red" ControlToValidate="usernameTxt" ValidationExpression="^[A-Za-z][A-Za-z0-9_]{3,29}$" ErrorMessage="اسم المستخدم يجب ان يكون مابين 4 الى 29 حرف"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="requsername" runat="server" ForeColor="Red" ControlToValidate="usernameTxt" ErrorMessage="حقل اسم المستخدم فارغ"></asp:RequiredFieldValidator>

             <asp:Label ID="emaillbl" runat="server">بريدك الإلكتروني</asp:Label>

             <asp:TextBox ID="EmailTxt" runat="server" TextMode="Email" CssClass="grayInput"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailTxt"   
                ErrorMessage="الرجاء ادخال بريد الإلكتروني صحيح!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                </asp:RegularExpressionValidator>  
             <asp:RequiredFieldValidator  ID="EmailVal" ControlToValidate="EmailTxt" runat="server" ErrorMessage="ss"></asp:RequiredFieldValidator>
            
            <asp:Label ID="password" runat="server">كلمة المرور</asp:Label>
            
             <asp:TextBox ID="passwordTxt" runat="server" TextMode="Password" CssClass="grayInput"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rgxPassword" runat="server" ForeColor="Red" ControlToValidate="passwordTxt" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$" ErrorMessage="كلمة المرور يجب ان تتكون من حرف كبير وحرف صغير ورقم ورموز الحد الادنى 8 خانات"> </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="reqpassword" runat="server" ForeColor="Red" ControlToValidate="passwordTxt" ErrorMessage="حقل كلمة المرور فارغ"></asp:RequiredFieldValidator>

             <asp:Label ID="Label4" runat="server">تأكيد كلمة المرور</asp:Label>

              <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="grayInput"></asp:TextBox>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="حقل كلمة المرور فارغ" ForeColor="red"></asp:RequiredFieldValidator>
             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                 ControlToValidate="ConfirmPassword"
                 CssClass="ValidationError"
                 ControlToCompare="passwordTxt"
                 ForeColor="Red"
                 ErrorMessage="كلمة المرور غير متطابقة " />
                <asp:Label ID="badReg" CssClass="badReg" runat="server" Visible="false">اسم المستخدم او البريد الالكتروني مستخدم سابقا</asp:Label>
            
            <asp:Button ID="loginBtn" CssClass="loginBtn" runat="server" Text="سجل" OnClick="Reg" />
            
        </div>
    </section>
</asp:Content>
