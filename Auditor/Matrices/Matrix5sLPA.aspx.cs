using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System;
using System.Drawing;
using System.Globalization;

namespace Auditor
{
    public partial class Matrix5sLPA : System.Web.UI.Page
    {
        private decimal OkNokTreshold = 80;
        private Color ColorOK = Color.FromArgb(142, 204, 142);
        private Color ColorNOK = Color.FromArgb(229, 145, 145);
        private Color ColorCW = Color.FromArgb(96, 205, 255);

        protected void Page_Load(object sender, EventArgs e)
        {
            sdsTargetsAdmin.SelectParameters["audit_type"].DefaultValue = AuditTypes.Audit5sAdministration;
            sdsTargetsProduction.SelectParameters["audit_type"].DefaultValue = AuditTypes.Audit5sLpaProduction;
            sdsPivot.SelectParameters["schedule_name1"].DefaultValue = Schedules.Audit5sAdministration;
            sdsPivot.SelectParameters["schedule_name2"].DefaultValue = Schedules.Audit5sLpaProduction;
            sdsPivotSV.SelectParameters["schedule_name"].DefaultValue = Schedules.Audit5sLpaSV;
            sdsScores.SelectParameters["schedule_name1"].DefaultValue = Schedules.Audit5sAdministration;
            sdsScores.SelectParameters["schedule_name2"].DefaultValue = Schedules.Audit5sLpaProduction;
            sdsScores.SelectParameters["schedule_name3"].DefaultValue = Schedules.Audit5sLpaSV;
            sdsAvg.SelectParameters["schedule_name1"].DefaultValue = Schedules.Audit5sAdministration;
            sdsAvg.SelectParameters["schedule_name2"].DefaultValue = Schedules.Audit5sLpaProduction;
            sdsAvg.SelectParameters["schedule_name3"].DefaultValue = Schedules.Audit5sLpaSV;
            sdsTop5.SelectParameters["schedule_name1"].DefaultValue = Schedules.Audit5sAdministration;
            sdsTop5.SelectParameters["schedule_name2"].DefaultValue = Schedules.Audit5sLpaProduction;
            sdsTop5.SelectParameters["schedule_name3"].DefaultValue = Schedules.Audit5sLpaSV;
            sdsBot5.SelectParameters["schedule_name1"].DefaultValue = Schedules.Audit5sAdministration;
            sdsBot5.SelectParameters["schedule_name2"].DefaultValue = Schedules.Audit5sLpaProduction;
            sdsBot5.SelectParameters["schedule_name3"].DefaultValue = Schedules.Audit5sLpaSV;
        }

        protected void gv_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.Settings.ShowFilterRowMenu = false;
            gridview.Settings.ShowHeaderFilterButton = false;
            gridview.Settings.ShowTitlePanel = true;
            gridview.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            gridview.SettingsBehavior.AllowFocusedRow = false;
            gridview.KeyboardSupport = false;
            gridview.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
            gridview.SettingsPager.AlwaysShowPager = false;
            gridview.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            gridview.Font.Size = new System.Web.UI.WebControls.FontUnit("12px");
            gridview.SettingsText.Title = "PRODUCTION AREAS";
        }

        protected void pivot_Init(object sender, EventArgs e)
        {
            ASPxPivotGrid pivot = sender as ASPxPivotGrid;
            pivot.OptionsPager.RowsPerPage = 30;
            pivot.OptionsPager.ColumnsPerPage = 40;
            pivot.OptionsPager.Position = System.Web.UI.WebControls.PagerPosition.Bottom;
            pivot.OptionsPager.Visible = false;
            pivot.OptionsView.VerticalScrollBarMode = ScrollBarMode.Visible;
            pivot.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Visible;
            pivot.OptionsView.VerticalScrollingMode = PivotScrollingMode.Virtual;
            pivot.OptionsView.HorizontalScrollingMode = PivotScrollingMode.Virtual;
            pivot.OptionsView.ShowRowTotals = false;
            pivot.OptionsView.ShowColumnGrandTotals = false;
            pivot.OptionsView.ShowRowGrandTotals = false;
            pivot.OptionsView.ShowFilterSeparatorBar = false;
            pivot.OptionsView.ShowFilterHeaders = false;
            pivot.OptionsView.ShowDataHeaders = false;
            pivot.OptionsView.ShowContextMenus = false;
            pivot.OptionsView.ShowColumnHeaders = false;
            pivot.OptionsFilter.ShowOnlyAvailableItems = true;
            pivot.Styles.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            pivot.Styles.HeaderStyle.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            pivot.OptionsCustomization.AllowPrefilter = false;
            pivot.OptionsCustomization.AllowCustomizationForm = false;
            pivot.OptionsCustomization.AllowDrag = false;
            pivot.OptionsCustomization.AllowDragInCustomizationForm = false;
            pivot.OptionsCustomization.AllowSortBySummary = false;
            pivot.OptionsCustomization.AllowSortInCustomizationForm = false;
            pivot.OptionsCustomization.AllowExpand = false;
            pivot.OptionsCustomization.AllowFilter = false;
            pivot.OptionsCustomization.AllowSort = false;
        }

