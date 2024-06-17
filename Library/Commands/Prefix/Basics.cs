using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Library.Commands.Prefix
{
    public class Basics : BaseCommandModule
    {
        //Use the Command attribute to declare a command
        [Command("hello")]

        //Then create an async method for the command. It MUST have the CommandContext as its 1st parameter
        public async Task MyFirstCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello there~~");
        }

        [Command("query")]

        public async Task ApiCall(CommandContext ctx)
        {

            await ctx.Channel.SendMessageAsync("");
        }
        [Command("embed")]
        public async Task EmbedMessageExamples(CommandContext ctx)
        {
            //Using a DiscordMessageBuilder
            var embedMessage1 = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Embed Message Title")
                    .WithDescription("Embed Message Description"));

            //Using a DiscordEmbedBuilder
            var embedMessage2 = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = "Embed Message Title",
                Description = "Embed Message Description"
            };

            await ctx.Channel.SendMessageAsync(embedMessage1);
            await ctx.Channel.SendMessageAsync(embed: embedMessage2);
        }

        [Command("calculator")]
        public async Task CommandParametersExample(CommandContext ctx, int num1, string operation, int num2)
        {
            int result = 0;

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Please enter a valid operation (+, -, *, /)");
                    break;
            }

            await ctx.Channel.SendMessageAsync(result.ToString());
        }


        //[Command("poll")]
        //public async Task BasicPollExample(CommandContext ctx, string option1, string option2, string option3, string option4, [RemainingText] string pollTitle)
        //{
        //    DiscordEmoji[] emojiOptions = [ DiscordEmoji.FromName(Program.Client, ":one:"),
        //                                    DiscordEmoji.FromName(Program.Client, ":two:"),
        //                                    DiscordEmoji.FromName(Program.Client, ":three:"),
        //                                    DiscordEmoji.FromName(Program.Client, ":four:") ];

        //    string optionsDescription = $"{emojiOptions[0]} | **{option1}** \n" +
        //                                $"{emojiOptions[1]} | **{option2}** \n" +
        //                                $"{emojiOptions[2]} | **{option3}** \n" +
        //                                $"{emojiOptions[3]} | **{option4}**";

        //    var pollMessage = new DiscordEmbedBuilder
        //    {
        //        Color = DiscordColor.Azure,
        //        Title = pollTitle,
        //        Description = optionsDescription
        //    };

        //    var message = await ctx.Channel.SendMessageAsync(embed: pollMessage);
        //    foreach (var emoji in emojiOptions)
        //    {
        //        await message.CreateReactionAsync(emoji);
        //    }
        //}
    }
}
