using MinhaMoto.Models;
using MinhaMoto.Repositorio;


namespace MinhaMoto.Services
{
    public class ServicoService
    {
        private readonly DataBase _dataBase;
        public ServicoService(DataBase dataBase)
        {
            _dataBase = dataBase;
        }
        public async Task<int> Salvar(Servico servico)
        {
            if (servico.Id == 0)
            {
                return await _dataBase._connection.InsertAsync(servico);
            }
            else
            {
                return await _dataBase._connection.UpdateAsync(servico);
            }
        }

        public async Task<List<Servico>> GetAll()
        {
            return await _dataBase._connection.Table<Servico>().ToListAsync();
        }

        public async Task<int> Deletar(Servico servico)
        {
            return await _dataBase._connection.DeleteAsync(servico);
        }
        public async Task<List<Servico>> GetAllPorVeiculo(int Idveiculo)
        {
            var dados = await _dataBase._connection.Table<Servico>().ToListAsync();
            return dados.Where(x => x.IdVeiculo == Idveiculo).ToList();
        }

        public async Task<int> GetUltimoId()
        {
            var dados =  await _dataBase._connection.Table<Servico>().ToListAsync();
            return dados.Max(item => item.Id);
        }
    }
}
