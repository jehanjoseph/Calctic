using System.Threading.Tasks;

namespace Calctic.Interfaces
{
    public interface IMessagingService
    {
        Task ShowAsync(string message);
    }
}
