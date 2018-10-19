namespace SampleHttpsServer.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using JsGridLib.Contracts;
    using JsGridLib.Controller;

    [Authorize]
    public class GridController : GenericJsGridController<GridController.Employee>
    {
        public static string ApplicationName = "My App";
        public static string CompanyName = "XYZ Company";
        public static string ApplicationTitle = "Data entry platform";
        public static List<ALink> Links=new List<ALink>()
        {
            new ALink("Home","/"),new ALink("About","#"),new ALink("Services","#"),
            new ALink("Pricing","#"),new ALink("Blog","#"),new ALink("Contact","#"),
        };
        public GridController(): base(false, (db, filter) => db.Where(c => c != null)
            ,ValidationObj.Validation,new StorageService<Employee>()){}
        public class Employee : IJsGridEntity
        {
            public string EmployeeName { get; set; }
            public string EmployeePosition { get; set; }
            public string EmployeeContect { get; set; }
            public string Id { get; set; }
            public string Boo { get; set; }
        }
    }

    [Authorize]
    public class GridDetailsController : GenericJsGridController<GridDetailsController.Education>
    {
        public GridDetailsController() : base(false, (db, filter) => db.Where(c => c != null)
            , ValidationObj.Validation, new StorageService<Education>()){ }
        public class Education : IJsGridEntity
        {
            public string SchoolName { get; set; }
            public string EducationLevel { get; set; }

            public string Id { get; set; }
        }
    }
}