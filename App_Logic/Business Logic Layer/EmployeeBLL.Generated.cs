using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using Eisk.BusinessEntities;
using Eisk.DataAccessLayer;
using Eisk.Helpers;

namespace Eisk.BusinessLogicLayer
{   
	[System.ComponentModel.DataObject(true)]
	public partial class EmployeeBLL:IDisposable
	{
		#region Constructors, Dependency and Partial Method Declaration

        public EmployeeBLL() : this(new DatabaseContext()) { }

        public EmployeeBLL(DatabaseContext DatabaseContext)
        {
            _DatabaseContext = DatabaseContext;
        }

        DatabaseContext _DatabaseContext;

        public void Dispose()
        {
            if (_DatabaseContext != null)
            {
                _DatabaseContext.Dispose();
                _DatabaseContext = null;
            }
            
            GC.SuppressFinalize(this);
        }

        partial void OnEmployeeSaving(Employee employee);

        partial void OnEmployeeCreating(Employee employee);
        partial void OnEmployeeCreated(Employee employee);

        partial void OnEmployeeUpdating(Employee employee);
        partial void OnEmployeeUpdated(Employee employee);

        partial void OnEmployeeSaved(Employee employee);

        partial void OnEmployeeDeleting(Employee employee);
        partial void OnEmployeeDeleted(Employee employee);


        #endregion

