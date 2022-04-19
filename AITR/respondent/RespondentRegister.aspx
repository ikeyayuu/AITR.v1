<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RespondentRegister.aspx.cs" Inherits="AITR.RespondentRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="title">Register your information</h1>
    <div class="register__respondent__area">
        <div class="register__respondent__line">
            <div class="field_title">First name:</div> <asp:TextBox class="field" ID="TextBoxFirstName" runat="server" />
        <asp:RequiredFieldValidator ID="First_Name__validator" runat="server" ErrorMessage="Please fill the field." ControlToValidate="TextBoxFirstName"></asp:RequiredFieldValidator>
        </div>
        <div class="register__respondent__line">
            <div class="field_title">Last name:</div> <asp:TextBox class="field" ID="TextBoxLastName" runat="server" />
        <asp:RequiredFieldValidator ID="Last_Name__validator" runat="server" ErrorMessage="Please fill the field." ControlToValidate="TextBoxLastName"></asp:RequiredFieldValidator>
        </div>
        <div class="register__respondent__line">
            <div class="field_title">Date of birth:</div> 
            <asp:TextBox class="field" ID="TextBoxDOB" runat="server">dd/mm/yyyy</asp:TextBox>
        <asp:RequiredFieldValidator ID="DOB_validator" runat="server" ErrorMessage="Please fill the field." ControlToValidate="TextBoxDOB"></asp:RequiredFieldValidator>
        
        </div>
        <div class="register__respondent__line">
            <div class="field_title">Phone number:</div> <asp:TextBox class="field" ID="TextBoxPhoneNumber" runat="server" />
        <asp:RequiredFieldValidator ID="Phone__validator" runat="server" ErrorMessage="Please fill the field." ControlToValidate="TextBoxPhoneNumber"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="register__buttons">
        <a href="StartSurvey.aspx"><input id="anonymous__cancel" type="button" value="Cancel" /></a><asp:Button ID="ButtonRegister" runat="server" Text="Register" OnClick="ButtonRegister_Click" />
        
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>
