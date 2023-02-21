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
            MessagingCenter.Subscribe<UzenetViewModel>(this, "log", (sender) =>
            {
                FrissitBejegyzesek();
            });
        }
    }
}
