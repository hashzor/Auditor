<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Matrix5sLPA.aspx.cs" Inherits="Auditor.Matrix5sLPA" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>5S/LPA MATRIX</title>
    <link href="~/Styles/Loader.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dxpgMainTable caption {
            background-color: #64645B;
            font-weight: bold;
        }
    </style>
</head>
<body onload="BodyLoad()" onunload="BodyUnload()">
    <div id="loaderBox">
        <div class="loaderCircle"></div>
    </div>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery-3.3.1.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/CustomUtilities.js") %>"></script>
    <script type="text/javascript">
        var _returnUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.ViewSchedules) %>';
        var _timeout;
        var _delayMs = 30000;
        var _height = getHeight() - 220;
        function btnBack_Click(s, e) {
            window.location.replace(_returnUrl);
        }
        function pageControl_Init(s, e) {
            ChangeTab();
        }
        function pageControl_EndCallback(s, e) {
            ChangeTab();
        }
        function ChangeTab() {
            window.clearTimeout(_timeout);
            _timeout = window.setTimeout(
                function () {
                    var activeTab = pageControl.GetActiveTabIndex() + 1;
                    if (activeTab >= pageControl.GetTabCount()) {
                        activeTab = 0;
                    }
                    pageControl.PerformCallback();
                    pageControl.SetActiveTab(pageControl.GetTab(activeTab));
                },
                _delayMs
            );
        };
        function pivotMatrix_AdjustGrid(s, e) {
            s.SetHeight(_height);
        }
    </script>
    <form id="form1" runat="server">
        <div style="margin: 0 auto; width: 1000px;">
            <table style="margin: 0 auto; width: 100%;">
                <tr>
                    <td style="text-align: left; font-size: 50px; font-weight: bold;">5S/LPA AUDITS
                    </td>
                    <td style="text-align: right;">
                        <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" Text="" AutoPostBack="false" Width="80" Height="60">
                            <ClientSideEvents Click="btnBack_Click" />
                            <Image Url="~/Images/exit.png" Width="40" Height="40" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <dx:ASPxPageControl ID="pageControl" ClientInstanceName="pageControl" runat="server" ActiveTabIndex="0" Width="100%" Style="margin: 0 auto; float: none; margin-top: 20px;" Font-Size="24px" TabAlign="Center">
                <ClientSideEvents Init="pageControl_Init" EndCallback="pageControl_EndCallback" />
                <TabPages>
                    <dx:TabPage Text="AREAS">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <table style="text-align: center;">
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <dx:ASPxGridView ID="gvTargetsProduction" ClientInstanceName="gvTargetsProduction" runat="server" AutoGenerateColumns="False" DataSourceID="sdsTargetsProduction" KeyFieldName="id"
                                                Style="margin: 0 auto;" Width="450px" OnInit="gvTargetsProduction_Init">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="id" VisibleIndex="0" ReadOnly="True" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="area" VisibleIndex="1" Caption="AREA">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="subarea" VisibleIndex="2" Caption="SUBAREA">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="audit_target" VisibleIndex="3" Caption="PLACE">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="supervisor" VisibleIndex="4" Caption="PLACE SUPERVISOR">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                        <td>
                                            <dx:ASPxGridView ID="gvTargetsAdmin" ClientInstanceName="gvTargetsAdmin" runat="server" AutoGenerateColumns="False" DataSourceID="sdsTargetsAdmin" KeyFieldName="id"
                                                Style="margin: 0 auto;" Width="450px" OnInit="gvTargetsAdmin_Init">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="id" VisibleIndex="0" ReadOnly="True" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="area" VisibleIndex="1" Caption="AREA">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="subarea" VisibleIndex="2" Caption="SUBAREA">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="audit_target" VisibleIndex="3" Caption="PLACE">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="supervisor" VisibleIndex="4" Caption="PLACE SUPERVISOR">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxImage ID="imgLayout" ClientInstanceName="imgLayout" runat="server" ShowLoadingImage="true" Width="100%" ImageUrl="~/Matrices/plantlayout.png" Style="margin: 10px auto;">
                                            </dx:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="SCORES">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:WebChartControl ID="chartScores" ClientInstanceName="chartScores" runat="server" CrosshairEnabled="True" Height="550px" Width="900px" Style="margin: 0 auto;"
                                    DataSourceID="sdsScores" AutoLayout="True">
                                    <BorderOptions Visibility="True" />
                                    <Legend Name="Default Legend" MarkerMode="Marker"></Legend>
                                    <DiagramSerializable>
                                        <dx:XYDiagram>
                                            <AxisX VisibleInPanesSerializable="-1" Interlaced="True" Title-Text="WEEKS" Title-Visibility="True">
                                            </AxisX>
                                            <AxisY VisibleInPanesSerializable="-1" Interlaced="True" Title-Text="SCORES [%]" Title-Visibility="True">
                                                <WholeRange AlwaysShowZeroLevel="False"></WholeRange>
                                            </AxisY>
                                        </dx:XYDiagram>
                                    </DiagramSerializable>
                                    <SeriesSerializable>
                                        <dx:Series ArgumentDataMember="yearweek" Name="PLANT" ValueDataMembersSerializable="plant">
                                            <ViewSerializable>
                                                <dx:LineSeriesView>
                                                </dx:LineSeriesView>
                                            </ViewSerializable>
                                        </dx:Series>
                                        <dx:Series ArgumentDataMember="yearweek" Name="HALL 1" ValueDataMembersSerializable="hall1">
                                            <ViewSerializable>
                                                <dx:LineSeriesView>
                                                </dx:LineSeriesView>
                                            </ViewSerializable>
                                        </dx:Series>
                                        <dx:Series ArgumentDataMember="yearweek" Name="HALL 2" ValueDataMembersSerializable="hall2">
                                            <ViewSerializable>
                                                <dx:LineSeriesView>
                                                </dx:LineSeriesView>
                                            </ViewSerializable>
                                        </dx:Series>
                                        <dx:Series ArgumentDataMember="yearweek" Name="HALL 3" ValueDataMembersSerializable="hall3">
                                            <ViewSerializable>
                                                <dx:LineSeriesView>
                                                </dx:LineSeriesView>
                                            </ViewSerializable>
                                        </dx:Series>
                                        <dx:Series ArgumentDataMember="yearweek" Name="OTHERS" ValueDataMembersSerializable="others">
                                            <ViewSerializable>
                                                <dx:LineSeriesView>
                                                </dx:LineSeriesView>
                                            </ViewSerializable>
                                        </dx:Series>
                                    </SeriesSerializable>
                                    <Titles>
                                        <dx:ChartTitle Text="AVERAGE FROM RECENT WEEKS" />
                                    </Titles>
                                </dx:WebChartControl>
                                <dx:ASPxGridView ID="gvAvg" ClientInstanceName="gvAvg" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAvg"
                                    Style="margin: 0 auto; margin-top: 30px;" Width="400px" OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared" OnInit="gvAvg_Init">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="score" VisibleIndex="0" Caption="SCORE">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <dx:ASPxGridView ID="gvTop5" ClientInstanceName="gvTop5" runat="server" AutoGenerateColumns="False" DataSourceID="sdsTop5"
                                    Style="margin: 0 auto; margin-top: 30px;" Width="800px" OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared" OnInit="gvTop5_Init">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="area" VisibleIndex="1" Caption="AREA">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="subarea" VisibleIndex="2" Caption="SUBAREA">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="place" VisibleIndex="3" Caption="PLACE">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="score" VisibleIndex="4" Caption="SCORE">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="auditor" VisibleIndex="5" Caption="AUDITOR">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <dx:ASPxGridView ID="gvBot5" ClientInstanceName="gvBot5" runat="server" AutoGenerateColumns="False" DataSourceID="sdsBot5"
                                    Style="margin: 0 auto; margin-top: 30px;" Width="800px" OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared" OnInit="gvBot5_Init">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="area" VisibleIndex="1" Caption="AREA">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="subarea" VisibleIndex="2" Caption="SUBAREA">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="place" VisibleIndex="3" Caption="PLACE">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="score" VisibleIndex="4" Caption="SCORE">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="auditor" VisibleIndex="5" Caption="AUDITOR">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="MATRIX">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxPivotGrid ID="pivotMatrix" ClientInstanceName="pivotMatrix" runat="server" ClientIDMode="AutoID" DataSourceID="sdsPivot" Width="100%" Height="400px" Style="margin: 0 auto;"
                                    OnHtmlCellPrepared="pivotMatrix_HtmlCellPrepared" OnInit="pivotMatrix_Init">
                                    <Fields>
                                        <dx:PivotGridField ID="fieldaudittargetarea" Area="RowArea" AreaIndex="0" FieldName="audit_target_area" Name="fieldaudittargetarea" Caption="AREA" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldaudittargetsubarea" Area="RowArea" AreaIndex="1" FieldName="audit_target_subarea" Name="fieldaudittargetsubarea" Caption="SUBAREA" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldaudittarget" Area="RowArea" AreaIndex="2" FieldName="audit_target" Name="fieldaudittarget" Caption="PLACE" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldyearweek" Area="ColumnArea" AreaIndex="0" FieldName="yearweek" Name="fieldyearweek" Caption="WEEK" ValueStyle-Font-Bold="true">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldauditorfullname" Area="DataArea" AreaIndex="0" FieldName="auditor_full_name" Name="auditorfullname" Caption="AUDITOR" SummaryType="Min">
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldauditscore" Area="DataArea" AreaIndex="1" FieldName="audit_score" Name="fieldauditscore" Caption="SCORE" CellFormat-FormatType="Numeric" CellFormat-FormatString="D" ValueFormat-FormatString="D" ValueFormat-FormatType="Numeric" SummaryType="Min">
                                        </dx:PivotGridField>
                                    </Fields>
                                    <ClientSideEvents Init="pivotMatrix_AdjustGrid" />
                                </dx:ASPxPivotGrid>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="SUPERVISORS MATRIX">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxPivotGrid ID="pivotMatrixSV" ClientInstanceName="pivotMatrixSV" runat="server" ClientIDMode="AutoID" DataSourceID="sdsPivotSV" Width="100%" Height="400px" Style="margin: 0 auto;"
                                    OnHtmlCellPrepared="pivotMatrixSV_HtmlCellPrepared" OnInit="pivotMatrixSV_Init">
                                    <Fields>
                                        <dx:PivotGridField ID="fieldaudittargetareaSV" Area="RowArea" AreaIndex="0" FieldName="audit_target_area" Name="fieldaudittargetarea" Caption="AREA" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldaudittargetsubareaSV" Area="RowArea" AreaIndex="1" FieldName="audit_target_subarea" Name="fieldaudittargetsubarea" Caption="SUBAREA" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldaudittargetsupervisorSV" Area="RowArea" AreaIndex="2" FieldName="audit_target_supervisor" Name="fieldaudittargetsupervisor" Caption="SUPERVISOR" ValueStyle-Font-Bold="true" Width="100">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldyearweekSV" Area="ColumnArea" AreaIndex="0" FieldName="yearweek" Name="fieldyearweek" Caption="WEEK" ValueStyle-Font-Bold="true">
                                            <ValueStyle Font-Bold="True"></ValueStyle>
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldaudittargetSV" Area="DataArea" AreaIndex="0" FieldName="audit_target" Name="fieldaudittarget" Caption="PLACE" SummaryType="Min">
                                        </dx:PivotGridField>
                                        <dx:PivotGridField ID="fieldauditscoreSV" Area="DataArea" AreaIndex="1" FieldName="audit_score" Name="fieldauditscore" Caption="SCORE" CellFormat-FormatType="Numeric" CellFormat-FormatString="D" ValueFormat-FormatString="D" ValueFormat-FormatType="Numeric" SummaryType="Min">
                                        </dx:PivotGridField>
                                    </Fields>
                                    <ClientSideEvents Init="pivotMatrix_AdjustGrid" />
                                </dx:ASPxPivotGrid>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
            <asp:SqlDataSource ID="sdsTargetsProduction" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT  [id],
                                       [audit_target],
                                       [area],
                                       [subarea],
                                       [supervisor]
                                FROM   [setts_audit_targets]
                                WHERE [audit_type] = @audit_type
                                ORDER BY [area],[subarea],[audit_target]">
                <SelectParameters>
                    <asp:Parameter Name="audit_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsTargetsAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT  [id],
                                       [audit_target],
                                       [area],
                                       [subarea],
                                       [supervisor]
                                FROM   [setts_audit_targets]
                                WHERE [audit_type] = @audit_type
                                ORDER BY [area],[subarea],[audit_target]">
                <SelectParameters>
                    <asp:Parameter Name="audit_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsPivot" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT [schedule_id],
                                       [audit_year],
                                       [audit_week],
                                       [audit_target],
                                       [audit_target_area],
                                       [audit_target_subarea],
                                       [auditor_full_name],
                                       [audit_id],
                                       [audit_score],
	                                  CONVERT(NVARCHAR(10),[audit_year]) + ' W-' + CONVERT(NVARCHAR(10),[audit_week]) AS [yearweek]
                                FROM   [vAuditResults]
                                WHERE  [audit_year] * 100 + [audit_week] BETWEEN DATEPART(YEAR, DATEADD([WK], -3, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -3, GETDATE())) AND DATEPART(YEAR, DATEADD([WK], 3, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], 3, GETDATE()))
	                                   AND [schedule_name] IN (@schedule_name1,@schedule_name2)">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name1" Type="String" />
                    <asp:Parameter Name="schedule_name2" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsPivotSV" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT [schedule_id],
                                       [audit_year],
                                       [audit_week],
                                       [audit_target],
                                       [audit_target_area],
                                       [audit_target_subarea],
                                       [auditor_full_name],
                                       [audit_id],
                                       [audit_score],
                                       [audit_target_supervisor],
	                                  CONVERT(NVARCHAR(10),[audit_year]) + ' W-' + CONVERT(NVARCHAR(10),[audit_week]) AS [yearweek]
                                FROM   [vAuditResults]
                                WHERE  [audit_year] * 100 + [audit_week] BETWEEN DATEPART(YEAR, DATEADD([WK], -3, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -3, GETDATE())) AND DATEPART(YEAR, DATEADD([WK], 3, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], 3, GETDATE()))
	                                   AND [schedule_name] IN (@schedule_name)">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsScores" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand=" SELECT
                                       CONVERT(NVARCHAR(10), [weeks].[audit_year])+' W-'+CONVERT(NVARCHAR(10), [weeks].[audit_week]) AS [yearweek],
                                       [plant].[avg_score] AS   [plant],
                                       [hall1].[avg_score] AS   [hall1],
                                       [hall2].[avg_score] AS   [hall2],
                                       [hall3].[avg_score] AS   [hall3],
                                       [others].[avg_score] AS  [others]
                                FROM
                                           (
                                            SELECT DISTINCT
                                                   [audit_year],
                                                   [audit_week]
                                            FROM   [vAuditResults]
                                            WHERE  [audit_year] * 100 + [audit_week] BETWEEN DATEPART(YEAR, DATEADD([WK], -10, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -10, GETDATE())) AND DATEPART(YEAR, DATEADD([WK], -1, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -1, GETDATE()))
                                                   AND [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                           ) AS [weeks]
                                           LEFT JOIN
                                                    (
                                                     SELECT
                                                            [audit_year],
                                                            [audit_week],
                                                            FLOOR(AVG([audit_score])) AS [avg_score]
                                                     FROM   [vAuditResults]
                                                     WHERE  [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                                     GROUP BY
                                                              [audit_year],
                                                              [audit_week]
                                                    ) AS [plant] ON [plant].[audit_year] = [weeks].[audit_year]
                                                                    AND [plant].[audit_week] = [weeks].[audit_week]
                                           LEFT JOIN
                                                    (
                                                     SELECT
                                                            [audit_year],
                                                            [audit_week],
                                                            FLOOR(AVG([audit_score])) AS [avg_score]
                                                     FROM   [vAuditResults]
                                                     WHERE  [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                                     AND [audit_target_area] = 'HALA 1'
                                                     GROUP BY
                                                              [audit_year],
                                                              [audit_week]
                                                    ) AS [hall1] ON [hall1].[audit_year] = [weeks].[audit_year]
                                                                    AND [hall1].[audit_week] = [weeks].[audit_week]
                                           LEFT JOIN
                                                    (
                                                     SELECT
                                                            [audit_year],
                                                            [audit_week],
                                                            FLOOR(AVG([audit_score])) AS [avg_score]
                                                     FROM   [vAuditResults]
                                                     WHERE  [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                                     AND [audit_target_area] = 'HALA 2'
                                                     GROUP BY
                                                              [audit_year],
                                                              [audit_week]
                                                    ) AS [hall2] ON [hall2].[audit_year] = [weeks].[audit_year]
                                                                    AND [hall2].[audit_week] = [weeks].[audit_week]
                                           LEFT JOIN
                                                    (
                                                     SELECT
                                                            [audit_year],
                                                            [audit_week],
                                                            FLOOR(AVG([audit_score])) AS [avg_score]
                                                     FROM   [vAuditResults]
                                                     WHERE  [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                                     AND [audit_target_area] = 'HALA 3'
                                                     GROUP BY
                                                              [audit_year],
                                                              [audit_week]
                                                    ) AS [hall3] ON [hall3].[audit_year] = [weeks].[audit_year]
                                                                    AND [hall3].[audit_week] = [weeks].[audit_week]
                                           LEFT JOIN
                                                    (
                                                     SELECT
                                                            [audit_year],
                                                            [audit_week],
                                                            FLOOR(AVG([audit_score])) AS [avg_score]
                                                     FROM   [vAuditResults]
                                                     WHERE  [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                                     AND [audit_target_area] = 'INNE'
                                                     GROUP BY
                                                              [audit_year],
                                                              [audit_week]
                                                    ) AS [others] ON [others].[audit_year] = [weeks].[audit_year]
                                                                     AND [others].[audit_week] = [weeks].[audit_week];">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name1" Type="String" />
                    <asp:Parameter Name="schedule_name2" Type="String" />
                    <asp:Parameter Name="schedule_name3" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsTop5" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT TOP 5
                                       [audit_target_area] AS    [area],
                                       [audit_target_subarea] AS [subarea],
                                       [audit_target] AS         [place],
                                       FLOOR([audit_score]) AS   [score],
                                       [auditor_full_name] AS    [auditor]
                                FROM   [vAuditResults]
                                WHERE  [audit_score] IS NOT NULL
                                       AND [audit_year] * 100 + [audit_week] = DATEPART(YEAR, DATEADD([WK], -1, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -1, GETDATE()))
                                       AND [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                ORDER BY
                                         [score] DESC;">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name1" Type="String" />
                    <asp:Parameter Name="schedule_name2" Type="String" />
                    <asp:Parameter Name="schedule_name3" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsBot5" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT TOP 5
                                       [audit_target_area] AS    [area],
                                       [audit_target_subarea] AS [subarea],
                                       [audit_target] AS         [place],
                                       FLOOR([audit_score]) AS   [score],
                                       [auditor_full_name] AS    [auditor]
                                FROM   [vAuditResults]
                                WHERE  [audit_score] IS NOT NULL
                                       AND [audit_year] * 100 + [audit_week] = DATEPART(YEAR, DATEADD([WK], -1, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -1, GETDATE()))
                                       AND [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                ORDER BY
                                         [score];">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name1" Type="String" />
                    <asp:Parameter Name="schedule_name2" Type="String" />
                    <asp:Parameter Name="schedule_name3" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsAvg" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                SelectCommand="SELECT FLOOR(AVG([audit_score])) AS [score]
                                FROM [vAuditResults]
                                WHERE [audit_score] IS NOT NULL
                                       AND [audit_year] * 100 + [audit_week] = DATEPART(YEAR, DATEADD([WK], -1, GETDATE())) * 100 + DATEPART(WEEK, DATEADD([WK], -1, GETDATE()))
                                       AND [schedule_name] IN (@schedule_name1, @schedule_name2, @schedule_name3)
                                GROUP BY [audit_year],[audit_week]">
                <SelectParameters>
                    <asp:Parameter Name="schedule_name1" Type="String" />
                    <asp:Parameter Name="schedule_name2" Type="String" />
                    <asp:Parameter Name="schedule_name3" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>