        protected void gvTargetsProduction_Init(object sender, EventArgs e)
        {
            gv_Init(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsText.Title = "PRODUCTION AREAS";
        }

        protected void gvTargetsAdmin_Init(object sender, EventArgs e)
        {
            gv_Init(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsText.Title = "ADMINISTRATIVE AREAS";
        }

        protected void pivotMatrix_Init(object sender, EventArgs e)
        {
            pivot_Init(sender, e);
            ASPxPivotGrid pivot = sender as ASPxPivotGrid;
            pivot.Caption = "AUDIT MATRIX";
        }

        protected void pivotMatrixSV_Init(object sender, EventArgs e)
        {
            pivot_Init(sender, e);
            ASPxPivotGrid pivot = sender as ASPxPivotGrid;
            pivot.Caption = "SUPERVISORS AUDIT MATRIX";
        }

        protected void pivotMatrix_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
        {
            var pivot = sender as ASPxPivotGrid;
            PivotGridField fieldauditscore = pivot.Fields.GetFieldByName("fieldauditscore") as PivotGridField;
            PivotGridField fieldyearweek = pivot.Fields.GetFieldByName("fieldyearweek") as PivotGridField;

            if (e.DataField.FieldName == "auditor_full_name" || e.DataField.FieldName == "audit_score")
            {
                var column_week = Utils.ConvertToTrimmedString(e.GetFieldValue(fieldyearweek));
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                string current_week = $"{DateTime.Now.Year.ToString()} W-{cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString()}";
                if (column_week != null && column_week == current_week)
                {
                    e.Cell.BackColor = ColorCW;
                }
            }
            if (e.DataField.FieldName == "auditor_full_name")
            {
                e.Cell.Style.Add("text-align", "left");
                string text = e.Cell.Text;
                text = text.Replace(" ", @"<br />");
                text = text.Replace("-", @"-<br />");
                e.Cell.Text = text;
                var score = Utils.ConvertToNullableDecimal(e.GetFieldValue(fieldauditscore));
                if (score != null)
                {
                    e.Cell.BackColor = (score >= OkNokTreshold) ? ColorOK : ColorNOK;
                }
            }
            if (e.DataField.FieldName == "audit_score")
            {
                e.Cell.Style.Add("border-left-width", "0px");
                var score = Utils.ConvertToNullableDecimal(e.GetFieldValue(fieldauditscore));
                if (score != null)
                {
                    e.Cell.BackColor = (score >= OkNokTreshold) ? ColorOK : ColorNOK;
                    e.Cell.Text = $"{Math.Floor((decimal)score)} %";
                }
            }
        }

        protected void pivotMatrixSV_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
        {
            var pivot = sender as ASPxPivotGrid;
            PivotGridField fieldauditscore = pivot.Fields.GetFieldByName("fieldauditscore") as PivotGridField;
            PivotGridField fieldyearweek = pivot.Fields.GetFieldByName("fieldyearweek") as PivotGridField;

            if (e.DataField.FieldName == "audit_target" || e.DataField.FieldName == "audit_score")
            {
                var column_week = Utils.ConvertToTrimmedString(e.GetFieldValue(fieldyearweek));
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                string current_week = $"{DateTime.Now.Year.ToString()} W-{cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString()}";
                if (column_week != null && column_week == current_week)
                {
                    e.Cell.BackColor = ColorCW;
                }
            }
            if (e.DataField.FieldName == "audit_target")
            {
                e.Cell.Style.Add("text-align", "left");
                var score = Utils.ConvertToNullableDecimal(e.GetFieldValue(fieldauditscore));
                if (score != null)
                {
                    e.Cell.BackColor = (score >= OkNokTreshold) ? ColorOK : ColorNOK;
                }
            }
            if (e.DataField.FieldName == "audit_score")
            {
                e.Cell.Style.Add("border-left-width", "0px");
                var score = Utils.ConvertToNullableDecimal(e.GetFieldValue(fieldauditscore));
                if (score != null)
                {
                    e.Cell.BackColor = (score >= OkNokTreshold) ? ColorOK : ColorNOK;
                    e.Cell.Text = $"{Math.Floor((decimal)score)} %";
                }
            }
        }

        protected void gv_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.VisibleIndex >= 0)
            {
                string fieldName = e.DataColumn.FieldName;
                if (fieldName == "score")
                {
                    var score = Utils.ConvertToNullableDecimal(e.CellValue);
                    if (score != null)
                    {
                        e.Cell.BackColor = (score >= OkNokTreshold) ? ColorOK : ColorNOK;
                        e.Cell.Text = $"{Math.Floor((decimal)score)} %";
                    }
                }
            }
        }

        protected void gvAvg_Init(object sender, EventArgs e)
        {
            gv_Init(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsText.Title = "PLANT AVERAGE (LAST WEEK)";
        }

        protected void gvTop5_Init(object sender, EventArgs e)
        {
            gv_Init(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsText.Title = "BEST 5 PLACES (LAST WEEK)";
        }

        protected void gvBot5_Init(object sender, EventArgs e)
        {
            gv_Init(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsText.Title = "WORST 5 PLACES (LAST WEEK)";
        }
    }
}