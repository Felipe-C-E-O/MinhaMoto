using MinhaMoto.Models;
using MinhaMoto.Pages;
using MinhaMoto.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace MinhaMoto.ViewModel
{
    [QueryProperty(nameof(VeiculoRecebido), "VeiculoClicado")]
    public class ListaServicoViewModel : INotifyPropertyChanged
    {
        private ServicoService _servicoService;
        public ObservableCollection<Servico> ListaServicoDoVeiculo { get; set; } = new();
        public ListaServicoViewModel(ServicoService servicoService)
        {
            _servicoService = servicoService;
            GoTelaRegistrarServicoCommand = new Command(async () => await GoTelaRegistrarServico());
            GoToEditarServicoCommand = new Command<Servico>(async(S) =>  await GoToEditarServico(S));
        }

        #region Command
        public ICommand GoTelaRegistrarServicoCommand { get; }
        public ICommand GoToEditarServicoCommand { get; }
        #endregion
        #region Metodos
        public async Task GoTelaRegistrarServico()
        {
            var parametros = new Dictionary<string, object>
            {
                {"IdVeiculo",VeiculoRecebido}
            };
            await Shell.Current.GoToAsync(nameof(Tela_RegistrarServico),parametros);
        }
        public async Task GoToEditarServico(Servico servico)
        {
            var parametros = new Dictionary<string, object>
            {
                {"servicoClicado",servico}
            };
            await Shell.Current.GoToAsync(nameof(Tela_RegistrarServico), parametros);
        }
        public async Task CarregarDados()
        {
            ListaServicoDoVeiculo.Clear();
            var dados = await _servicoService.GetAllPorVeiculo(_veiculoRecebido.Id);
            foreach (var item in dados)
            {
                ListaServicoDoVeiculo.Add(item);
            }
        }

        #endregion

        private Veiculo _veiculoRecebido;
        public Veiculo VeiculoRecebido
        {
            get => _veiculoRecebido;
            set
            {
                DefinirPropriedade(ref _veiculoRecebido, value);
            }
        }

        #region INotifyProperty
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
