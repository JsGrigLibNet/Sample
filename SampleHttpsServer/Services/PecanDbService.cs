namespace SampleHttpsServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PecanDB;

    public class PecanDbService : IJsGridStorage<Employee>,IDisposable
    {
        public PecanDbService()
        {
            this.Store = new PecanDocumentStore("JsGridSample", new DatabaseOptions(true, true)
            {
                
            });
        }
        
        public PecanDocumentStore Store { get; set; }

        public JsGridStorageStatistics<Employee> LoadAll(Employee sampleForFilter, Func<IEnumerable<Employee>, Employee, IEnumerable<Employee>> clientSideFiltering, int take, int skip)
        {
            using (var session=this.Store.OpenSession())
            {
                var query = session.Query<Employee>(q => q);
                var result= query.Skip(skip).Take(take);
                return new JsGridStorageStatistics<Employee>()
                {
                    Results = result,
                    Total = query.Count(x=>x!=null)
                };
            }
        }

        public Employee LoadById(string id)
        {
            using (var session = this.Store.OpenSession())
            {
                return session.Load<Employee>(id);
            }
        }

        public void Update(string id, Employee client)
        {
           
            using (var session = this.Store.OpenSession())
            {
                var data= session.Load<Employee>(id);
                data.EmployeeContect = client.EmployeeContect;
                data.EmployeeName = client.EmployeeName;
                data.EmployeePosition = client.EmployeePosition;
                session.SaveChanges(true);
            }
            
        }

        public void Save(Employee client)
        {

            client.Id = Guid.NewGuid().ToString();
            using (var session = this.Store.OpenSession())
            {
                session.Save(client, client.Id);
            }
        }

        public void Delete(string id, Employee client)
        {
            using (var session = this.Store.OpenSession())
            {
                session.DeleteForever<Employee>(id);
            }
        }

        public void Dispose()
        {
            this.Store.Dispose();
        }
    }
}