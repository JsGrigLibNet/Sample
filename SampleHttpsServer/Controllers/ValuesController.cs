namespace SampleHttpsServer
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using JsGridLib.Contracts;
    using JsGridLib.Controller;

    [Authorize]
    public class GridController : GenericJsGridController
           <Employee,ViewableEmployee, ViewableEmployee, ViewableEmployee,ViewableEmployee>
    {

       static Func<ViewableEmployee, Employee> ViewableEmployeeConverter = (s) => new Employee()
        {
            EmployeePosition = s.Position,
            EmployeeName = s.Name,
            Id = s.Id,
            EmployeeContect = s.Contact,
            Boo = s.Baa
        };
        static Func<Employee, ViewableEmployee> EmployeeConverter = (s) => new ViewableEmployee()
        {
            Position = s.EmployeePosition,
            Name = s.EmployeeName,
            Id = s.Id,
            Contact = s.EmployeeContect,
            Baa = s.Boo
        };
        public GridController()
            : base(false,(db, filter) =>
                {
                    return db.Where(c => c != null);
                }, 
                ValidationObj.Validation,
                new PecanDbService(),
                ViewableEmployeeConverter, EmployeeConverter, 
                ViewableEmployeeConverter,ViewableEmployeeConverter,ViewableEmployeeConverter)
        {
        }
    }
}