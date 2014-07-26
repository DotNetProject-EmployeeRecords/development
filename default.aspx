<%@ Page Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" Inherits="_Default" Title="Welcome User!" Codebehind="default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="Server">
    <h1 class="title-regular clearfix">
        Jarrad's HOME</h1>
    <asp:Literal runat="server" EnableViewState="False" ID="labelMessage"></asp:Literal>
    <p>
        Jarrad's Technical Evaluation</p>
</asp:Content>
