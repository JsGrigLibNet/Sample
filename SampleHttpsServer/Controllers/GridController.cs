namespace SampleHttpsServer
{
    using JsGridLib.Controller;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class GridController : GenericJsGridController
           <Employee, ViewableEmployee, ViewableEmployee, ViewableEmployee, ViewableEmployee>
    {
        private static Func<ViewableEmployee, Employee> ViewableEmployeeConverter = (s) => new Employee()
        {
            EmployeePosition = s.Position,
            EmployeeName = s.Name,
            Id = s.Id,
            EmployeeContect = s.Contact,
            Boo = s.Baa
        };

        private static Func<Employee, ViewableEmployee> EmployeeConverter = (s) => new ViewableEmployee()
        {
            Position = s.EmployeePosition,
            Name = s.EmployeeName,
            Id = s.Id,
            Contact = s.EmployeeContect,
            Baa = s.Boo
        };

        public GridController()
            : base(false, (db, filter) =>
                 {
                     return db.Where(c => c != null);
                 },
                ValidationObj.Validation,
                new StorageService<Employee>(),
                ViewableEmployeeConverter, EmployeeConverter,
                ViewableEmployeeConverter, ViewableEmployeeConverter, ViewableEmployeeConverter)
        {
        }
    }
}