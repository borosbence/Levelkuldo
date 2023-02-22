namespace Levelkuldo.Services
{
    public class FileDialogService
    {
        // https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-picker?view=net-maui-6.0&tabs=android
        public async Task<string> PickFileToString(string title, List<string> types)
        {
            var fileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, types },
                    { DevicePlatform.Android, types }
                });

            PickOptions options = new()
            {
                PickerTitle = title,
                FileTypes = fileTypes,
            };
            var fileResult = await FilePicker.Default.PickAsync(options);
            if (fileResult != null)
            {
                using (var sr = new StreamReader(fileResult.FullPath))
                {
                    return sr.ReadToEnd();
                }
            }
            return await Task.FromResult<string>(null);
        }

        public async Task<List<string>> PickFileToList(string title, List<string> types)
        {
            var fileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, types },
                    { DevicePlatform.Android, types }
                });

            PickOptions options = new()
            {
                PickerTitle = title,
                FileTypes = fileTypes,
            };

            List<string> result = new();

            var fileResult = await FilePicker.Default.PickAsync(options);
            if (fileResult != null)
            {
                using (var sr = new StreamReader(fileResult.FullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        result.Add(sr.ReadLine());
                    }
                }
            }
            return await Task.FromResult(result);
        }
    }
}
