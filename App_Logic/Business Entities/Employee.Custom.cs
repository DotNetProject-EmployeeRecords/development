using System;
using Eisk.Helpers;
namespace Eisk.BusinessEntities
{
    partial class Employee
    {
        //optional field
        partial void OnBirthDateChanged()
        {
            ValidateBirthDateAndHireDate();
        }

        //required field
        partial void OnHireDateChanged()
        {
            ValidateBirthDateAndHireDate();
        }

        /// <summary>
        /// validation method
        /// RULE: BirthDate should not be later than the HireDate
        /// </summary>
        void ValidateBirthDateAndHireDate()
        {
            if ((BirthDate != null) && (!HireDate.IsEmpty()) && (DateTime.Compare((DateTime)BirthDate, HireDate) >= 0))
                throw new BusinessRuleViolationOnInMemoryException("Exception!!! BirthDate should be earlier than HireDate!");
        }
        
        //optional field - with default value. 
        //Please note that, default value concept may not work for a required value.
        //also, since we'll not allow empty value for required and optional value, we are considering only NULL value to set default value
        partial void OnExtensionChanged()
        {
            if (Extension.IsNull())
            {
                Extension = "n/a";
            }
        }
 
    }
}
