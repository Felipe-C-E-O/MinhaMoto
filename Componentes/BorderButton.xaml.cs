using System.Windows.Input;

namespace MinhaMoto.Componentes;

public partial class BorderButton : ContentView
{
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(BorderButton), default(ICommand));
	public BorderButton()
	{
		InitializeComponent();
	}

    public ICommand Command 
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		if(sender is Border borda)
		{
			await borda.ScaleTo(0.8, 100);
			await borda.ScaleTo(1,80);
		}
    }
}