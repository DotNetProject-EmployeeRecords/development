using System.Linq;
using Eisk.BusinessEntities;
using System;
using Eisk.Helpers;

namespace Eisk.BusinessLogicLayer
{
    public partial class EmployeeBLL
    {
        partial void OnEmployeeSaving(Employee employee)
        {
            /* an employee's country must be same as his supervisors country */
            if (employee.ReportsTo != null)
                employee.Supervisor = GetEmployeeByEmployeeId((int)employee.ReportsTo);

            if (employee.Supervisor != null)
            {
                if (employee.Country != employee.Supervisor.Country)
                    throw new BusinessRuleViolationOnDbAccessException("An employee's country must be same as his supervisors country.");
            }

        }

    }
}
