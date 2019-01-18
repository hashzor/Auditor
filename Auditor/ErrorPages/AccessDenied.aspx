<%@ Page Title="AccessDenied" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccessDenied.aspx.cs" Inherits="Auditor.AccessDenied" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="accountTable">
        <tr>
            <td style="text-align: center">
                <p>
                    <b>You do not have access to this application!</b>
                    <br />
                    <img src="../Images/lock.png" alt="no access" />
                    <br />
                    Contact the IT department.
                </p>
            </td>
        </tr>
    </table>
</asp:Content>