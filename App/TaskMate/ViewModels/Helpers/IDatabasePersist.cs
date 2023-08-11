using System.Threading.Tasks;

namespace TaskMate.ViewModels.Helpers
{
    public interface IDatabasePersist
    {
        Task<bool> SaveAsync();

        Task<bool> RevertChangesAsync();
    }
}
