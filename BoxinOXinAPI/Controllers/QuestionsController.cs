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
    public class QuestionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public QuestionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select id, Question, CorrectAnswer, WrongAnswerOne, WrongAnswerTwo from dbo.Questions 
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
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Questions quest)
        {
            string query = @"
                    insert into dbo.Questions 
                    (Question,CorrectAnswer,WrongAnswerOne,WrongAnswerTwo)                    
                    values
                    (
                    '" + quest.Question + @"'
                    ,'" + quest.CorrectAnswer + @"'
                    ,'" + quest.WrongAnswerOne + @"'
                    ,'" + quest.WrongAnswerTwo + @"'
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

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Questions 
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
            return new JsonResult("id " + id + " Deletion Successful");
        }

        [Route("GetAllQuestions")]
        public JsonResult GetAllQuestions()
        {
            string query = @"
                    select id,Question from dbo.Questions 
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
            return new JsonResult(table);
        }

        [Route("GetSpecificQuestion/{id}")]
        public JsonResult GetSpecificQuestion(int id)
        {
            string query = @"
                    select id, Question, CorrectAnswer, WrongAnswerOne, WrongAnswerTwo from dbo.Questions
                    where id ='" + id + @"'
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
            return new JsonResult(table);
        }

    }
}
