using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Levelkuldo.Services;

namespace Levelkuldo.ViewModels
{
    public class UzenetViewModel : ObservableObject
    {
        private string _felado;
        public string Felado
        {
            get { return _felado; }
            set { SetProperty(ref _felado, value); }
        }

        private string _targy;
        public string Targy
        {
            get { return _targy; }
            set { SetProperty(ref _targy, value); }
        }

        private string _uzenet;
        public string Uzenet
        {
            get { return _uzenet; }
            set { SetProperty(ref _uzenet, value); }
        }

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }

        private bool loaded = true;
        public bool Loaded
        {
            get { return loaded; }
            set { SetProperty(ref loaded, value); }
        }

        private FileDialogService _dialogService;
        private EmailService _emailService;
        private List<string> emailCimek;
        public IAsyncRelayCommand<string> ImportCommandAsync { get; }
        public IAsyncRelayCommand SendMailCommandAsync { get; }

        public UzenetViewModel(FileDialogService dialogService, EmailService emailService)
        {
            _dialogService = dialogService;
            _emailService = emailService;
            emailCimek = new();
            ImportCommandAsync = new AsyncRelayCommand<string>(PickFile);
            SendMailCommandAsync = new AsyncRelayCommand(SendMail);
        }

        private async Task PickFile(string param)
        {
            List<string> fileTypes = new();
            string title = null;
            if (param == "üzenet")
            {
                fileTypes.Add("*.html");
                fileTypes.Add("*.htm");
                Loading = true;
                Loaded = !Loading;
                var result = await _dialogService.PickFileToString(title, fileTypes);
                Uzenet = result;
                LogService.Insert("Üzenet betöltve.");
            }
            else if (param == "címzettek")
            {
                fileTypes.Add("*.csv");
                Loading = true;
                Loaded = !Loading;
                emailCimek = await _dialogService.PickFileToList(title, fileTypes);
                foreach (var item in emailCimek)
                {
                    LogService.Insert($"Címzett: {item} betöltve.");
                }
            }
            Loading = false;
            Loaded = !Loading;
            MessagingCenter.Send(this, "log");
        }

        private async Task SendMail()
        {
            Loading = true;
            Loaded = !Loading;
            foreach (var item in emailCimek)
            {
                await _emailService.SendEmailAsync(Felado, item, Targy, Uzenet);
            }
            MessagingCenter.Send(this, "log");
            Loading = false;
            Loaded = !Loading;
        }
    }
}
