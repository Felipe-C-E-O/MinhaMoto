namespace MinhaMoto.Componentes;

public partial class CollectionView_Tela_Principal : ContentView
{
	public CollectionView_Tela_Principal()
	{
		InitializeComponent();
	}

    private async void ContentView_Loaded(object sender, EventArgs e)
    {
		await Task.Delay(20);
		this.Scale = 0;
		await this.ScaleTo(1, 200, Easing.SpringOut);
    }
}