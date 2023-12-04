using Microsoft.EntityFrameworkCore;
using WordGuessGame.Core.Entities;
using WordGuessGame.Core.Interfaces;
using WordGuessGame.Infrastructure.Contexts;

namespace WordGuessGame.Core.Repositories
{
    public class WordListRepository : IWordListRepository
    {
        private readonly WordDbContext _context;

        public WordListRepository(WordDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Word> GetWordByIdAsync(Guid id)
        {
            try
            {
                var wordObj = await _context.Words.FindAsync(id);
                if (wordObj != null)
                {
                    return wordObj;
                }
                else
                {
                    throw new Exception("Word Not Found!");
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<Word> GetWordByTextAsync(string word)
        {
            return await _context.Words.Where(w => w.Text.ToLower() == word.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<Word>> GetWordListAsync(string difficulty)
        {
            if (string.IsNullOrEmpty(difficulty) || difficulty.ToLower() == "all")
            {
                return await _context.Words.ToListAsync();
            }
            return await _context.Words.Where(w => w.Difficulty.ToLower() == difficulty.ToLower()).ToListAsync();
        }

        public async Task<int> AddWordAsync(Word word)
        {
            await _context.Words.AddAsync(word);
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteWordAsync(string word)
        {
            var wordToRemove = await _context.Words.Where(w => w.Text.ToLower() == word.ToLower()).FirstOrDefaultAsync();
            if (wordToRemove != null)
            {
                _context.Words.Remove(wordToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteWordByIdAsync(Guid Id)
        {
            var wordToRemove = await _context.Words.FindAsync(Id);
            if (wordToRemove != null)
            {
                _context.Words.Remove(wordToRemove);
            }
        }
    }
}
