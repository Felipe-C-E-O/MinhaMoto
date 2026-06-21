using MinhaMoto.Models;
using MinhaMoto.Pages;
using MinhaMoto.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MinhaMoto.ViewModel
{
    public class PrincipalViewModel : INotifyPropertyChanged
    {
        private readonly VeiculoServices _veiculoService;

        private ObservableCollection<Veiculo> _listaVeiculos = new();
        public ObservableCollection<Veiculo> ListaVeiculos
        {
            get => _listaVeiculos;
            set => DefinirPropriedade(ref  _listaVeiculos, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => DefinirPropriedade(ref _isBusy, value);
        }
        public PrincipalViewModel(VeiculoServices veiculoService)
        {
            _veiculoService = veiculoService;
            TelaCadastrarVeiculoCommand = new Command(async () => await TelaCadastrarVeiculo());
            DeletarCommand = new Command<Veiculo>(async (c) => await Deletar(c));
            TelaListaDeServicoCommand = new Command<Veiculo>(async (v) => await TelaListaDeServico(v));
        }

        #region Command
        public ICommand TelaCadastrarVeiculoCommand { get; }
        public ICommand DeletarCommand { get; }
        public ICommand TelaListaDeServicoCommand { get; }
        #endregion

        #region Metodos
        public async Task TelaListaDeServico(Veiculo veiculo)
        {
            var _veiculoParaEnviar = new Dictionary<string, Object>
            {
                {"VeiculoClicado",veiculo }
            };

            await Shell.Current.GoToAsync($"\\{nameof(Tela_ListaDeServico)}",_veiculoParaEnviar);
        }
        public async Task TelaCadastrarVeiculo()
        {
            await Shell.Current.GoToAsync(nameof(Tela_CadastrarVeiculo));
        }
        public async Task Carregar()
        {
            
            ListaVeiculos.Clear();
            var lista = await _veiculoService.GetAll();
            var temp = new ObservableCollection<Veiculo>();
            foreach(var item in lista)
            {
                temp.Add(item);
            }
            ListaVeiculos = temp;
            
        }
        public async Task Deletar(Veiculo veiculo)
        {
            if(veiculo != null)
            {
                var resposta = await Shell.Current.DisplayAlert("Confirmação", "Tem certeza que quer excluir", "Sim", "Não");
                if(resposta)
                {
                    ListaVeiculos.Remove(veiculo);
                    await _veiculoService.DeletarVeiculo(veiculo);
                }
                
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DefinirPropriedade<T>(ref T CampoPrivado,T Value, [CallerMemberName] string? Propriedade = null)
        {
            if (Equals(CampoPrivado, Value)) return;
            CampoPrivado = Value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propriedade));
        }
        #endregion
    }
}
