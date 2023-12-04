using WordGuessGame.Core.Entities;

namespace WordGuessGame.Core.Interfaces
{
    public interface IWordListRepository
    {
        Task<List<Word>> GetWordListAsync(string difficulty);
        Task<int> AddWordAsync(Word word);
        Task DeleteWordAsync(string word);
        Task DeleteWordByIdAsync(Guid Id);
        Task<Word> GetWordByTextAsync(string word);
        Task<Word> GetWordByIdAsync(Guid id);
    }
}
