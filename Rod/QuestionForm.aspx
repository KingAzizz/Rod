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
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="titletxt" ValidationExpression="^\s*(?:\S\s*){15,50}$" ErrorMessage="يجب ان يكون طول العنوان مابين 15 حرف الى 50" ForeColor="Red" Font-Size="X-Small">
                     </asp:RegularExpressionValidator>

            <h1 class="subject">الموضوع</h1>
                 <h6 class="subinfo">يرجى ارفاق كل المعلومات المطلوبه</h6>
             <div class="Movesubjecttxt">
                     <asp:TextBox ID="subjecttxt" CssClass="subjecttxt" runat="server"></asp:TextBox>
      
                 </div>
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="subjecttxt" ValidationExpression="^\s*(?:\S\s*){30,80}$" ErrorMessage="يجب ان يكون طول الموضوع مابين 30 حرف الى 80" ForeColor="Red" Font-Size="X-Small">
                     </asp:RegularExpressionValidator>

            <h1 class="subject">القسم</h1>
                 <h6 class="subinfo">ادخل القسم المتعلق بالسؤال</h6>
             <div class="Movesubjecttxt">
                     <asp:TextBox ID="sectiontxt" CssClass="titletxt" runat="server"></asp:TextBox>
                 </div>
                 <div class="btncontrol">
            <asp:Button CssClass="reviewbtn" Text="مراجعة السؤال" runat="server" OnClick="submitQuestion_Click"/>
                     
        </div>
        </div>
        </div>
</asp:Content>
