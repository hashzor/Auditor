<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocsPrinter.aspx.cs" Inherits="Auditor.DocsPrinter" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p style="text-align: center; margin-top: 20px;">
        <dx:ASPxLabel ID="lblInfo" runat="server" Text="" Font-Size="X-Large"></dx:ASPxLabel>
    </p>
</asp:Content>