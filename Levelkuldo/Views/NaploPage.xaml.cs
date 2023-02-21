using Levelkuldo.ViewModels;

namespace Levelkuldo.Views;

public partial class NaploPage : ContentPage
{
    public NaploPage(NaploViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}