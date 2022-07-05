using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public long Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public object DataAtualizacao { get; set; }
        public bool Deletado { get; set; }

    }
}
