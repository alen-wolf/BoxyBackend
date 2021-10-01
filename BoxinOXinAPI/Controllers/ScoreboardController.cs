using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BoxinOXinAPI.Models;

namespace BoxinOXinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ScoreboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select id, PlayerName, Wins from dbo.Scoreboard ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BoxinOXinAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Scoreboard score)
        {
            string query = @"
                    insert into dbo.Scoreboard 
                    (PlayerName,Wins)                    
                    values
                    (
                    '"+score.PlayerName+@"'
                    ,'"+score.Wins+@"'
                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BoxinOXinAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Addition Successfull");

        } 

        [HttpPut("{PlayerName}")]
        public JsonResult Put(string PlayerName)
        {
            string query = @"
                    update dbo.Scoreboard 
                    set Wins=Wins+1
                    where PlayerName = '"+ PlayerName +@"'
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BoxinOXinAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); 

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Successfull");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Scoreboard 
                    where id = '" + id + @"'
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BoxinOXinAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("id "+ id + " Deletion Successful");
        }



    }
}
