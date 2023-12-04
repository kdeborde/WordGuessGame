using Swashbuckle.AspNetCore.Annotations;

namespace WordGuessGame.Core.Entities
{
    public class Word
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Difficulty { get; set; }
    }
}
