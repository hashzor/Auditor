<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSchedules.aspx.cs" Inherits="Auditor.ViewSchedules" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var _printerUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.DocsPrinter) %>';
        var _checkUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.BrowseAudits) %>';

        function gvSchedules_Init(s, e) {
            AdjustGridViewHeight(s, e, 120);
        }
        function gvSchedules_ToolbarItemClick(s, e) {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var focusedRowKey = s.GetRowKey(s.GetFocusedRowIndex());
            var button = e.item.name;
            switch (button) {
                case '<%=Auditor.ToolbarButtons.PrintAudit %>':
                    if (focusedRowIndex >= 0) {
                        s.GetRowValues(focusedRowIndex, "audit_id", Print_OnGetRowValues)
                    }
                    else {
                        alert("Choose row!");
                    }
                    break;
                case '<%=Auditor.ToolbarButtons.CheckAudit %>':
                    if (focusedRowIndex >= 0) {
                        s.GetRowValues(focusedRowIndex, "audit_id", Check_OnGetRowValues)
                    }
                    else {
                        alert("Choose row!");
                    }
                    break;
                default:
                    break;
            }
        }
        function Print_OnGetRowValues(auditId) {
            if (auditId != null) {
                var url = _printerUrl + "?id=" + auditId;
                window.open(url, '_blank');
            }
            else {
                alert("Audit can not be printed!");
            }
        }
        function Check_OnGetRowValues(auditId) {
            if (auditId != null) {
                var url = _checkUrl + "?id=" + auditId;
                window.open(url, '_blank');
            }
            else {
                alert("Audit can not be displayed!");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="gvSchedules" ClientInstanceName="gvSchedules" runat="server" AutoGenerateColumns="False" DataSourceID="sdsSchedules" KeyFieldName="schedule_id" Style="margin: 0 auto; margin-top: 10px;" Width="100%"
        OnInit="gvSchedules_Init">
        <ClientSideEvents Init="gvSchedules_Init" ToolbarItemClick="gvSchedules_ToolbarItemClick" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="schedule_id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="80">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="schedule_name" VisibleIndex="1" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="schedule_description" VisibleIndex="2" Caption="SCHEDULE" Width="120">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_type" VisibleIndex="3" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_type_name" VisibleIndex="4" Caption="AUDIT NAME">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn VisibleIndex="5" Caption="AUDIT DATE">
                <Columns>
                    <dx:GridViewDataDateColumn FieldName="audit_date" VisibleIndex="0" Caption="DATE" Width="80">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_year" VisibleIndex="1" Caption="YEAR" Width="80">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_month" VisibleIndex="2" Caption="MONTH" Width="80">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_week" VisibleIndex="3" Caption="WEEK" Width="80">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="AUDIT TARGET" VisibleIndex="6">
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
                    <dx:GridViewDataTextColumn FieldName="audit_target_supervisor_login" VisibleIndex="5" Visible="false">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="audit_target_supervisor" VisibleIndex="6" Caption="SUPERVISOR">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn FieldName="auditor_login" VisibleIndex="7" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="auditor_full_name" VisibleIndex="8" Caption="AUDITOR">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="auditor_email" VisibleIndex="9" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_id" VisibleIndex="10" Caption="AUDIT ID">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="audit_score" VisibleIndex="11" Caption="SCORE [%]">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sdsSchedules" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT
                               [schedule_id],
                               [schedule_name],
                               [schedule_description],
                               [audit_type],
                               [audit_type_name],
                               [audit_date],
                               [audit_year],
                               [audit_month],
                               [audit_week],
                               [audit_target],
                               [audit_target_area],
                               [audit_target_subarea],
                               [audit_target_section],
                               [audit_target_supervisor_login],
                               [audit_target_supervisor],
                               [auditor_login],
                               [auditor_full_name],
                               [auditor_email],
                               [audit_id],
                               [audit_score],
                               [audit_shift_name]
                        FROM  [vAuditResults]
                        ORDER BY [audit_date],[audit_year],[audit_month],[audit_week],[audit_target],[auditor_login]"></asp:SqlDataSource>
</asp:Content>