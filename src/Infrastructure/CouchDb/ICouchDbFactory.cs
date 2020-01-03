using System;
using Couchbase.Core;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public interface ICouchDbFactory : IDisposable
    {
        IBucket CreateBucket();
    }
}
