using MinhaMoto.ViewModel;

namespace MinhaMoto.Pages;

public partial class Tela_RegistrarMeterial : ContentPage
{
	public Tela_RegistrarMeterial(RegistrarMaterialViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}