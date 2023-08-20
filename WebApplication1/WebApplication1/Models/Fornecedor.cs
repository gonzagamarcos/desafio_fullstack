using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public class Fornecedor : Empresa
    {
        public string Email { get; set; }
        public bool PessoaFisica { get; set; }
        public string Rg { get; set; }
        public string DataNascimento { get; set; }

        public Fornecedor (string codigo, string nome, string cep, string email, bool pessoaFisica, string rg = null, string dataNascimento = null)
            : base(codigo, nome, cep) 
        {
            Email = email;
            PessoaFisica = pessoaFisica;
            Rg = rg;
            DataNascimento = dataNascimento;

        }
    }
}
