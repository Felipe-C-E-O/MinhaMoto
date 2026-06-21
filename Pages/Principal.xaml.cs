
using MinhaMoto.Componentes;
using MinhaMoto.ViewModel;

namespace MinhaMoto.Pages;

public partial class Principal : ContentPage
{
	PrincipalViewModel viewModel;
	public Principal(PrincipalViewModel principalViewModel)
	{
		InitializeComponent();
		BindingContext = principalViewModel;
		viewModel = principalViewModel;
		Titulo.Text = "Acompanhe Aqui!\nAs manutenções\ndos seus Veiculos!";
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		if(viewModel != null)
		{
            //Task tarefaAnimacao =  CarregarAnimacao();
            //Task tarefaDados = Task.Run(async () => await viewModel.Carregar());

            //await Task.WhenAll(tarefaAnimacao, tarefaDados);
            _ = viewModel.Carregar();
            _= CarregarAnimacao();
			
        }
    }
	public async Task CarregarAnimacao()
	{
		viewModel.IsBusy = true;
		Container.Clear();
		// Na Pratica Posso deixar um Delay Menor
		await Task.Delay(80);		
		Cabecario.Scale = 0;
		await Task.Delay(90);
		await Cabecario.ScaleTo(1, 200, Easing.SpringOut);
		//await Task.Delay(120);
		Container.Children.Add(new CollectionView_Tela_Principal());
        viewModel.IsBusy = false;
    }

}