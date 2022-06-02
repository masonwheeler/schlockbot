using Discord;
using Discord.WebSocket;

namespace SchlockBot
{
	public class Program
	{
		private readonly DiscordSocketClient _client = new();

		public static Task Main() => new Program().MainAsync();

		public async Task MainAsync()
		{
			_client.Log += Log;
			await _client.LoginAsync(TokenType.Bot,
				Environment.GetEnvironmentVariable("DiscordToken"));
			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}