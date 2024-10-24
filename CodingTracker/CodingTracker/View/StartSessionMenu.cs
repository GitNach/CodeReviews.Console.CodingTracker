using CodingTracker.Controller;
using Spectre.Console;

namespace CodingTracker.View
{
    internal class StartSessionMenu : Menu
    {
        public override void ShowMenu()
        {
            string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("CodingTracker [Cyan]StartSessionMenu[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to choose option)[/]")
            .AddChoices(new[] {
            "START", "Back to Main Menu"
            }));

            switch (option)
            {

                case "START":
                    Console.WriteLine("STARTING Session...");
                    SessionController.StartSession().Wait();
                    break;
                case "Back to Main Menu":
                    MenuController.SwitchMenu(new MainMenu());
                    break;
            }
        }
    }
}
