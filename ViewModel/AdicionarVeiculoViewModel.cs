
using MinhaMoto.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MinhaMoto.ViewModel
{
    public class AdicionarVeiculoViewModel : INotifyPropertyChanged
    {
        private string _nome;
        public string Nome 
        {
            get => _nome;
            set => DefinirPropriedade(ref _nome, value);
        }
        private int _ano;
        public int Ano
        {
            get => _ano;
            set => DefinirPropriedade(ref _ano, value);
        }
        private string _icon;
        public string Icon
        {
            get => _icon;
            set => DefinirPropriedade(ref _icon, value);
        }
        private readonly VeiculoServices _veiculoServices;
        public ObservableCollection<string> ListaDeVeiculos { get; set; } = new();
        public AdicionarVeiculoViewModel(VeiculoServices veiculoService)
        {
            _veiculoServices = veiculoService;
            SalvarCommand = new Command(async () => await Salvar());
            Ano = DateTime.Now.Year;
            Task.Run(async () => await Carregar());
        }

        #region Commands
        public ICommand SalvarCommand { get; }
        #endregion

        #region Metodos
        public async Task Salvar()
        {
            if(string.IsNullOrEmpty(Nome) || Ano == 0 || string.IsNullOrWhiteSpace(Icon))
            {
                await Shell.Current.DisplayAlert("Erro", "Por Favor Preencha \n[Nome]\n[Ano]", "OK");
                return;
            }

            try
            {
                await _veiculoServices.Salvar(new Models.Veiculo
                {
                    Nome = Nome.ToUpper(),
                    Ano = Ano,
                    Icon = Icon

                });               
                await Shell.Current.GoToAsync("..");
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Tentativa de Salvar Dados\n{ex.Message}", "OK");
            }
           
        }
        public async Task Carregar()
        {
            ListaDeVeiculos.Clear();
            ListaDeVeiculos.Add("aeronave.png");
            ListaDeVeiculos.Add("bicicleta.png");
            ListaDeVeiculos.Add("caminhao.png");
            ListaDeVeiculos.Add("carro.png");
            ListaDeVeiculos.Add("moto.png");
        }
        #endregion

        #region INotify Property Changed
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DefinirPropriedade<T>(ref T CampoPrivado , T Value, [CallerMemberName]string? PropertyName = null)
        {
            if (Equals(CampoPrivado, Value)) return;
            CampoPrivado = Value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

    }
}
