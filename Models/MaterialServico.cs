using SQLite;

namespace MinhaMoto.Models
{
    public class MaterialServico
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        [Indexed]
        public int IdServico { get; set; }
    }
}
