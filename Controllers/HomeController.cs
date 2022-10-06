using Microsoft.AspNetCore.Mvc;
using Npgsql;
using pruebaConexionPostgreSQLV.Models.Conexiones;
using pruebaConexionPostgreSQLV.Models;
using pruebaConexionPostgreSQLV.Util;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using pruebaConexionPostgreSQLV.Models.DTOs;
using pruebaConexionPostgreSQLV.Models.Consultas;

namespace pruebaConexionPostgreSQLV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ConexionPostgreSQL conexionPostgreSQL)
        {
            // Profesor

            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Entra en Index");
            
            //CONSTANTES
            const string HOST = VariablesConexionPostgreSQL.HOST;
            const string PORT = VariablesConexionPostgreSQL.PORT;
            const string USER = VariablesConexionPostgreSQL.USER;
            const string PASS = VariablesConexionPostgreSQL.PASS;
            const string DB = VariablesConexionPostgreSQL.DB;
            
            //Se genera una conexión a PostgreSQL y validamos que esté abierta fuera del método
            var estadoGenerada = "";
            NpgsqlConnection conexionGenerada = new NpgsqlConnection();
            List<AlumnoDTO> listAlumnoDTO = new List<AlumnoDTO>();

            //NpgsqlCommand consulta = new NpgsqlCommand();
            conexionGenerada = conexionPostgreSQL.GeneraConexion(HOST, PORT, DB, USER, PASS);
            estadoGenerada = conexionGenerada.State.ToString();
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Estado conexión generada: " + estadoGenerada);

            //Se realiza la consulta y se guarda una lista de alumnosDTO
            listAlumnoDTO = ConsultasPostgreSQL.ConsultaSelectPostgreSQL(conexionGenerada);
            int cont = listAlumnoDTO.Count();
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista compuesta por: "+cont+" alumnos");
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista alumnos: " + listAlumnoDTO[0].id_alumno
                + listAlumnoDTO[0].nombre + listAlumnoDTO[0].apellidos + listAlumnoDTO[0].email);
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista alumnos: " + listAlumnoDTO[1].id_alumno
                + listAlumnoDTO[1].nombre + listAlumnoDTO[1].apellidos + listAlumnoDTO[1].email);
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista alumnos: " + listAlumnoDTO[2].id_alumno
                + listAlumnoDTO[2].nombre + listAlumnoDTO[2].apellidos + listAlumnoDTO[2].email);
            System.Console.WriteLine("[INFORMACIÓN-HomeController-Index] Lista alumnos: " + listAlumnoDTO[3].id_alumno
                + listAlumnoDTO[3].nombre + listAlumnoDTO[3].apellidos + listAlumnoDTO[3].email);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}