using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Infrastructure
{
    public interface IRepositoryCommand
    {
        Task<string> CreateAsync();
    }
}
