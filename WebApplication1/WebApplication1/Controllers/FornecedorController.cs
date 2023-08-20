using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        public JsonResult Post(Fornecedor fornecedor)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO fornecedores (codigo, nome, cep, rg, datanascimento) VALUES (@codigo, @nome, @cep, @rg, @datanascimento)";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codigo", fornecedor.Codigo);
                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cep", fornecedor.Cep);
                cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                cmd.Parameters.AddWithValue("@datanascimento", fornecedor.DataNascimento);

                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar fornecedor: " + ex.Message);
            }

            return new JsonResult("Cadastro Adicionado com Sucesso!");

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

                string query = "SELECT codigo, nome, cep FROM fornecedores";

                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar empresa: " + ex.Message);
            }

            return new JsonResult(table);
        }

        // Função para Alterar Fornecedores no banco de dados MySQL
        [HttpPut]
        public JsonResult Put(Fornecedor fornecedor)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE forncedores SET codigo = @codigo, nome = @nome, cep = @cep, id = @id, datanascimento = @datanascimento WHERE id_empresa = @id_empresa";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codigo", fornecedor.Codigo);
                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cep", fornecedor.Cep);
                cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                cmd.Parameters.AddWithValue("@datanascimento", fornecedor.DataNascimento);

                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar empresa: " + ex.Message);
            }

            return new JsonResult("Cadastro Alterado com Sucesso!");
        }

        // Função para deletar os fornecedores no banco de dados MySQL
        [HttpDelete]
        public JsonResult Delete(Fornecedor fornecedor)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM fornecedores WHERE id_fornecedor = @id_fornecedor";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_fornecedor", fornecedor.Id);
                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar fornecedor: " + ex.Message);
            }

            return new JsonResult("Cadastro Deletado com Sucesso!");

        }
    }
}
