using System;
using System.Threading.Tasks;
using Couchbase.Core;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public interface ICouchDbFactoryAsync : IDisposable
    {
        Task<IBucket> CreateBucketAsync();
    }
}
