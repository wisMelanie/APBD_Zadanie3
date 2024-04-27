using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Data.SqlClient;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsControllers : ControllerBase
    {
        public IConfiguration _configuration;

        public AnimalsControllers(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AnimalDTO>> GetAnimals()
        {
            using SqlConnection con = new SqlConnection();

            using SqlCommand com = new SqlCommand();
            com.Connection = con;

            com.CommanText = "SELECT * FROM Animals";
            con.Open();
            SqlDataReader reader = com.ExecuteReader();

            List<Animal> animals = new List<Animal>();
            while (reader.Read())
            {
                Animal a = new Animal();
                a.Id = (int)reader["id"];
                a.Name = (string)reader["name"];
                a.Description = (string)reader["description"];
                a.Area = (string)reader["area"];

                animals.Add(a);

            }

            return Ok(animals); ;

        }

        [HttpPost]
        public ActionResult CreateAnimal(AnimalCreationDTO animal) {
            using SqlConnection con = new SqlConnection();

            using SqlCommand com = new SqlCommand();
            com.Connection = con;

            com.CommanText = "INSERT INTO Animals(Name, Description, Area, Category) VALUES (@Name, @Description, @Area, @Category); SELECT SCOPE";
            com.Parameters.AddWithValue("@Name", animal.Name);
            com.Parameters.AddWithValue("@Description", animal.Description);
            // i area i category

            con.Open();

            var affectedCount = (int)com.ExecuteScalar();

            return Created((int)affectedCount);


        }
    }

    
}
