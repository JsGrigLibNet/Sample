namespace SampleHttpsServer
{
    using System.Collections.Generic;

    public class AppData
    {
        public string Name { set; get; }

        public bool? IsAuthenticated { get; set; }

        public List<ALink> Links { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationTitle { get; set; }

        public string CompanyName { get; set; }

        public int Year { get; set; }
    }
}