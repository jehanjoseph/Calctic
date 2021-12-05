using System.Threading.Tasks;
using Calctic.Interfaces;
using Xamarin.Forms;

namespace Calctic.Services
{
    public class MessagingService : IMessagingService
    {
        public async Task ShowAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert(Application.Current.MainPage.Title, message, "Ok");
        }
    }
}
