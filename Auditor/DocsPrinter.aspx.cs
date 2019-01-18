using System;

namespace Auditor
{
    public partial class DocsPrinter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Utils.ConvertToNullableInt(Request.QueryString["id"]);
            lblInfo.Text = string.Empty;
            if (id != null)
            {
                if (Audit.AuditExist((int)id))
                {
                    var report = AuditTypes.GetXtraReport(new Audit((int)id), Languages.Polish);
                    var reportFileName = $"Audit_{id}";
                    Utils.PrintPDF(report, reportFileName);
                }
                else
                {
                    lblInfo.Text = $"Printing of the document failed!{Environment.NewLine}Audit does not exist!";
                }
            }
            else
            {
                lblInfo.Text = $"Printing of the document failed!{Environment.NewLine}Wrong parameter!";
            }
        }
    }
}