using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

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
