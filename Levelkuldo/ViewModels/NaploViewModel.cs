using CommunityToolkit.Mvvm.ComponentModel;
using Levelkuldo.Services;

namespace Levelkuldo.ViewModels
{
    public class NaploViewModel : ObservableObject
    {
        private string _bejegyzes;
        public string Bejegyzes
        {
            get { return _bejegyzes; }
            set { SetProperty(ref _bejegyzes, value); }
        }

        public NaploViewModel()
        {
            FrissitBejegyzesek();
            RegisterMessages();
        }

        private void FrissitBejegyzesek()
        {
            Bejegyzes = string.Join(Environment.NewLine, LogService.Logs);
        }

        private void RegisterMessages()
        {
            // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/messagingcenter?view=net-maui-6.0
            MessagingCenter.Subscribe<UzenetViewModel>(this, "log", (sender) =>
            {
                FrissitBejegyzesek();
            });
        }
    }
}
