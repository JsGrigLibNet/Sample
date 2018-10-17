namespace SampleHttpsServer
{
    using JsGridLib.Controller;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class BossController : GenericJsGridController<Boss>
    {
        public BossController()
            : base(
                false,
                (db, filter) => { return db.Where(c => c != null); },
                new { rules = new { } },
                new StorageService<Boss>())
        {
        }
    }
}