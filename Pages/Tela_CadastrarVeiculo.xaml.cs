using MinhaMoto.ViewModel;
using System.Threading.Tasks;

namespace MinhaMoto.Pages;

public partial class Tela_CadastrarVeiculo : ContentPage
{

	public Tela_CadastrarVeiculo(AdicionarVeiculoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await Carregar();
	}
	public async Task Carregar()
	{
        await Task.Delay(150);
        Formulario.Scale = 0;        
        await Formulario.ScaleTo(1, 230, Easing.SpringOut);
	}
}