using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlServerCe;

namespace RepositoryPattern.Data
{
    public abstract class AbstractDataMapper<T>
    {
        ///
        protected abstract string TableName { get; }

        protected IDbConnection Connection
        {
            get { return new SqlConnection(ConfigurationManager.ConnectionStrings["RPConnection"].ConnectionString); }
        }

        protected SqlCeConnection Connection2
        {
            get { return new SqlCeConnection(ConfigurationManager.ConnectionStrings["RPConnection"].ConnectionString); }
        }



         /// <summary>
    /// find a Single record
    /// 
        public virtual T FindSingle(string query, dynamic param)
        {
            dynamic item = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query(query, (object)param).SingleOrDefault();

                if (result != null) { item = Map(result); }
            }

            return item;
        }

        public virtual IEnumerable<T> FindAll()
        {
            var items = new List<T>();
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query("Select * from " + TableName).ToList();

                for (int i = 0; i < result.Count(); i++)
                {
                    items.Add(Map(result.ElementAt(i)));
                }

            }
            return items;
        }

        public virtual void Delete(int id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + TableName + " WHERE ID = @ID", new { ID = id });
            }
        }

        public abstract T Map(dynamic result);
    }

   
   
}
