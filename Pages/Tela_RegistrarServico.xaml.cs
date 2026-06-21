using MinhaMoto.ViewModel;

namespace MinhaMoto.Pages;

public partial class Tela_RegistrarServico : ContentPage
{
	private RegistrarServicoViewModel _viewModel;
	public Tela_RegistrarServico(RegistrarServicoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await Task.Delay(250);
		/*await Task.Run(async() => */await _viewModel.CarregarDados();
    }

   
}