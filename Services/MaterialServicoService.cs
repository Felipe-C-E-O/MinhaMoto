using MinhaMoto.Models;
using MinhaMoto.Repositorio;


namespace MinhaMoto.Services
{
    public class MaterialServicoService
    {
        private readonly DataBase _dataBase;
        public MaterialServicoService(DataBase database)
        {
            _dataBase = database;
        }

        public async Task<int> Salvar(MaterialServico materialServico)
        {
            if (materialServico.Id == 0)
            {
                return await _dataBase._connection.InsertAsync(materialServico);
            }
            else
            {
                return await _dataBase._connection.UpdateAsync(materialServico);
            }
        }
        public async Task<int> SalvarTodos(List<MaterialServico> listaDeMaterial)
        {
            if (!listaDeMaterial.Any()) return 0;
            return await _dataBase._connection.InsertAllAsync(listaDeMaterial);
        }
        public async Task<int> DeletarTodos(int idServico)
        {
            if(idServico == 0) return 0;
            string sql = $"delete  from MaterialServico where IdServico = {idServico}";
            return await _dataBase._connection.ExecuteAsync(sql);
        }

        public async Task<List<MaterialServico>> GetAll()
        {
            return await _dataBase._connection.Table<MaterialServico>().ToListAsync();
        }

        public async Task<int> Deletar(MaterialServico materialServico)
        {
            return await _dataBase._connection.DeleteAsync(materialServico);
        }
    }
}
