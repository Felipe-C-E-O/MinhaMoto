using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MinhaMoto.Repositorio;
using MinhaMoto.Services;
using MinhaMoto.ViewModel;
using MinhaMoto.Pages;


namespace MinhaMoto;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//Injeção
		builder.Services.AddSingleton<DataBase>();
		builder.Services.AddSingleton<VeiculoServices>();
		builder.Services.AddSingleton<ServicoService>();
		builder.Services.AddSingleton<MaterialServicoService>();
		builder.Services.AddTransient<PrincipalViewModel>();
		builder.Services.AddTransient<Principal>();
		builder.Services.AddTransient<AdicionarVeiculoViewModel>();
		builder.Services.AddTransient<Tela_CadastrarVeiculo>();
		builder.Services.AddTransient<Tela_ListaDeServico>();
		builder.Services.AddTransient<ListaServicoViewModel>();
		builder.Services.AddTransient<RegistrarServicoViewModel>();
		builder.Services.AddTransient<Tela_RegistrarServico>();
        builder.Services.AddTransient<Tela_RegistrarMeterial>();
        builder.Services.AddTransient<RegistrarMaterialViewModel>();
      

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
