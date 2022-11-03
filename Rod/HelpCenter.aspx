<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HelpCenter.aspx.cs" Inherits="Rod.HelpCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href='<%= Page.ResolveUrl("~/css/helpCenter.css")%>'rel="stylesheet" type="text/css">
    <script>
        console.log('%c Developed By: Turki Faisal Alotaibi & Waleed Khaild Almalki', 'background: #000b42; color: #FFFFF;font-size:15px;');

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="main">

                     
                <div class="cards_item">
                            <div class="card">
                                <div class="card_title">
                                    <h1 class="sub_title">السلامة والأمان 🤳</h1>
                                </div>
                                <div class="card_content">
                                    <h6 class="card_sub_title">
                                        كيفية حماية معلوماتك الشخصية
                                    </h6>
                                    <p class="card_text">
انت تتحكم في المعلومات الذي تنشرها على ROD، لا تنشر معلومات تعتبرها سريه وكن حذرًا من اي رسائل عن طريق البريد الالكتروني تطلب منك معلوماتك الشخصيه او كلمات المرور وان حدث اختراق لحسابك الشخصي يمكنك التواصل معنا مع اثبات ان الحساب المخترق ملكك 
                                    </p>
                             </div>
                        </div>
                </div>

                <div class="cards_item">
                            <div class="card">
                                <div class="card_title">
                                    <h1 class="sub_title"> إدارة حسابك 🔐</h1>
                                </div>
                                <div class="card_content">
                                    
                                    <h6 class="card_sub_title">
                                        المساعدة بشأن الحساب المقفل أو المقيد
                                    </h6>
                                     <p class="card_text">
                                        اذا كان حسابك على ROD مقفلًا او مقيد بمميزات محدده، فقد يكون عرضه للخطر او للانتهاك لقوانين ROD، في حال حدوث مثل هذا الامر يمكنك التواصل معنا
                                    </p>
                                    <h6 class="card_sub_title">
                                        كيفية تحديث عنوان البريد الإلكترونيّ
                                    </h6>
                                    <p class="card_text">
يمكنك تحديث عنوانك البريدي عن طريق الدخول على ملفك الشخصي و الذهاب الى اعدادات الحساب وبذلك يظهر لك البريد الالكتروني الحالي ويمكنك تغييره 
                                    </p>
                                </div>
                            
                        </div>
                </div>

                <div class="cards_item">
                            <div class="card">
                                <div class="card_title">
                                    <h1 class="sub_title"> القوانين والسياسات 📝</h1>
                                </div>
                                <div class="card_content">
                                    
                                    <p class="card_text">
                                       يجب ان يكون الجواب متعلق بالسؤال
                                        <br />
                                        السؤال يجب ان يكون منطقي
                                        <br />
                                        الاساءة او السلوك المسيء
                                        <br />
                                        التدخل في الامور السياسية
                                        <br />
                                        الرسائل المزعجة
                                    </p>
                             </div>
                        </div>
                </div>
               
                <div class="cards_item">
                            <div class="card">
                                <div class="card_title">
                                    <h1 class="sub_title">الإبلاغ عن إساءة ❗</h1>
                                </div>
                                <div class="card_content">
                                    
                                    <p class="card_text">
                                       عند إبلاغ ROD  عن أي محتوى، نقوم بمراجعته وإزالة أي شيء يختلف مع معايير مجتمع ROD. إذا كنت بصدد الإبلاغ عن رسائل، يمكن استخدام هذه الرسائل التي تم الإبلاغ عنها لمساعدتنا على تحسين أنظمتنا لمراجعة عناصر المحتوى الأخرى التي يتم الإبلاغ عنها والتي تنتهك معايير مجتمعنا. ولا نقم بتضمين أي معلومات عن الشخص مقدم البلاغ عند الوصول إلى الشخص المبلغ عنه. يُرجى تذكر أن الإبلاغ عن محتوى إلى ROD لا يضمن إزالته.
                                    </p>
                             </div>
                        </div>
                </div>
            </section>
</asp:Content>
