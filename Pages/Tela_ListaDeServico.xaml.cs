
using MinhaMoto.ViewModel;
using System.Threading.Tasks;

namespace MinhaMoto.Pages;

public partial class Tela_ListaDeServico : ContentPage
{
	private ListaServicoViewModel _viewModel;
	public Tela_ListaDeServico(ListaServicoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await CarregarAnimacao();
		await Task.Run(async () => await _viewModel.CarregarDados());
    }
	public async Task CarregarAnimacao()
	{
		await Task.Delay(150);
		Cabecario.Scale = 0;
		await Cabecario.ScaleTo(1, 230, Easing.SpringOut);
	}
}