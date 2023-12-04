

using Microsoft.AspNetCore.Mvc;
using System.Net;
using WordGuessGame.Core.Entities;
using WordGuessGame.Core.Interfaces;

namespace WordGuessGame.API.Controllers
{
    public class WordController : BaseApiController
    {
        private readonly IWordListRepository _wordListRepository;
        private readonly ILogger<WordController> _logger;

        public WordController(IWordListRepository wordListRepository, ILogger<WordController> logger)
        {
            _wordListRepository = wordListRepository;
            _logger = logger;
        }

        [HttpGet("GetWord/{difficulty}")]
        public async Task<ActionResult<Word>> GetWord(string difficulty)
        {
            try
            {
                var wordList = await _wordListRepository.GetWordListAsync(difficulty); ;
                var max = wordList.ToList().Count();
                var rand = new Random();
                return wordList.ToList()[rand.Next(0, max + 1)];
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred: {ex}");
                return null;
            }
        }

        [HttpGet("GetWords/{difficulty}")]
        public async Task<List<Word>> GetWords(string difficulty = "all")
        {
            try
            {
                return await _wordListRepository.GetWordListAsync(difficulty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred: {ex}");
                return null;
            }

        }

        [HttpGet("GetSingleWordByText/{word}")]
        public async Task<ActionResult<Word>> GetSingleWordByText(string word)
        {
            try
            {
                var wordRet = await _wordListRepository.GetWordByTextAsync(word);
                if (wordRet == null)
                {
                    return NotFound("No matching word found.");
                }
                return wordRet;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred: {ex}");
                return StatusCode(500, $"An Error Has Occurred. {ex}");
            }
        }

        [HttpPost("AddWord")]
        public async Task<IActionResult> AddWord(Word word)
        {
            try
            {
                var wordExists = await _wordListRepository.GetWordByTextAsync(word.Text);
                if (wordExists == null)
                {
                    if (await _wordListRepository.AddWordAsync(word) > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "An error has occured while processing your request, please try again later.");
                    }
                }
                return Conflict("The given word already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred: {ex}");
                return StatusCode(500, $"An Error Has Occurred. {ex}");
            }
        }

        [HttpDelete("DeleteWord/{word}")]
        public IActionResult DeleteWord(string word)
        {
            _wordListRepository.DeleteWordAsync(word);
            return Ok();
        }
    }
}
