<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerformAudit.aspx.cs" Inherits="Auditor.PerformAudit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var _cameraUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.Camera) %>';
        var _escapeUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.Default) %>';

        function gvAudit_Init(s, e) {
            AdjustGridViewHeight(s, e, 130);
        }
        function gvAudit_EndCallback(s, e) {
            if (s.cp_move != null) {
                s.cp_move = null;
                var focusedRowIndex = s.GetFocusedRowIndex();
                var lastRowIndex = s.GetTopVisibleIndex() + s.GetVisibleRowsOnPage() - 1;
                if (focusedRowIndex < lastRowIndex) {
                    s.SetFocusedRowIndex(focusedRowIndex + 1);
                }
                else {
                    s.SetFocusedRowIndex(s.GetTopVisibleIndex());
                    s.PerformCallback('<%=Auditor.ToolbarButtons.NextPage %>');
                }
            }
            if (s.cp_refresh != null) {
                s.cp_refresh = null;
                s.Refresh();
            }
            if (s.cp_launch_camera != null) {
                s.cp_launch_camera = null;
                window.location.replace(_cameraUrl);
            }
            if (s.cp_escape_page != null) {
                s.cp_escape_page = null;
                window.location.replace(_escapeUrl);
            }
            if (s.IsEditing()) {
                var form = s.GetPopupEditForm();
                form.PopUp.AddHandler(function (sender, eventArgs) {
                    commentEditor.Focus();
                });
            }
        }
        function gvAudit_ToolbarItemClick(s, e) {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var focusedRowKey = s.GetRowKey(s.GetFocusedRowIndex());
            var button = e.item.name;
            switch (button) {
                case '<%=Auditor.ToolbarButtons.NextPage %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.PrevPage %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.LangPol %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.LangEng %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.AnswerOK %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.AnswerNOK %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.AnswerNC %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.AnswerNA %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.Camera %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.DeleteAudit %>':
                    e.processOnServer = confirm("Are you sure you want to delete the audit?");
                    break;
                case '<%=Auditor.ToolbarButtons.PrintAudit %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.EndAudit %>':
                    e.processOnServer = confirm("Are you sure you want to end the audit?");
                    break;
                case '<%=Auditor.ToolbarButtons.Edit %>':
                    if (focusedRowIndex >= 0) {
                        s.StartEditRow(focusedRowIndex);
                    }
                    else {
                        alert("Choose row!");
                    }
                    break;
            }
        }
    </script>
    <style type="text/css">
        .dxgvTable caption {
            background-color: #64645B;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="gvAudit" ClientInstanceName="gvAudit" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAudit" KeyFieldName="id" Style="margin: 10px auto;" Width="100%"
        OnInit="gvAudit_Init" OnHtmlRowPrepared="gvAudit_HtmlRowPrepared" OnToolbarItemClick="gvAudit_ToolbarItemClick" OnRowUpdating="gvAudit_RowUpdating" OnCustomCallback="gvAudit_CustomCallback">
        <ClientSideEvents Init="gvAudit_Init" ToolbarItemClick="gvAudit_ToolbarItemClick" EndCallback="gvAudit_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_id" VisibleIndex="1" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="group_position" VisibleIndex="2" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="group_name" VisibleIndex="3" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="group_name_ENG" VisibleIndex="4" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="question_position" VisibleIndex="5" Caption="NO" Width="80">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="question" VisibleIndex="6" Caption="QUESTION">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="question_ENG" VisibleIndex="7" Caption="QUESTION">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="answer" VisibleIndex="8" Caption="ANSWER" Width="150">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="9" Caption="COMMENT">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn FieldName="answer_OK" VisibleIndex="10" Visible="false">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="answer_NOK" VisibleIndex="11" Visible="false">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="answer_NC" VisibleIndex="12" Visible="false">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="answer_NA" VisibleIndex="13" Visible="false">
            </dx:GridViewDataCheckColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table style="width: 90%; margin: 0 auto;">
                    <tr>
                        <td style="padding: 10px 0;">
                            <dx:ASPxMemo runat="server" ID="commentEditor" ClientInstanceName="commentEditor" Text='<%# Eval("comment")%>' Width="100%" Height="50px" Caption="COMMENT" CaptionSettings-Position="Top" Font-Size="18px">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 15px 200px;">
                            <div style="float: left">
                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                            </div>
                            <div style="float: right">
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                            </div>
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <div style="width: 100%; text-align: center;">
        <b>OK</b> - compatibility, <b>NOK</b> - incompatibility, <b>NC</b> - incompatibility removed during the audit, <b>NA</b> - not applicable
    </div>
    <asp:SqlDataSource ID="sdsAudit" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [id], [audit_id], [group_position], [group_name], [group_name_ENG], [question_position], [question], [question_ENG], [answer], [comment], [answer_OK], [answer_NOK], [answer_NC], [answer_NA]
                        FROM [audit_details]
                        WHERE [audit_id] = @audit_id
                        ORDER BY [audit_id], [group_position],[question_position]"
        UpdateCommand="UPDATE [audit_details] SET [comment] = @comment WHERE [id] = @id">
        <UpdateParameters>
            <asp:Parameter Name="comment" Type="String" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="audit_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>