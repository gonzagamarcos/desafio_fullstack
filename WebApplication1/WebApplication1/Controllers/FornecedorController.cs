using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebApplication1.Controllers
{
    public class FornecedorController
    {
        private IConfiguration Configuration { get; set; }
        private string MySqlConection { get; set; }
        public FornecedorController(IConfiguration configuration)
        {
            Configuration = configuration;
            MySqlConection = configuration.GetConnectionString("DefaultConnection");
        }
        //Função para inserir o cadastro das fornecedores no banco de dados MySQL
        public void InserirForncedor(string connectionString, string codigo, string nome, string email, string cep, string rg = null, DateTime? datanascimento = null)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO fornecedores (codigo, nome, email, cep, rg, datanascimento) VALUES (@codigo, @nome, @email, @cep, @rg, @datanascimento)";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@cep", cep);
                cmd.Parameters.AddWithValue("@rg", rg);
                cmd.Parameters.AddWithValue("@datanascimento", datanascimento);
                

                cmd.ExecuteNonQuery();

                Console.WriteLine("Forncedor Cadastrado com Sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Inserir Forncedor: " + ex.Message);
            }
        }

        // Função para consultar as fornecedores no banco de dados MySQL
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT codigo, nome, email, cep FROM fornecedores";

                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar fornecedor: " + ex.Message);
            }

            return new JsonResult(table);
        }

        public void AlterarForncedor(string connectionString, int id_fornecedor, string codigo, string nome, string email, string cep, string rg = null, DateTime? datanascimento = null)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE fornecedores SET codigo = @codigo, nome = @nome, email = @email, cep = @cep, rg = @rg, datanascimento = @datanascimento WHERE id_fornecedor = @id_fornecedor";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_fornecedor", id_fornecedor);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@cep", cep);
                cmd.Parameters.AddWithValue("@rg", rg);
                cmd.Parameters.AddWithValue("@datanascimento", datanascimento);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Nome da Forncedor Alterado com Sucesso!");


            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Atualizar Forncedor: " + ex.Message);

            }
        }

        // Função para deletar os fornecedores no banco de dados MySQL
        public void DeletarForncedor(string connectionString, int id_fornecedore)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM fornecedores WHERE id_fornecedor = @id_fornecedor";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_fornecedor", id_fornecedore);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Forncedor Deletado com Sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao Deletar Forncedor: " + ex.Message);
            }
        }
    }
}
