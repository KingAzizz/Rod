<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QuestionForm.aspx.cs" Inherits="Rod.QuestionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/questionForm.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
 
        <div class="askContainer">
             <h1 class="nav">اسأل</h1>
             <div class="askform">
            <h1 class="subject">العنوان</h1>
            <h6 class="subinfo">دقة العنوان تساعد على إيجاد الأجابه</h6>
                 <div class="Movesubjecttxt">
                     <asp:TextBox ID="titletxt" CssClass="titletxt" runat="server"></asp:TextBox>
                     
                 </div>
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="titletxt" ValidationExpression="^\s*(?:\S\s*){15,50}$" Display="Dynamic" ErrorMessage="يجب ان يكون طول العنوان مابين 15 حرف الى 50" ForeColor="Red" Font-Size="Medium">
                     </asp:RegularExpressionValidator>
                 <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="titletxt" ErrorMessage="حقل العنوان ألزامي" Display="Dynamic" ForeColor="Red"  Font-Size="Medium"></asp:RequiredFieldValidator>
            <h1 class="subject">الموضوع</h1>
                 <h6 class="subinfo">يرجى ارفاق كل المعلومات المطلوبه</h6>
             <div class="Movesubjecttxt">
                     <asp:TextBox ID="subjecttxt" CssClass="subjecttxt" runat="server"></asp:TextBox>

                 </div>
                      <asp:RequiredFieldValidator ID="reqSubject" runat="server" ControlToValidate="subjecttxt" ErrorMessage="حقل الموضوع ألزامي" Display="Dynamic" ForeColor="Red"  Font-Size="Medium"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="subjecttxt" Display="Dynamic" ValidationExpression="^\s*(?:\S\s*){30,80}$" ErrorMessage="يجب ان يكون طول الموضوع مابين 30 حرف الى 80" ForeColor="Red" Font-Size="Medium">
                     </asp:RegularExpressionValidator>
                
            <h1 class="subject">القسم</h1>
                 <h6 class="subinfo">ادخل القسم المتعلق بالسؤال</h6>
             <div class="Movesubjecttxt">
                      <asp:DropDownList ID="tagsDropDownList" runat="server" CssClass="dropdownlistTags">
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
                 <asp:Label ID="tagMissing" runat="server" Visible="false" ForeColor="Red" >حقل القسم ألزامي</asp:Label>
                 <div class="btncontrol">
            <asp:Button CssClass="reviewbtn" Text="مراجعة السؤال" runat="server" OnClick="submitQuestion_Click"/>
                     
        </div>
        </div>
        </div>
</asp:Content>
