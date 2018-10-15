namespace SampleHttpsServer
{
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class GridController : GenericJsGridController<Employee,ViewableEmployee>
    {
        public GridController()
            : base((db, filter) =>
                {
                    return db.Where(c => c != null);
                }, 
                ValidationObj.Validation, 
                new PecanDbService(), 
                (s) => new Employee()
                {
                    EmployeePosition = s.Position,
                    EmployeeName = s.Name,
                    Id = s.Id,
                    EmployeeContect = s.Contact
                },
                (s)=> new ViewableEmployee()
                {
                    Position = s.EmployeePosition,
                    Name = s.EmployeeName,
                    Id = s.Id,
                    Contact = s.EmployeeContect
                })
        {
        }
    }
}