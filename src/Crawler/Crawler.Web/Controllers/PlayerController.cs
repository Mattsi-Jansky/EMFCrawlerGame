using System;
using Crawler.Models;
using Crawler.Web.GameContainers;
using Crawler.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Web.Controllers
{
    public class PlayerController : Controller
    {
        [HttpGet]
        [Route("[controller]/CanAddCharacter")]
        public bool CanAddCharacter()
        {
            return GameContainer.Instance.CanAddCharacter();
        }

        [HttpGet]
        [Route("[controller]/status/{id}")]
        public IActionResult Status(Guid id)
        {
            if (!GameContainer.Instance.ValidateClientId(id)) return BadRequest("No such client");
            return Ok(GameContainer.Instance.GetStatus(id));
        }
        
        [HttpPost]
        [Route("[controller]/New")]
        public IActionResult AddCharacter([FromBody]NewCharacterRequest request)
        {
            if (request == null) return BadRequest();
            
            if (GameContainer.Instance.CanAddCharacter())
            {
                var id = GameContainer.Instance.AddCharater(request);
                return Created("Created character", id);
            }
            else
            {
                return BadRequest("No spaces available.");
            }
        }
        
        [HttpPost]
        [Route("[controller]/Move")]
        public IActionResult Move([FromBody]MoveRequestModel request)
        {
            if (!GameContainer.Instance.ValidateClientId(request.Id)) return BadRequest("No such client");
            
            var command = GameContainer.Instance.GetCommandFactory(request.Id).Move(request.Direction);
            GameContainer.Instance.AddCommand(request.Id, command);
            
            return Ok();
        }
    }
}