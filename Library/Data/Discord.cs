using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using System;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using System.Linq;

namespace Library.Data
{
    internal class Discord
    {
        //public readonly DiscordClient Client;
        public static readonly DiscordClient Client;
        public readonly CommandsNextExtension Commands;

        public Discord( CommandsNextExtension commands)
        {//DiscordClient client,
            //Client = client;
            Commands = commands;

            //Set defaults for interactivity based events
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });
           
        }

        internal static async Task Client_ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            switch (args.Interaction.Data.CustomId)
            {
                case "button1":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 1"));
                    break;
                case "button2":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 2"));
                    break;
                case "basicsButton":
                    var basicCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "- !test -> Send a basic message \n" +
                                      "- !embed -> Sends a basic embed message \n" +
                                      "- !calculator -> Performs an operation on 2 numbers \n" +
                                      "- !cardgame -> Play a simple card game. Highest number wins the game"
                    };

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(basicCommandsEmbed));
                    break;
                case "calculatorButton":
                    var calculatorCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "- /calculator add -> Adds 2 numbers together \n" +
                                      "- /calculator subtract -> Subtracts 2 numbers \n" +
                                      "- /calculator multiply -> Multiplies 2 numbers together"
                    };

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(calculatorCommandsEmbed));
                    break;
                case "modalButton":
                    var modal = new DiscordInteractionResponseBuilder()
                        .WithTitle("Test Modal")
                        .WithCustomId("testModal")
                        .AddComponents(new TextInputComponent("Random", "randomTextBox", "Type something here"));

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
                    break;
            }

            //Drop-Down Events (You can change this to a SWITCH block!!!!)
            if (args.Id == "dropDownList" && args.Interaction.Data.ComponentType == ComponentType.StringSelect)
            {
                var options = args.Values;
                foreach (var option in options)
                {
                    switch (option)
                    {
                        case "option1":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 1"));
                            break;

                        case "option2":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 2"));
                            break;

                        case "option3":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 3"));
                            break;
                    }
                }
            }
            else if (args.Id == "channelDropDownList")
            {
                var options = args.Values;
                foreach (var channel in options)
                {
                    var selectedChannel = await Client.GetChannelAsync(ulong.Parse(channel));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} selected the channel with name {selectedChannel.Name}"));
                }
            }

            else if (args.Id == "mentionDropDownList")
            {
                var options = args.Values;
                foreach (var user in options)
                {
                    var selectedUser = await Client.GetUserAsync(ulong.Parse(user));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{selectedUser.Mention} was mentionned"));
                }
            }
        }

        internal static async Task Client_GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            var defaultChannel = args.Guild.GetDefaultChannel();

            var welcomeEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                Title = $"Welcome {args.Member.Username} to the server",
                Description = "Hope you enjoy your stay, please read the rules"
            };

            await defaultChannel.SendMessageAsync(embed: welcomeEmbed);
        }

        internal static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
        internal static async Task Client_ModalSubmitted(DiscordClient sender, ModalSubmitEventArgs args)
        {
            if (args.Interaction.Type == InteractionType.ModalSubmit && args.Interaction.Data.CustomId == "testModal")
            {
                var values = args.Values;
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.Interaction.User.Username} submitted a modal with the input {values.Values.First()}"));
            }
        }
    }
}
