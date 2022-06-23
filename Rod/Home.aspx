<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Rod.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>الصفحة الرئيسية</title>
    <link href="./css/home.css" rel="stylesheet" type="text/css" />
    <script src="./js/home.js" defer></script>
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="homeNoLoggin" id="HideIfUserLoggedIn" runat="server">
        <div class="introContent">
            
            <asp:Image ID="homeImage" runat="server" CssClass="homeImage1" ImageUrl="./images/homeIIIustr.jpg" />
            <div class="RodJoin">
                <h1> ُرد</h1>
                <p>اسأل او جاوب لمساعدة مجتمع المتعلمين كافه</p>
                <asp:HyperLink ID="joinLink" runat="server" NavigateUrl="~/Registration.aspx">انضم</asp:HyperLink>

                <asp:HyperLink ID="questionsLink" runat="server" NavigateUrl="~/Questions.aspx">ابحث عن الأسئلة</asp:HyperLink>
            </div>
        </div>
        
        <section class="aboutUs">
            <div>
                <div>
                    <h1>حولنا</h1>
                </div>

                <p><span>رد ُ</span>منصة بدت من طلاب معهد الادارة العامة لإنعاش المجتمع العربي من ناحية الأسئلة والأجوبة لزيادة معرفتهم لتخصصاتهم او مجالات اخرى يعملون فيها</p>
        </div>
        </section>

        <section class="founders">
            
                
                    <h1>المؤسسين</h1>
                    <div>

                        <table>
                            <tr>
                                <td>وليد المالكي</td>
                                <td>تركي العتيبي</td>
                            </tr>
                            <tr>
                                <td>عبدالعزيز الجهني</td>
                                <td>عبدالعزيز السنيدي</td>
                            </tr>
                        </table>
                    </div>
        </section>


        <section class="whyRod">
            <h1>لماذا <span>رد</span></h1>
            <div>

                <p dir="rtl">ارتفعت الحاجة إلى مثل هذا التطبيق على شبكة الإنترنت استجابة لعدم وجود طرق للتواصل مفيده للمتعلمين.  يتم يستخدام تطبيقات مثل WhatsApp و Telegram  لكن مميزاتها محدودة.
                    و يفتقر المتعلمين إلى منصة مركزية لتبادل المعرفة والخبرات.</p>
                </div>
        </section>
    </section>

     <section class="homeLoggedIn" id="homeLoggedIn" runat="server">
         <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="selected" runat="server" NavigateUrl="~/Home.aspx">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/Tags.aspx">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TopUsers.aspx">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>
        <section class="content">
            <div>
                
                 <asp:Button ID="monthFilter" runat="server" Text="الشهر" OnClick="MonthFilter" />
                  <asp:Button ID="commonFilter" runat="server" Text="الشائع" />
                  <asp:Button ID="weekFilter" runat="server" Text="الأسبوع" />

            </div>
            <div>
                <h1>أفضل ألاسئلة</h1>
                <button id="askQuestion" runat="server">إسأل</button>
             

            </div>
           
        </section>
          <section class="sectionQuestions" id="sectionQuestions" runat="server">
        
        </section>
    </section>

</asp:Content>
