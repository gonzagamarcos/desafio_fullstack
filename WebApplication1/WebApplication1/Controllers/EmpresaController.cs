using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using System.Runtime.ConstrainedExecution;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private IConfiguration Configuration { get; set;  }
        private string MySqlConection { get; set; }
        public EmpresaController(IConfiguration configuration)
        {
            Configuration = configuration;
            MySqlConection = configuration.GetConnectionString("DefaultConnection");
        }
        //Função para inserir o cadastro das empresas no banco de dados MySQL
        [HttpPost]
        public JsonResult Post (Empresa empresa)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO empresas (codigo, nome, cep) VALUES (@codigo, @nome, @cep)";

                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codigo", empresa.Codigo);
                cmd.Parameters.AddWithValue("@nome", empresa.Nome);
                cmd.Parameters.AddWithValue("@cep", empresa.Cep);

                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar empresa: " + ex.Message);
            }

            return new JsonResult("Cadastro Adicionado com Sucesso!");
            
        }

        // Função para consultar as empresas no banco de dados MySQL
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT codigo, nome, cep FROM empresas";

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

        // Função para alterar as fornecedores no banco de dados MySQL
        [HttpPut] 
        public JsonResult Put(Empresa empresa)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE empresas SET codigo = @codigo, nome = @nome, cep = @cep WHERE id_empresa = @id_empresa";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_empresa", empresa.Id);
                cmd.Parameters.AddWithValue("@codigo", empresa.Codigo);
                cmd.Parameters.AddWithValue("@nome", empresa.Nome);
                cmd.Parameters.AddWithValue("@cep", empresa.Cep);

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

        // Função para deletar as empresas no banco de dados MySQL
        [HttpDelete]
        public JsonResult Delete(Empresa empresa)
        {
            DataTable table = new DataTable();
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM empresas WHERE id_empresa = @id_empresa";
                using MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id_empresa", empresa.Id);
                using MySqlCommand command = new MySqlCommand(query, connection);
                table.Load(command.ExecuteReader());
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao consultar empresa: " + ex.Message);
            }

            return new JsonResult("Cadastro Deletado com Sucesso!");
            
        }
    }
}
