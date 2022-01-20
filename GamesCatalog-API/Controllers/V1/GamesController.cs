using GamesCatalog_API.Exceptions;
using GamesCatalog_API.InputModel;
using GamesCatalog_API.Services;
using GamesCatalog_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GamesCatalog_API.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Search all games by pagination
        /// </summary>
        /// <remarks>
        /// It's not possible to return games without pagination.
        /// </remarks>
        /// <param name="page">Indicates which page is been required. Minumum 1</param>
        /// <param name="quantity">Indicates the quantity of registers by page. Minumum 1 e maximum 50</param>
        /// <response code="200">Returns a game list</response>
        /// <response code="204">Case there are not games</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Count == 0)
            {
                return NoContent();
            }
            return Ok(games);
        }


        /// <summary>
        /// Search a game by Id
        /// </summary>
        /// <param name="gameId">game id to search</param>
        /// <response code="200">Return a filtered game</response>
        /// <response code="204">Case there is no game with this id</response>   
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
        {
            var game = await _gameService.Get(gameId);
            if (game == null)
            {
                return NoContent();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);

            }
            //catch (GameAlreadyRegisteredException ex)
            catch (Exception ex)
            {
                return UnprocessableEntity("Game already registered with this name for this producer.");
            }
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(gameId, gameInputModel);

                return Ok();
            }
            //catch (GameNotRegisteredException ex)
            catch (Exception ex)
            {
                return NotFound("This game is not registered.");
            }
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameId, price);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game is not registered.");
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.Remove(gameId);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game is not registered.");

            }
        }

    }
}
