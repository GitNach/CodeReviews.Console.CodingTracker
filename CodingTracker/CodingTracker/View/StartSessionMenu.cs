using CodingTracker.Controller;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View
{
    internal class StartSessionMenu: Menu
    {
        public override void ShowMenu()
        {
            string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("CodingTracker [Cyan]StartSessionMenu[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to choose option)[/]")
            .AddChoices(new[] {
            "START", "PAUSE", "STOP", "Back to Main Menu"
            }));

            switch (option)
            {
                
                case "START":
                    Console.WriteLine("STARTING Session...");
                    break;
                case "PAUSE":
                    Console.WriteLine("PAUSING Session...");
                    break;
                case "STOP":
                    Console.WriteLine("STOPPING Session...");
                    break;
                case "Back to Main Menu":
                    MenuController.SwitchMenu(new MainMenu());
                    break;
            }
        }
    }
}
