using System;
using System.Web.UI.WebControls;

namespace Eisk.TestHelpers
{
    public sealed class UITestDataHelper
    {
        public static void LoadUITestDataFromFormView(FormView formViewEmployee)
        {
            TextBox txtFirstName = (TextBox)formViewEmployee.FindControl("txtFirstName");
            TextBox txtLastName = (TextBox)formViewEmployee.FindControl("txtLastName");
            TextBox txtHireDate = (TextBox)formViewEmployee.FindControl("txtHireDate");
            TextBox txtAddress = (TextBox)formViewEmployee.FindControl("txtAddress");
            TextBox txtHomePhone = (TextBox)formViewEmployee.FindControl("txtHomePhone");
            DropDownList ddlCountry = (DropDownList)formViewEmployee.FindControl("ddlCountry");

            //since in read-only mode there is no text box control
            if (formViewEmployee.CurrentMode != FormViewMode.ReadOnly)
            {

                ////using data value in form in custom way

                //populating per-fill data
                if (formViewEmployee.CurrentMode == FormViewMode.Insert)
                {
                    txtFirstName.Text = "Jarrad";
                    txtLastName.Text = "Strausbaugh";
                    txtHireDate.Text = DateTime.Now.ToString();
                    txtAddress.Text = "1832 Sand Hill Rd, Hershey, PA 17033";
                    txtHomePhone.Text = "7176023166";
                    ddlCountry.Items.FindByText("USA").Selected = true;
                }
            }

        }
    }
}