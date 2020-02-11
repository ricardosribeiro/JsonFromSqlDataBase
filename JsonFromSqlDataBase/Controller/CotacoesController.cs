using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace JsonFromSqlDataBase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacoesController: ControllerBase
    {
        private readonly IConfiguration _config;

        public CotacoesController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Get()
        {
            string jsonQuery=string.Empty;

            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("Dev")))
            {
                string query = "SELECT Sigla,NomeMoeda,UltimaCotacao,ValorComercial," +
                    "ValorTurismo FROM dbo.Cotacoes ORDER BY ValorTurismo FOR JSON AUTO;";

                jsonQuery = conn.QueryFirst<string>(query); 
            }

            if (!string.IsNullOrEmpty(jsonQuery)) return Ok(jsonQuery);

            return NotFound();
        }


    }
}
