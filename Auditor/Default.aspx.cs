using DevExpress.Web;
using System;

namespace Auditor
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ActiveUser.IsAuthenticated)
            {
                Response.Redirect(Pages.Login);
            }
            var activeUser = new ActiveUser();
            if (Audit.GetUserActiveAudit(activeUser.UserName) != null)
            {
                Response.Redirect(Pages.PerformAudit);
            }
            lblGreetings.Text = $"Hello {activeUser.FullName}!";
            lblInfo.Text = Utils.ConvertToTrimmedString(Session["message"]);
            Session["message"] = null;
            cbAuditTarget.DataBind();
            cbAuditShiftName.DataBind();
        }

        protected void btnStartAudit_Click(object sender, EventArgs e)
        {
            var activeUser = new ActiveUser();
            var auditType = cbAuditType.SelectedItem != null ? Utils.ConvertToTrimmedString(cbAuditType.SelectedItem.Value) : null;
            var auditTarget = cbAuditTarget.SelectedItem != null ? Utils.ConvertToTrimmedString(cbAuditTarget.SelectedItem.Value) : null;
            var auditShiftName = cbAuditShiftName.SelectedItem != null ? Utils.ConvertToTrimmedString(cbAuditShiftName.SelectedItem.Value) : null;
            var auditorLogin = activeUser.UserName;
            var auditorFullName = activeUser.FullName;
            if (auditTarget != null && auditType != null)
            {
                if (!(AuditTypes.ShiftNameObligatory(auditType) && auditShiftName == null))
                {
                    auditShiftName = AuditTypes.ShiftNameObligatory(auditType) ? auditShiftName : null;
                    if (auditorLogin != null && auditorFullName != null)
                    {
                        if (AuditTypes.UserInAuditorList(auditorLogin, auditType, auditTarget))
                        {
                            var newAudit = Audit.CreateNew(auditType, auditTarget, auditorLogin, auditorFullName, auditShiftName);
                            if (newAudit != null)
                            {
                                Session["lang"] = null;
                                Session["page"] = null;
                                Session["audit_id"] = null;
                                Session["audit_detail_id"] = null;
                                Response.Redirect(Pages.PerformAudit);
                            }
                            else
                            {
                                Session["message"] = "Error when generating the audit template!!";
                            }
                        }
                        else
                        {
                            Session["message"] = "You are not assigned as an auditor for this type / target of audit!";
                        }
                    }
                    else
                    {
                        Session["message"] = "You are logged out!";
                    }
                }
                else
                {
                    Session["message"] = "Choose shift!";
                }
            }
            else
            {
                Session["message"] = "Choose type and target of audit!";
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void cbPanelAudit_Callback(object sender, CallbackEventArgsBase e)
        {
            var auditType = Utils.ConvertToTrimmedString(e.Parameter);
            AuditTypes.AdjustTemplateToAuditType(auditType, cbAuditTarget, cbAuditShiftName);
        }
    }
}