        #region Get Methods

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public Employee GetEmployeeByEmployeeId(int employeeId)
        {
            //Validate Input
            if (employeeId.IsInvalidKey())
                BusinessLayerHelper.ThrowErrorForInvalidDataKey("employeeId");
            return (_DatabaseContext.Employees.FirstOrDefault(employee => employee.EmployeeId == employeeId));
        }
			
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Employee> GetEmployeesByReportsTo(int? reportsTo)
        {
            //Validate Input
            if (reportsTo.IsEmpty())
                return GetAllEmployees();
 
            return (from employee in _DatabaseContext.Employees
                    where reportsTo == null ? employee.ReportsTo == null : employee.ReportsTo == reportsTo
                    select employee).ToList();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Employee> GetEmployeesByReportsToPaged(int? reportsTo, string orderBy = default(string), int startRowIndex = default(int), int maximumRows = default(int))
        {
            //Validate Input
            if (reportsTo.IsEmpty())
                return GetAllEmployeesPaged(orderBy, startRowIndex, maximumRows);

            if (string.IsNullOrEmpty(orderBy))
                orderBy = "EmployeeId";
            
			if (startRowIndex < 0) 
				throw (new ArgumentOutOfRangeException("startRowIndex"));
				
			if (maximumRows < 0) 
				throw (new ArgumentOutOfRangeException("maximumRows"));
				
            return (
                    from employee in
                        _DatabaseContext.Employees
                        .DynamicOrderBy(orderBy)
                    where reportsTo == null ? employee.ReportsTo == null : employee.ReportsTo == reportsTo
                    select employee
                    )
                    .Skip(startRowIndex)
                    .Take(maximumRows)
                    .ToList();
        }

        public int GetTotalCountForAllEmployeesByReportsTo(int? reportsTo, string orderBy = default(string), int startRowIndex = default(int), int maximumRows = default(int))
        {
            //Validate Input
            if (reportsTo.IsEmpty())
                return GetTotalCountForAllEmployees(orderBy, startRowIndex, maximumRows);
			
            return _DatabaseContext.Employees.Count(employee => reportsTo == null ? employee.ReportsTo == null : employee.ReportsTo == reportsTo);
        }
			
				
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Employee> GetAllEmployees()
        {
            return _DatabaseContext.Employees.ToList();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Employee> GetAllEmployeesPaged(string orderBy = default(string), int startRowIndex = default(int), int maximumRows = default(int))
        {
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "EmployeeId";
				
			if (startRowIndex < 0) 
				throw (new ArgumentOutOfRangeException("startRowIndex"));
				
			if (maximumRows < 0) 
				throw (new ArgumentOutOfRangeException("maximumRows"));
			
            return (
                    from employee in 
                        _DatabaseContext.Employees
                        .DynamicOrderBy(orderBy)
                    select employee
                    )
                    .Skip(startRowIndex)
                    .Take(maximumRows)
                    .ToList();
        }

        public int GetTotalCountForAllEmployees(string orderBy = default(string), int startRowIndex = default(int), int maximumRows = default(int))
        {
            return _DatabaseContext.Employees.Count();
        }

        #endregion

        #region Persistence (Create, Update, Delete) Methods

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int CreateNewEmployee(Employee newEmployee)
        {
            // Validate Parameters 
            if (newEmployee == null)
                throw (new ArgumentNullException("newEmployee"));

	        // Apply business rules
            OnEmployeeSaving(newEmployee);
            OnEmployeeCreating(newEmployee);

            _DatabaseContext.Employees.AddObject(newEmployee);
            int numberOfAffectedRows = _DatabaseContext.SaveChanges();
            if (numberOfAffectedRows == 0) 
                throw new DataNotUpdatedException("No employee created!");

            // Apply business workflow
            OnEmployeeCreated(newEmployee);
            OnEmployeeSaved(newEmployee);

            return newEmployee.EmployeeId;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public void UpdateEmployee(Employee updatedEmployee)
        {
            // Validate Parameters
            if (updatedEmployee == null)
                throw (new ArgumentNullException("updatedEmployee"));

            // Validate Primary key value
            if (updatedEmployee.EmployeeId.IsInvalidKey())
                BusinessLayerHelper.ThrowErrorForInvalidDataKey("EmployeeId");

            // Apply business rules
            OnEmployeeSaving(updatedEmployee);
            OnEmployeeUpdating(updatedEmployee);

            //attaching and making ready for parsistance
            if (updatedEmployee.EntityState == EntityState.Detached)
                _DatabaseContext.Employees.Attach(updatedEmployee);
			_DatabaseContext.ObjectStateManager.ChangeObjectState(updatedEmployee, System.Data.EntityState.Modified);//this line makes the code un-testable!
            int numberOfAffectedRows = _DatabaseContext.SaveChanges();
            if (numberOfAffectedRows == 0) 
                throw new DataNotUpdatedException("No employee updated!");

            //Apply business workflow
            OnEmployeeUpdated(updatedEmployee);
            OnEmployeeSaved(updatedEmployee);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public void DeleteEmployee(Employee employeeToBeDeleted)
        {
            //Validate Input
            if (employeeToBeDeleted == null)
                throw (new ArgumentNullException("employeeToBeDeleted"));

            // Validate Primary key value
            if (employeeToBeDeleted.EmployeeId.IsInvalidKey())
                BusinessLayerHelper.ThrowErrorForInvalidDataKey("EmployeeId");

            OnEmployeeSaving(employeeToBeDeleted);
            OnEmployeeDeleting(employeeToBeDeleted);

            if (employeeToBeDeleted.EntityState == EntityState.Detached)
             _DatabaseContext.Employees.Attach(employeeToBeDeleted);
			_DatabaseContext.Employees.DeleteObject(employeeToBeDeleted);
            int numberOfAffectedRows = _DatabaseContext.SaveChanges();
            if (numberOfAffectedRows == 0) 
                throw new DataNotUpdatedException("No Employee deleted!");
            
            OnEmployeeDeleted(employeeToBeDeleted);
            OnEmployeeSaved(employeeToBeDeleted);

        }

        public void DeleteEmployees(List<int> employeeIdsToDelete)
        {
            //Validate Input
            foreach (int employeeId in employeeIdsToDelete)
                if (employeeId.IsInvalidKey())
                    BusinessLayerHelper.ThrowErrorForInvalidDataKey("EmployeeId");

            List<Employee> employeesToBeDeleted = new List<Employee>();

            foreach (int employeeId in employeeIdsToDelete)
            {
                Employee employee = new Employee { EmployeeId = employeeId };
                _DatabaseContext.Employees.Attach(employee);
				_DatabaseContext.Employees.DeleteObject(employee);
                employeesToBeDeleted.Add(employee);
                OnEmployeeDeleting(employee);
            }

            int numberOfAffectedRows = _DatabaseContext.SaveChanges();
            if (numberOfAffectedRows != employeeIdsToDelete.Count) 
                throw new DataNotUpdatedException("One or more employee records have not been deleted.");
            foreach (Employee employeeToBeDeleted in employeesToBeDeleted)
                OnEmployeeDeleted(employeeToBeDeleted);
        }

        #endregion
	
	}
}