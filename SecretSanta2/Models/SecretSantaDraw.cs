using System.Collections.Generic;

namespace SecretSanta2.Models
{
    public class SecretSantaDraw
    {
        public string Name { get; set; } = string.Empty;
        public List<Participant> Participants { get; set; } = new();
        // Restricciones en formato "giverEmail -> receiverEmail"
        public List<string> Restrictions { get; set; } = new();
    }
}