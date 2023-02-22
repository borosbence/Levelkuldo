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

        private bool _isSending;
        public bool IsSending
        {
            get { return _isSending; }
            set { SetProperty(ref _isSending, value); }
        }

        private bool sent = true;
        public bool Sent
        {
            get { return sent; }
            set { SetProperty(ref sent, value); }
        }

        private readonly FileDialogService _dialogService;
        private readonly EmailService _emailService;
        private readonly LogService _logService;
        private List<string> emailCimek;
        public IAsyncRelayCommand<string> ImportCommandAsync { get; }
        public IAsyncRelayCommand SendMailCommandAsync { get; }

        public UzenetViewModel(FileDialogService dialogService, LogService logService, EmailService emailService)
        {
            _dialogService = dialogService;
            _logService = logService;
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
                var result = await _dialogService.PickFileToString(title, fileTypes);
                Uzenet = result;
                _logService.Insert("Üzenet betöltve.");
            }
            else if (param == "címzettek")
            {
                fileTypes.Add("*.txt");
                emailCimek = await _dialogService.PickFileToList(title, fileTypes);
                foreach (var item in emailCimek)
                {
                    _logService.Insert($"Címzett: {item} betöltve.");
                }
            }
            MessagingCenter.Send(this, "log");
        }

        private async Task SendMail()
        {
            IsSending = true;
            Sent = !IsSending;
            foreach (var cim in emailCimek)
            {
                try
                {
                    await _emailService.SendEmailAsync(Felado, cim, Targy, Uzenet);
                    _logService.Insert($"Sikeres üzenet küldés: {cim} .");
                }
                catch (Exception ex)
                {
                    _logService.Insert($"Hiba: {ex.Message}");
                }
            }
            MessagingCenter.Send(this, "log");
            IsSending = false;
            Sent = !IsSending;
        }
    }
}
