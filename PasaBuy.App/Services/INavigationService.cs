using System.Threading.Tasks;
using Xamarin.Forms;

namespace PasaBuy.App.Services
{
    public interface INavigationService
    {
        string PreviousPage { get; }
        Task InitializeAsync();
        Task RemoveModalAsync();
        Task NavigateToPageAsync(Page page, object parameter);
        Task NavigateToRootPage();
        Task RemoveLastFromBackStackAsync();
        Task RemovePopupAsync();
        Task RemoveBackStackAsync();
    }
}
