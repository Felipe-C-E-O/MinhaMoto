using SQLite;

namespace MinhaMoto.Models
{
    public class Veiculo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nome { get; set; }
        public string Icon { get; set; }
        public int Ano { get; set; }
    }
}
