using System.Text;
using WordGuessGame.Core.Entities;
using WordGuessGame.Infrastructure.Contexts;

namespace WordGuessGame.Infrastructure.Seed
{
    public class Seed
    {
        public static async Task SeedData(WordDbContext context)
        {
            if (context.Words.Any()) return;

            var wordList = new List<Word>();
            var count = 0;

            using (var fileStream = File.OpenRead("../WordGuessGame.Infrastructure/Seed/EasyWordList.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string word;

                    while ((word = streamReader.ReadLine()) != null)
                    {
                        wordList.Add(new Word
                        {
                            Text = word,
                            Difficulty = "Easy"
                        });
                        count++;
                    }
                }
                Console.WriteLine(count);
            }
            using (var fileStream = File.OpenRead("../WordGuessGame.Infrastructure/Seed/MediumWordList.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string word;
                    while ((word = streamReader.ReadLine()) != null)
                    {
                        wordList.Add(new Word
                        {
                            Text = word,
                            Difficulty = "Medium"
                        });
                    }

                }
            }
            using (var fileStream = File.OpenRead("../WordGuessGame.Infrastructure/Seed/HardWordList.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string word;
                    while ((word = streamReader.ReadLine()) != null)
                    {
                        wordList.Add(new Word
                        {
                            Text = word,
                            Difficulty = "Hard"
                        });
                    }

                }
            }
            using (var fileStream = File.OpenRead("../WordGuessGame.Infrastructure/Seed/ReallyHardWordList.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string word;
                    while ((word = streamReader.ReadLine()) != null)
                    {
                        wordList.Add(new Word
                        {
                            Text = word,
                            Difficulty = "ReallyHard"
                        });
                    }

                }
            }
            using (var fileStream = File.OpenRead("../WordGuessGame.Infrastructure/Seed/MoviesWordList.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string word;
                    while ((word = streamReader.ReadLine()) != null)
                    {
                        wordList.Add(new Word
                        {
                            Text = word,
                            Difficulty = "Movies"
                        });
                    }

                }
            }
            await context.Words.AddRangeAsync(wordList);
            await context.SaveChangesAsync();
        }
    }
}
