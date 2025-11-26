using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta2.Models;

namespace SecretSanta2.Services
{
    public class SecretSantaService
    {
        private readonly Random _rnd = new();

        private sealed class ExclusionComparer : IEqualityComparer<(string giver, string receiver)>
        {
            public bool Equals((string giver, string receiver) x, (string giver, string receiver) y) =>
                string.Equals(x.giver, y.giver, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.receiver, y.receiver, StringComparison.OrdinalIgnoreCase);

            public int GetHashCode((string giver, string receiver) obj)
            {
                int h1 = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.giver ?? string.Empty);
                int h2 = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.receiver ?? string.Empty);
                return HashCode.Combine(h1, h2);
            }
        }

        public Dictionary<Participant, Participant>? GenerateAssignments(
            List<Participant> participants,
            List<(string giver, string receiver)> exclusions)
        {
            var exclusionSet = new HashSet<(string giver, string receiver)>(exclusions.Select(e => (e.giver.Trim(), e.receiver.Trim())), new ExclusionComparer());

            bool IsExcluded(string giverEmail, string receiverEmail) => exclusionSet.Contains((giverEmail, receiverEmail));

            // Intentos aleatorios rápidos
            for (int attempt = 0; attempt < 200; attempt++)
            {
                var shuffled = participants.OrderBy(_ => _rnd.Next()).ToList();
                bool ok = true;
                var map = new Dictionary<Participant, Participant>();
                for (int i = 0; i < participants.Count; i++)
                {
                    var giver = participants[i];
                    var receiver = shuffled[i];
                    if (giver.Email.Equals(receiver.Email, StringComparison.OrdinalIgnoreCase) || IsExcluded(giver.Email, receiver.Email))
                    {
                        ok = false;
                        break;
                    }
                    map[giver] = receiver;
                }
                if (ok) return map;
            }

            // Backtracking
            var receivers = participants.ToList();
            var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var assignment = new Dictionary<Participant, Participant>();

            bool Backtrack(int index)
            {
                if (index == participants.Count) return true;
                var giver = participants[index];
                foreach (var candidate in receivers.OrderBy(_ => _rnd.Next()))
                {
                    if (candidate.Email.Equals(giver.Email, StringComparison.OrdinalIgnoreCase)) continue;
                    if (used.Contains(candidate.Email)) continue;
                    if (IsExcluded(giver.Email, candidate.Email)) continue;

                    assignment[giver] = candidate;
                    used.Add(candidate.Email);
                    if (Backtrack(index + 1)) return true;
                    used.Remove(candidate.Email);
                    assignment.Remove(giver);
                }
                return false;
            }

            if (Backtrack(0)) return assignment;
            return null;
        }
    }
}