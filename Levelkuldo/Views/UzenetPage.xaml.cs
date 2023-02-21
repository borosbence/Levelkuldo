using Levelkuldo.ViewModels;

namespace Levelkuldo.Views;

public partial class UzenetPage : ContentPage
{
    public UzenetPage(UzenetViewModel viewModel)
    {
        BindingContext= viewModel;
        InitializeComponent();
    }
}