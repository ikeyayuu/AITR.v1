<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="AITR.respondent.Survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginbox">
        <asp:Label class="page__title" ID="page_title" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label class="question__title" ID="question_title" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label class="question__description" ID="question_description" runat="server"></asp:Label>
        <br />
        <asp:Label class="question__option" ID="question_option" runat="server"></asp:Label>
        <div class="options">
            <asp:PlaceHolder ID="Option" runat="server"></asp:PlaceHolder>
        </div>
        <div class="survey__buttons__area">        
            <asp:Button ID="button_previous" runat="server" Text="Previous" OnClick="button_previous_Click" />
            <asp:Button ID="button_next" runat="server" Text="Next" OnClick="button_next_Click" />
            <asp:Button ID="button_finish" runat="server" Text="Finish" />
        </div>
    </div>
    <asp:GridView ID="dbTableView" runat="server" />
    <asp:GridView ID="dbSubQuestion" runat="server" />


</asp:Content>
