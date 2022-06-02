using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace SchlockBot
{
	public class Program
	{
		private readonly DiscordSocketClient _client = new();
        private InteractionService _interactionService;
		private ulong _guildId;

        public static Task Main() => new Program().MainAsync();

		public async Task MainAsync()
		{
			_client.Log += Log;
			_client.Ready += ClientReady;
			_guildId = ulong.Parse(Environment.GetEnvironmentVariable("GuildID")!);
			await _client.LoginAsync(TokenType.Bot,
				Environment.GetEnvironmentVariable("DiscordToken"));
			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

        private async Task ClientReady()
        {
			_interactionService = new InteractionService(_client);
			await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
			await _interactionService.RegisterCommandsToGuildAsync(_guildId);

			_client.InteractionCreated += async interaction =>
			{
				var ctx = new SocketInteractionContext(_client, interaction);
				await _interactionService.ExecuteCommandAsync(ctx, null);
			};
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}