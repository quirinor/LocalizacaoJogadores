using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using romans.Data.Collections;
using romans.Models;

namespace romans.Controllers
{
    public class JogadoresController
    {
    
    [ApiController]
    [Route("[controller]")]
    public class jogadoresController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Jogadores> _jogadoresCollection;

    public jogadoresController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _jogadoresCollection = _mongoDB.DB.GetCollection<Jogadores>(typeof(Jogadores).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarJogadores([FromBody] JogadoresDto dto)
        {
            var Jogador = new Jogadores(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _jogadoresCollection.InsertOne(Jogador);
            
            return StatusCode(201, "jogadores adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterJogador()
        {
            var Jogador = _jogadoresCollection.Find(Builders<Jogadores>.Filter.Empty).ToList();
            
            return Ok(Jogador);
        }
    }
    
    
    
    }


}