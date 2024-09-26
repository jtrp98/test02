using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FingerprintPayment
{
    public class JabjaiSchoolLogsBulkInsert : DbContext
    {
        public JabjaiSchoolLogsBulkInsert() : base("name=JabjaiSchoolLogsContainer")
        {
        }

        public DbSet<TB_Send_API_Log> TB_Send_API_Logs { get; set; }

        public void BulkInsert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                Entry(entity).State = EntityState.Added;
            }
        }

        public partial class TB_Send_API_Log
        {
            public int ID { get; set; }
            public string Api_Name { get; set; }
            public string Info { get; set; }
            public string Result { get; set; }
            public System.DateTime Tstamp { get; set; }
            public int SchoolID { get; set; }
            public Nullable<int> ResponseTime { get; set; }
        }
    }
}