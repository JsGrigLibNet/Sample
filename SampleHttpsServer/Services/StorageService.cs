namespace SampleHttpsServer
{
    using JsGridLib.Contracts;
    using JsGridLib.Models;
    using PecanDB;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class StorageService<T> : IDisposable, IJsGridStorage<T>
        where T : IJsGridEntity
    {
        public StorageService()
        {
            this.Store = new PecanDocumentStore("JsGridSample", new DatabaseOptions(true, true)
            {
            });
        }

        public PecanDocumentStore Store { get; set; }

        JsGridStorageStatistics<T> IJsGridStorage<T>.LoadAll(T sampleForFilter, Func<IEnumerable<T>, T, IEnumerable<T>> clientSideFiltering, int take, int skip)
        {
            using (var session = this.Store.OpenSession())
            {
                var query = session.Query<T>(q => q);
                var result = query.Skip(skip).Take(take);
                return new JsGridStorageStatistics<T>()
                {
                    Results = result,
                    Total = query.Count(x => x != null)
                };
            }
        }

        public T LoadById(string id)
        {
            using (var session = this.Store.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        private void SetObjectProperty(string propertyName, string value, object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            // make sure object has the property we are after
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public void Update(string id, T client)
        {
            using (var session = this.Store.OpenSession())
            {
                var data = session.Load<T>(id);
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.ToLower() != "id")
                    {
                        property.SetValue(data, GetPropValue(client, property.Name));
                    }
                }
                session.SaveChanges(true);
            }
        }

        public void Save(T client)
        {
            client.Id = Guid.NewGuid().ToString();
            using (var session = this.Store.OpenSession())
            {
                session.Save(client, client.Id);
            }
        }

        public void Delete(string id, T client)
        {
            using (var session = this.Store.OpenSession())
            {
                session.DeleteForever<T>(id);
            }
        }

        public void Dispose()
        {
            this.Store.Dispose();
        }
    }
}