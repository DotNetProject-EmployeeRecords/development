using System;
using Eisk.DataAccessLayer;
using System.Web.UI.WebControls;
using System.Web;
using Eisk.Helpers;

public partial class Master_Default : System.Web.UI.MasterPage
{
    protected void lbtGenerateTestData_Click(object sender, EventArgs e)
    {
        SqlScriptRunner.RunScript(Server.MapPath("~/App_Data/SQL/Data/Clean-Data.sql"));
        SqlScriptRunner.RunScript(Server.MapPath("~/App_Data/SQL/Data/Create-Data.sql"));
        ltlMessage.Text = MessageFormatter.GetFormattedSuccessMessage("Test Data Generated.");
        Page.DataBind();

    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }
}
