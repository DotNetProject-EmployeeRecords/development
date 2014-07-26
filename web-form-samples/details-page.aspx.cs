using System;
using System.Web.UI.WebControls;
using Eisk.Helpers;
using Eisk.BusinessEntities;

public partial class Details_Page : System.Web.UI.Page
{

    #region "Select handlers"

    protected void OdsEmployeeDetails_Selecting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string editMode = Page.RouteData.Values["edit_mode"] as string;

        if (editMode == "view")
            formViewEmployee.ChangeMode(FormViewMode.ReadOnly);
        else if (editMode == "edit")
            formViewEmployee.ChangeMode(FormViewMode.Edit);
        else if (editMode == "new")
            formViewEmployee.ChangeMode(FormViewMode.Insert);
        else
            throw new InvalidOperationException("error");

        
        if (formViewEmployee.CurrentMode == FormViewMode.Insert)
            e.Cancel = true;
    }

   protected void FormViewEmployee_DataBound(object sender, System.EventArgs e)
    {

        if (Page.Request.UrlReferrer != null)
            if (Page.Request.UrlReferrer.AbsolutePath.Contains("new") && !IsPostBack)
            {
                ltlMessage.Text = MessageFormatter.GetFormattedSuccessMessage("Insert successful");
            }
    
    }

    #endregion

    #region "Command Handlers"

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (formViewEmployee.CurrentMode == FormViewMode.Insert)
        {
            formViewEmployee.InsertItem(true);
        }
        else
        {
            formViewEmployee.UpdateItem(true);
        }
    }


    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute(new { edit_mode = "edit", employee_id = RouteData.Values["employee_id"] });
    }

    protected void ButtonGoToListPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/web-form-samples/listing-page.aspx");
    }

    #endregion

    #region "Insert handlers"

    protected void FormViewEmployee_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        if (!String.IsNullOrEmpty(((FileUpload)this.formViewEmployee.FindControl("fuPhotoUpload")).FileName))
            e.Values["photo"] = ((FileUpload)this.formViewEmployee.FindControl("fuPhotoUpload")).FileBytes;
    }

    protected void OdsEmployeeDetails_Inserted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
    {
        int result = Convert.ToInt32(e.ReturnValue, System.Globalization.CultureInfo.CurrentCulture.NumberFormat);

        if (result != 0)
        {
            Response.RedirectToRoute(new { edit_mode = "edit", employee_id = result.ToString() });
        }
    }

    protected void formViewEmployee_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message 
            ltlMessage.Text = ExceptionManager.DoLogAndGetFriendlyMessageForException(e.Exception);
            // Indicate that the exception has been handled 
            e.ExceptionHandled = true;
            // Keep the row in edit mode 
            e.KeepInInsertMode = true;
        }
    }

    #endregion

    #region "Update handlers"

    protected void FormViewEmployee_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

        if (!String.IsNullOrEmpty(((FileUpload)this.formViewEmployee.FindControl("fuPhotoUpload")).FileName))
            e.NewValues["photo"] = ((FileUpload)this.formViewEmployee.FindControl("fuPhotoUpload")).FileBytes;
        else
        {
            if ((this.formViewEmployee.FindControl("chkRemoveOldImage") as CheckBox).Checked)
                e.NewValues["photo"] = null;
            else
                e.NewValues["photo"] = new Eisk.BusinessLogicLayer.EmployeeBLL().GetEmployeeByEmployeeId(int.Parse(RouteData.Values["employee_id"].ToString())).Photo;
        }
    }

    protected void formViewEmployee_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Display a user-friendly message 
            ltlMessage.Text = ExceptionManager.DoLogAndGetFriendlyMessageForException(e.Exception);
            // Indicate that the exception has been handled 
            e.ExceptionHandled = true;
            // Keep the current UI in edit mode 
            e.KeepInEditMode = true;
        }
        else
            ltlMessage.Text = MessageFormatter.GetFormattedSuccessMessage("Update successful");
    }
    
    #endregion
}
