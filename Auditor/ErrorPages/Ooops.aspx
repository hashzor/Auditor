<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Ooops.aspx.cs" Inherits="Auditor.Ooops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="accountTable">
        <tr>
            <td>
                <img alt="erroricon" src="../Images/error_icon.png" />
            </td>
            <td>
                <p>
                    <b>The application encountered unexpected error.</b>
                </p>
                <p>
                    If this problem will repeat please contact with your IT support.
                </p>
            </td>
        </tr>
    </table>
</asp:Content>