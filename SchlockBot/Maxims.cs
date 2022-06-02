using System;

using Discord.Interactions;
using Newtonsoft.Json;

namespace SchlockBot
{
    public class Maxims : InteractionModuleBase<SocketInteractionContext>
    {
        private static readonly string[] _maxims;
        private static readonly Random _rng = new();

        static Maxims ()
        {
            var json = File.ReadAllText(Path.Combine("Data", "maxims.json"));
            _maxims = JsonConvert.DeserializeObject<string[]>(json)!;
        }

        [SlashCommand("maxim", "Quote a random maxim from the Seventy Maxims")]
        public async Task Maxim()
        {
            var result = _maxims[_rng.Next(0, _maxims.Length)];
            await RespondAsync(result);
        }

        [SlashCommand("maxim", "Quote a maxim from the Seventy Maxims")]
        public async Task Maxim(int index)
        {
            if (index > 0 && index <= _maxims.Length) {
                await RespondAsync(_maxims[index - 1]);
            } else {
                await RespondAsync($"There are 70 maxims, not {index}. :ennesby:");
            }
        }

        private const string LINK_URL = "https://schlockmercenary.fandom.com/wiki/The_Seventy_Maxims_of_Maximally_Effective_Mercenaries";

        [SlashCommand("maxims", "Link me to the Seventy Maxims")]
        public async Task Link()
        {
            await RespondAsync(LINK_URL, ephemeral: true);
        }
    }
}
