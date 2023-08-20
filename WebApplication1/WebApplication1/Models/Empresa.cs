using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApplication1.Models
{
    public class Empresa
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public object? Id { get; internal set; }

        public Empresa(string codigo, string nome, string cep)
        {
            Codigo = codigo;
            Nome = nome;
            Cep = cep;
        }
        //Função para inserir o cadastro das empresas no banco de dados MySQL
        public void InserirEmpresa(string connectionString, string codigo, string nome, string cep)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO empresas (codigo, nome, cep) VALUES (@codigo, @nome, @cep)";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cep", cep);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Empresa Cadastrada com Sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Inserir Empresa: " + ex.Message);
            }
        }

        // Função para consultar as empresas no banco de dados MySQL
        public void ConsultarEmpresa(string connectionString, int id_empresa)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT codigo, nome, cep FROM empresas WHERE id_empresa = @id_empresa";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_empresa", id_empresa);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar empresa: " + ex.Message);
            }
        }

        // Função para alterar as informações das empresas no banco de dados MySQL
        public void AlterarEmpresa(string connectionString, int id_empresa, string codigo, string nome, string cep)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE empresas SET codigo = @codigo, nome = @nome, cep = @cep WHERE id_empresa = @id_empresa";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_empresa", id_empresa);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cep", cep);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Nome da Empresa Alterado com Sucesso!");

                
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Atualizar Empresa: " + ex.Message);

            }
        }

        // Função para deletar as empresas no banco de dados MySQL
        public void DeletarEmpresa(string connectionString, int id_empresa)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM empresas WHERE id_empresa = @id_empresa";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_empresa", id_empresa);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Empresa Deletada com Sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Deletar Empresa: " + ex.Message);
            }
        }
    }

}
