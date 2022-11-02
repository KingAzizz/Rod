<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Rod.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تسجيل دخول</title>
        <link href="./css/login.css" rel="stylesheet" type="text/css" />
        <link href="./css/masterPage.css" rel="stylesheet" type="text/css" />
    <script src="https://kit.fontawesome.com/f933819f72.js" crossorigin="anonymous"></script>
        <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1" />
        <link rel="shortcut icon" type="image/x-icon" href="./images\favicon-modified.png" />

</head>
<body>
    <form id="form1" runat="server">
         <nav class="navbar">
        <div class="container">

            <asp:HyperLink CssClass="logo" NavigateUrl="~/" runat="server">
                  <span>رد</span>
            </asp:HyperLink>
            
            <ul class="questionLink">
                <li> 
                 
                    <i class="fa-solid fa-earth-africa"></i>
                    <asp:HyperLink ID="questionsLink" runat="server" NavigateUrl="~/questions" ForeColor="Black">
                         الاسئلة
                    </asp:HyperLink>
                </li>
            </ul>
        
    

           <ul class="navLinks" style="justify-content: flex-end; margin-right: 25px;">
           
            <li><i class="fa-solid fa-moon icon"></i></li>

            <li>
                  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/register" Text="تسجيل"></asp:HyperLink>
                </li>
                <li id="loginLink" runat="server">
                    <asp:HyperLink ID="loginHyLink" runat="server" NavigateUrl="~/login" Text="دخول"></asp:HyperLink>
                </li>
             
        </ul>
    </div>
    </nav>
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
           <footer class="footer">
        <div>
            <ul>
                <li>رد</li>
                <li>الأسئلة</li>
                <li>تواصل</li>
                <li>حول</li>
                <li><asp:HyperLink ID="helpNav" runat="server" NavigateUrl="~/help" ForeColor="white">مساعدة</asp:HyperLink></li>
                <li><i class="fa-brands fa-twitter"></i></li>
            </ul>
        </div>
        <div>
            <h1>مكان لجميع المتعلمين</h1>
        </div>
    </footer>
    </form>
</body>
</html>
