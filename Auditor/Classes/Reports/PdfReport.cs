using DevExpress.DataAccess;
using DevExpress.DataAccess.Sql;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;

/// <summary>
/// Summary description for PdfReport
/// </summary>
public class PdfReport : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;

    private TopMarginBand TopMargin;

    private BottomMarginBand BottomMargin;

    private XRTable xrTable1;

    private XRTableRow xrTableRow1;

    private XRTableCell xrTableCell1;

    private XRTableCell xrTableCell2;

    private XRTableRow xrTableRow2;

    private XRTableCell xrTableCell3;

    private XRTableCell xrTableCell4;

    private SqlDataSource sdsData;

    private XRLabel xrLabel2;

    private XRLabel xrLabel1;

    private XRPageInfo xrPageInfo1;

    private XRPageInfo xrPageInfo2;

    private XRTable xrTable3;

    private XRTableRow xrTableRow6;

    private XRTableCell xrTableCell14;

    private XRTableCell xrTableCell15;

    private XRTableCell xrTableCell16;

    private XRTable xrTable2;

    private XRTableRow xrTableRow5;

    private XRTableCell xrTableCell10;

    private XRTableCell xrTableCell11;

    private XRTableCell xrTableCell12;

    private ReportHeaderBand ReportHeader;

    private DetailBand Detail2;

    private DetailReportBand DetailReport1;

    private DetailBand Detail1;

    private DetailReportBand DetailReport;

    private XRTable xrTable4;

    private XRTableRow xrTableRow9;

    private XRTableCell xrTableCell21;

    private XRTableCell xrTableCell22;

    private XRTableRow xrTableRow10;

    private XRTableCell xrTableCell23;

    private XRTableCell xrTableCell24;

    private Parameter id;

    private IContainer components;
    /// <summary>
    /// Required designer variable.
    /// </summary>

    public PdfReport(int auditId, string lang)
    {
        InitializeComponent(lang);
        this.id.Value = auditId;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent(string lang)
    {
        this.components = new Container();
        CustomSqlQuery customSqlQuery = new CustomSqlQuery();
        QueryParameter queryParameter = new QueryParameter();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PdfReport));
        CustomSqlQuery customSqlQuery2 = new CustomSqlQuery();
        CustomSqlQuery customSqlQuery3 = new CustomSqlQuery();
        MasterDetailInfo masterDetailInfo = new MasterDetailInfo();
        RelationColumnInfo relationColumnInfo = new RelationColumnInfo();
        MasterDetailInfo masterDetailInfo2 = new MasterDetailInfo();
        RelationColumnInfo relationColumnInfo2 = new RelationColumnInfo();
        RelationColumnInfo relationColumnInfo3 = new RelationColumnInfo();
        RelationColumnInfo relationColumnInfo4 = new RelationColumnInfo();
        RelationColumnInfo relationColumnInfo5 = new RelationColumnInfo();
        this.Detail = new DetailBand();
        this.TopMargin = new TopMarginBand();
        this.BottomMargin = new BottomMarginBand();
        this.sdsData = new SqlDataSource(this.components);
        this.xrTable1 = new XRTable();
        this.xrTableRow1 = new XRTableRow();
        this.xrTableCell1 = new XRTableCell();
        this.xrTableCell2 = new XRTableCell();
        this.xrTableRow2 = new XRTableRow();
        this.xrTableCell3 = new XRTableCell();
        this.xrTableCell4 = new XRTableCell();
        this.xrLabel1 = new XRLabel();
        this.xrLabel2 = new XRLabel();
        this.xrPageInfo2 = new XRPageInfo();
        this.xrPageInfo1 = new XRPageInfo();
        this.xrTable2 = new XRTable();
        this.xrTableRow5 = new XRTableRow();
        this.xrTableCell10 = new XRTableCell();
        this.xrTableCell11 = new XRTableCell();
        this.xrTableCell12 = new XRTableCell();
        this.xrTable3 = new XRTable();
        this.xrTableRow6 = new XRTableRow();
        this.xrTableCell14 = new XRTableCell();
        this.xrTableCell15 = new XRTableCell();
        this.xrTableCell16 = new XRTableCell();
        this.ReportHeader = new ReportHeaderBand();
        this.Detail2 = new DetailBand();
        this.DetailReport1 = new DetailReportBand();
        this.Detail1 = new DetailBand();
        this.DetailReport = new DetailReportBand();
        this.xrTable4 = new XRTable();
        this.xrTableRow9 = new XRTableRow();
        this.xrTableCell21 = new XRTableCell();
        this.xrTableCell22 = new XRTableCell();
        this.xrTableRow10 = new XRTableRow();
        this.xrTableCell23 = new XRTableCell();
        this.xrTableCell24 = new XRTableCell();
        this.id = new Parameter();
        ((ISupportInitialize)this.xrTable1).BeginInit();
        ((ISupportInitialize)this.xrTable2).BeginInit();
        ((ISupportInitialize)this.xrTable3).BeginInit();
        ((ISupportInitialize)this.xrTable4).BeginInit();
        ((ISupportInitialize)this).BeginInit();
        this.Detail.Expanded = false;
        this.Detail.HeightF = 100f;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
        this.Detail.TextAlignment = TextAlignment.TopLeft;
        this.TopMargin.Controls.AddRange(new XRControl[]
        {
               this.xrTable4,
               this.xrTable1
        });
        this.TopMargin.HeightF = 110f;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
        this.TopMargin.TextAlignment = TextAlignment.TopLeft;
        this.BottomMargin.Controls.AddRange(new XRControl[]
        {
               this.xrPageInfo1,
               this.xrPageInfo2
        });
        this.BottomMargin.HeightF = 70f;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
        this.BottomMargin.TextAlignment = TextAlignment.TopLeft;
        this.sdsData.ConnectionName = "csApp";
        this.sdsData.Name = "sdsData";
        customSqlQuery.Name = "audits";
        queryParameter.Name = "id";
        queryParameter.Type = typeof(Expression);
        queryParameter.Value = new Expression("[Parameters.id]", typeof(int));
        customSqlQuery.Parameters.Add(queryParameter);
        customSqlQuery.Sql = componentResourceManager.GetString("customSqlQuery1.Sql");
        customSqlQuery2.Name = "audit_details";
        customSqlQuery2.Sql = componentResourceManager.GetString("customSqlQuery2.Sql");
        customSqlQuery3.MetaSerializable = "<Meta X=\"90\" Y=\"70\" Width=\"100\" Height=\"105\" />";
        customSqlQuery3.Name = "audit_groups";
        customSqlQuery3.Sql = componentResourceManager.GetString("customSqlQuery3.Sql");
        this.sdsData.Queries.AddRange(new SqlQuery[]
        {
               customSqlQuery,
               customSqlQuery2,
               customSqlQuery3
        });
        masterDetailInfo.DetailQueryName = "audit_groups";
        relationColumnInfo.NestedKeyColumn = "audit_id";
        relationColumnInfo.ParentKeyColumn = "id";
        masterDetailInfo.KeyColumns.Add(relationColumnInfo);
        masterDetailInfo.MasterQueryName = "audits";
        masterDetailInfo2.DetailQueryName = "audit_details";
        relationColumnInfo2.NestedKeyColumn = "audit_id";
        relationColumnInfo2.ParentKeyColumn = "audit_id";
        relationColumnInfo3.NestedKeyColumn = "group_position";
        relationColumnInfo3.ParentKeyColumn = "group_position";
        relationColumnInfo4.NestedKeyColumn = "group_name";
        relationColumnInfo4.ParentKeyColumn = "group_name";
        relationColumnInfo5.NestedKeyColumn = "group_name_ENG";
        relationColumnInfo5.ParentKeyColumn = "group_name_ENG";
        masterDetailInfo2.KeyColumns.Add(relationColumnInfo2);
        masterDetailInfo2.KeyColumns.Add(relationColumnInfo3);
        masterDetailInfo2.KeyColumns.Add(relationColumnInfo4);
        masterDetailInfo2.KeyColumns.Add(relationColumnInfo5);
        masterDetailInfo2.MasterQueryName = "audit_groups";
        this.sdsData.Relations.AddRange(new MasterDetailInfo[]
        {
               masterDetailInfo,
               masterDetailInfo2
        });
        this.sdsData.ResultSchemaSerializable = componentResourceManager.GetString("sdsData.ResultSchemaSerializable");
        this.xrTable1.Borders = BorderSide.All;
        this.xrTable1.LocationFloat = new PointFloat(0f, 35.00001f);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new PaddingInfo(3, 3, 3, 3, 100f);
        this.xrTable1.Rows.AddRange(new XRTableRow[]
        {
               this.xrTableRow1,
               this.xrTableRow2
        });
        this.xrTable1.SizeF = new SizeF(402.0833f, 50f);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UsePadding = false;
        this.xrTableRow1.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell1,
               this.xrTableCell2
        });
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1.0;
        this.xrTableCell1.Font = new Font("Times New Roman", 9.75f, FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "AUDIT TYPE";
        this.xrTableCell1.TextAlignment = TextAlignment.MiddleLeft;
        this.xrTableCell1.Weight = 1.1564766917064278;
        this.xrTableCell2.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[audit_type_name]")
        });
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 1.8435233082935722;
        this.xrTableRow2.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell3,
               this.xrTableCell4
        });
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1.0;
        this.xrTableCell3.Font = new Font("Times New Roman", 9.75f, FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "AUDIT TARGET";
        this.xrTableCell3.TextAlignment = TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 1.1564766917064278;
        this.xrTableCell4.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[audit_target]")
        });
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.TextAlignment = TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 1.8435233082935722;
        this.xrLabel1.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[group_position]")
        });
        this.xrLabel1.Font = new Font("Times New Roman", 11f, FontStyle.Bold);
        this.xrLabel1.LocationFloat = new PointFloat(0f, 5f);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
        this.xrLabel1.SizeF = new SizeF(36.46f, 20f);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.TextFormatString = "{0}.";
        if (lang == "ENG")
        {
            this.xrLabel2.ExpressionBindings.AddRange(new ExpressionBinding[]
            {
                    new ExpressionBinding("BeforePrint", "Text", "[group_name_ENG]")
            });
        }
        else
        {
            this.xrLabel2.ExpressionBindings.AddRange(new ExpressionBinding[]
            {
                    new ExpressionBinding("BeforePrint", "Text", "[group_name]")
            });
        }
        this.xrLabel2.Font = new Font("Times New Roman", 11f, FontStyle.Bold);
        this.xrLabel2.LocationFloat = new PointFloat(36.46008f, 4.999987f);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
        this.xrLabel2.SizeF = new SizeF(690.54f, 20f);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrPageInfo2.Font = new Font("Times New Roman", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 238);
        this.xrPageInfo2.LocationFloat = new PointFloat(0f, 20f);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
        this.xrPageInfo2.PageInfo = PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new SizeF(208.3333f, 25f);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo1.Font = new Font("Times New Roman", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.xrPageInfo1.LocationFloat = new PointFloat(677.0001f, 20.00001f);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
        this.xrPageInfo1.SizeF = new SizeF(100f, 25f);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = TextAlignment.TopRight;
        this.xrTable2.BackColor = Color.Gray;
        this.xrTable2.Borders = BorderSide.All;
        this.xrTable2.Font = new Font("Times New Roman", 9.75f, FontStyle.Bold);
        this.xrTable2.ForeColor = Color.White;
        this.xrTable2.LocationFloat = new PointFloat(0f, 25f);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new XRTableRow[]
        {
               this.xrTableRow5
        });
        this.xrTable2.SizeF = new SizeF(777.0001f, 40f);
        this.xrTable2.StylePriority.UseBackColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseForeColor = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = TextAlignment.MiddleCenter;
        this.xrTableRow5.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell10,
               this.xrTableCell11,
               this.xrTableCell12
        });
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1.0;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "QUESTION";
        this.xrTableCell10.Weight = 2.8681152485956667;
        this.xrTableCell11.Multiline = true;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "ANSWER\r\nOK/NOK/NC/NA";
        this.xrTableCell11.Weight = 0.70283106238354953;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "COMMENT";
        this.xrTableCell12.Weight = 1.3721416953281411;
        this.xrTable3.Borders = (BorderSide.Left | BorderSide.Right | BorderSide.Bottom);
        this.xrTable3.LocationFloat = new PointFloat(0f, 0f);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Padding = new PaddingInfo(3, 3, 3, 3, 100f);
        this.xrTable3.Rows.AddRange(new XRTableRow[]
        {
               this.xrTableRow6
        });
        this.xrTable3.SizeF = new SizeF(777.0001f, 20f);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UsePadding = false;
        this.xrTableRow6.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell14,
               this.xrTableCell15,
               this.xrTableCell16
        });
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.StylePriority.UseTextAlignment = false;
        this.xrTableRow6.TextAlignment = TextAlignment.MiddleLeft;
        this.xrTableRow6.Weight = 1.0;
        if (lang == "ENG")
        {
            this.xrTableCell14.ExpressionBindings.AddRange(new ExpressionBinding[]
            {
                    new ExpressionBinding("BeforePrint", "Text", "[question_ENG]")
            });
        }
        else
        {
            this.xrTableCell14.ExpressionBindings.AddRange(new ExpressionBinding[]
            {
                    new ExpressionBinding("BeforePrint", "Text", "[question]")
            });
        }
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "QUESTION";
        this.xrTableCell14.Weight = 2.8681155156236566;
        this.xrTableCell15.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[answer]")
        });
        this.xrTableCell15.Multiline = true;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "ANSWER\r\nOK/NOK/NC/NA";
        this.xrTableCell15.Weight = 0.7028310597352837;
        this.xrTableCell16.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[comment]")
        });
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "COMMENT";
        this.xrTableCell16.Weight = 1.3721418459444952;
        this.ReportHeader.Controls.AddRange(new XRControl[]
        {
               this.xrLabel1,
               this.xrLabel2,
               this.xrTable2
        });
        this.ReportHeader.HeightF = 65f;
        this.ReportHeader.KeepTogether = true;
        this.ReportHeader.Name = "ReportHeader";
        this.Detail2.Controls.AddRange(new XRControl[]
        {
               this.xrTable3
        });
        this.Detail2.HeightF = 20f;
        this.Detail2.KeepTogether = true;
        this.Detail2.Name = "Detail2";
        this.DetailReport1.Bands.AddRange(new Band[]
        {
               this.Detail2,
               this.ReportHeader
        });
        this.DetailReport1.DataMember = "audits.auditsaudit_groups.audit_groupsaudit_details";
        this.DetailReport1.DataSource = this.sdsData;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.Detail1.HeightF = 0f;
        this.Detail1.Name = "Detail1";
        this.DetailReport.Bands.AddRange(new Band[]
        {
               this.Detail1,
               this.DetailReport1
        });
        this.DetailReport.DataMember = "audits.auditsaudit_groups";
        this.DetailReport.DataSource = this.sdsData;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.xrTable4.Borders = BorderSide.All;
        this.xrTable4.LocationFloat = new PointFloat(524.9167f, 34.99997f);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Padding = new PaddingInfo(3, 3, 3, 3, 100f);
        this.xrTable4.Rows.AddRange(new XRTableRow[]
        {
               this.xrTableRow9,
               this.xrTableRow10
        });
        this.xrTable4.SizeF = new SizeF(252.0833f, 50.00002f);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UsePadding = false;
        this.xrTableRow9.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell21,
               this.xrTableCell22
        });
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1.0;
        this.xrTableCell21.Font = new Font("Times New Roman", 9.75f, FontStyle.Bold);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.StylePriority.UsePadding = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "AUDITOR";
        this.xrTableCell21.TextAlignment = TextAlignment.MiddleLeft;
        this.xrTableCell21.Weight = 1.0023928564142768;
        this.xrTableCell22.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[auditor_full_name]")
        });
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.Text = "xrTableCell6";
        this.xrTableCell22.TextAlignment = TextAlignment.MiddleCenter;
        this.xrTableCell22.Weight = 1.9976071435857232;
        this.xrTableRow10.Cells.AddRange(new XRTableCell[]
        {
               this.xrTableCell23,
               this.xrTableCell24
        });
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1.0;
        this.xrTableCell23.Font = new Font("Times New Roman", 9.75f, FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.StylePriority.UsePadding = false;
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "DATE";
        this.xrTableCell23.TextAlignment = TextAlignment.MiddleLeft;
        this.xrTableCell23.Weight = 1.0023928564142768;
        this.xrTableCell24.ExpressionBindings.AddRange(new ExpressionBinding[]
        {
               new ExpressionBinding("BeforePrint", "Text", "[start_date]")
        });
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.Text = "xrTableCell8";
        this.xrTableCell24.TextAlignment = TextAlignment.MiddleCenter;
        this.xrTableCell24.TextFormatString = "{0:dd.MM.yyyy}";
        this.xrTableCell24.Weight = 1.9976071435857232;
        this.id.Description = "id";
        this.id.Name = "id";
        this.id.Type = typeof(int);
        this.id.ValueInfo = "0";
        base.Bands.AddRange(new Band[]
        {
               this.Detail,
               this.TopMargin,
               this.BottomMargin,
               this.DetailReport
        });
        base.ComponentStorage.AddRange(new IComponent[]
        {
               this.sdsData
        });
        base.DataMember = "audits";
        base.DataSource = this.sdsData;
        base.Margins = new Margins(25, 25, 110, 70);
        base.PageHeight = 1169;
        base.PageWidth = 827;
        base.PaperKind = PaperKind.A4;
        base.Parameters.AddRange(new Parameter[]
        {
               this.id
        });
        base.Version = "17.2";
        ((ISupportInitialize)this.xrTable1).EndInit();
        ((ISupportInitialize)this.xrTable2).EndInit();
        ((ISupportInitialize)this.xrTable3).EndInit();
        ((ISupportInitialize)this.xrTable4).EndInit();
        ((ISupportInitialize)this).EndInit();
    }
}