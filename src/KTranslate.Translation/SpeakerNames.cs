using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KTranslate.Translation
{
    // Session-wide on purpose: names learned in one capture are reused as a glossary for later lines that only mention them.
    // Not persisted, so the list starts empty on every launch and cannot leak names from one game into another.
    public static class SpeakerNames
    {
        // Optional leading token is the speaker's colored bar, which OCR reads as "|", "1", "l" or "I".
        // Titles like "Head of Security" are allowed: up to four capitalized words joined by lowercase connectors.
        private static readonly Regex SpeakerLineRegex = new(@"^(?:[|1lI!]\s+)?(\p{Lu}[\p{L}'.-]*(?:(?:\s(?:of|the|and|de|del|la))*\s\p{Lu}[\p{L}'.-]*){0,3})$");
        private const int MAX_NAME_LENGTH = 30;
        // Keeps two-line menus such as "Yes / No" from being taken as a speaker plus a line of dialogue.
        private const int MIN_DIALOGUE_LENGTH = 10;

        private static readonly ConcurrentDictionary<string, byte> Names = new(StringComparer.OrdinalIgnoreCase);

        public static IReadOnlyCollection<string> All => Names.Keys.ToArray();

        public static void Register(string name) => Names.TryAdd(name, 0);

        // Removes the speaker label that sits above the dialogue, or a lone known name whose dialogue OCR missed (e.g. "...").
        public static string[] StripSpeakerLine(string[] lines, out string speaker)
        {
            speaker = null;
            if (lines.Length == 0)
            {
                return lines;
            }

            var match = SpeakerLineRegex.Match(lines[0].Trim());
            if (!match.Success || match.Groups[1].Length > MAX_NAME_LENGTH)
            {
                return lines;
            }

            var name = match.Groups[1].Value;
            var rest = lines.Skip(1).ToArray();
            var restText = string.Join(' ', rest).Trim();
            var isSpeakerLabel = rest.Length > 0 && (restText.Length >= MIN_DIALOGUE_LENGTH || !restText.Any(char.IsLetter));
            if (!isSpeakerLabel && !(rest.Length == 0 && Names.ContainsKey(name)))
            {
                return lines;
            }

            speaker = name;
            return rest;
        }
    }
}
