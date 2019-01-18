<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseAudits.aspx.cs" Inherits="Auditor.BrowseAudits" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var _printerUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.DocsPrinter) %>';

        function gvAudits_Init(s, e) {
            AdjustGridViewHeight(s, e, 120);
        }
        function gvAuditDetails_Init(s, e) {
            s.SetHeight(350);
        }
        function gvAudits_ToolbarItemClick(s, e) {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var focusedRowKey = s.GetRowKey(s.GetFocusedRowIndex());
            var button = e.item.name;
            switch (button) {
                case '<%=Auditor.ToolbarButtons.PrintAudit %>':
                    if (focusedRowIndex >= 0) {
                        var url = _printerUrl + "?id=" + focusedRowKey;
                        window.open(url, '_blank');
                    }
                    else {
                        alert("Choose row!");
                    }
                    break;
                case '<%=Auditor.ToolbarButtons.DeleteAudit %>':
                    e.processOnServer = confirm("Are you sure you want to delete the audit?");
                    break;
                default:
                    break;
            }
        }
        function gvAudits_EndCallback(s, e) {
            if (s.cp_refresh != null) {
                s.cp_refresh = null;
                s.Refresh();
            }
        }
        function dxFileManager_SelectedFileOpened(s, e) {
            if (e.file) {
                picCallback.PerformCallback(e.file.GetFullName());
            }
        }
        function picCallback_CallbackComplete(s, e) {
            var data = e.result.split("|");
            if (data.length >= 3) {
                var width = data[0];
                var height = data[1];
                var url = data[2];
                var position_hor = ((getWidth() - width) / 2) - 10;
                var position_vert = 20;
                ShowPopup(url, width, height, position_hor, position_vert);
            }
            else {
                alert("Display error!");
            }
        }
        function ShowPopup(url, width, height, position_hor, position_vert) {
            if (picPopup.GetClientVisible()) picPopup.Hide();
            picPopup.ShowAtPos(position_hor, position_vert);
            picPopup.SetHeight(height);
            picPopup.SetWidth(width);
            imgPic.SetSize(width, height);
            imgPic.SetImageUrl(url);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="gvAudits" ClientInstanceName="gvAudits" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAudits" KeyFieldName="id" Style="margin: 0 auto; margin-top: 10px;" Width="100%"
        OnInit="gvAudits_Init" OnDetailRowExpandedChanged="gvAudits_DetailRowExpandedChanged" OnToolbarItemClick="gvAudits_ToolbarItemClick">
        <ClientSideEvents Init="gvAudits_Init" ToolbarItemClick="gvAudits_ToolbarItemClick" EndCallback="gvAudits_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="80">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_type" VisibleIndex="1" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_type_name" VisibleIndex="2" Caption="AUDIT NAME">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="AUDIT TARGET" VisibleIndex="3">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="audit_target" VisibleIndex="0" Caption="TARGET">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_target_area" VisibleIndex="1" Caption="AREA">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_target_subarea" VisibleIndex="2" Caption="SUBAREA">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_target_section" VisibleIndex="3" Caption="SECTION">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_shift_name" VisibleIndex="4" Caption="SHIFT">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn FieldName="auditor_login" VisibleIndex="4" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="auditor_full_name" VisibleIndex="5" Caption="AUDITOR">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="DATES" VisibleIndex="6">
                <Columns>
                    <dx:GridViewDataDateColumn FieldName="start_date" VisibleIndex="0" Caption="START" Width="100">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="end_date" VisibleIndex="1" Caption="END" Width="100">
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn FieldName="score" VisibleIndex="8" Caption="SCORE [%]" Width="100">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="9" Visible="false">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxGridView ID="gvAuditDetails" ClientInstanceName="gvAuditDetails" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditDetails" KeyFieldName="id" Style="margin: 0 auto;" Width="100%"
                                OnInit="gvAuditDetails_Init" OnBeforePerformDataSelect="gvAuditDetails_BeforePerformDataSelect">
                                <ClientSideEvents Init="gvAuditDetails_Init" />
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="audit_id" VisibleIndex="1" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewBandColumn Caption="GROUP" VisibleIndex="2">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="group_position" VisibleIndex="0" Caption="NO" Width="60">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="group_name" VisibleIndex="1" Caption="GROUP" Width="100">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewBandColumn Caption="QUESTION" VisibleIndex="3">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="question_position" VisibleIndex="0" Caption="NO" Width="60">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="question" VisibleIndex="1" Caption="QUESTION">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:GridViewBandColumn>
                                    <dx:GridViewDataTextColumn FieldName="answer" VisibleIndex="4" Caption="ANSWER" Width="100">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="5" Caption="COMMENT">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="action" VisibleIndex="10" Caption="ACTION">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                        <td>
                            <dx:ASPxFileManager ID="dxFileManager" ClientInstanceName="dxFileManager" runat="server" Style="margin: 0 auto;" Width="300px" Height="350px" Styles-FolderContainer-Width="150px"
                                OnInit="dxFileManager_Init">
                                <ClientSideEvents SelectedFileOpened="dxFileManager_SelectedFileOpened" />
                            </dx:ASPxFileManager>
                        </td>
                    </tr>
                </table>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sdsAudits" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [id], [audit_type], [audit_type_name], [audit_target], [auditor_login], [auditor_full_name], [start_date], [end_date],
                              [score], [comment], [audit_target_area], [audit_target_subarea], [audit_target_section], [audit_shift_name]
                        FROM [audits]
                        ORDER BY [id] DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAuditDetails" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [ad].[id], [ad].[audit_id], [ad].[group_position], [ad].[group_name], [ad].[question_position], [ad].[question], [ad].[answer],
                              [ad].[comment], [act].[action]
                        FROM [audit_details] [ad]
                            LEFT JOIN [actions] [act] ON [act].[audit_id] = [ad].[audit_id] AND [act].[audit_detail_id] = [ad].[id]
                        WHERE [ad].[audit_id] = @audit_id
                        ORDER BY [ad].[group_position], [ad].[question_position]">
        <SelectParameters>
            <asp:Parameter Name="audit_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dx:ASPxCallback ID="picCallback" ClientInstanceName="picCallback" runat="server" OnCallback="picCallback_Callback">
        <ClientSideEvents CallbackComplete="picCallback_CallbackComplete" />
    </dx:ASPxCallback>
    <dx:ASPxPopupControl ID="picPopup" runat="server" ClientInstanceName="picPopup" CloseAction="OuterMouseClick" ShowHeader="false" Modal="true">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <div style="margin: 0 auto;">
                    <dx:ASPxImage runat="server" ID="imgPic" ClientInstanceName="imgPic">
                    </dx:ASPxImage>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>