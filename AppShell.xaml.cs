using MinhaMoto.Pages;

namespace MinhaMoto;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(Tela_CadastrarVeiculo),typeof(Tela_CadastrarVeiculo));
		Routing.RegisterRoute(nameof(Tela_ListaDeServico), typeof(Tela_ListaDeServico));
		Routing.RegisterRoute(nameof(Tela_RegistrarServico), typeof(Tela_RegistrarServico));
        Routing.RegisterRoute(nameof(Tela_RegistrarMeterial), typeof(Tela_RegistrarMeterial));
    }
}
