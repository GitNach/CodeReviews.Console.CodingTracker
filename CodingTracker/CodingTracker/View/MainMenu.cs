using CodingTracker.Controller;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View
{
    internal class MainMenu: Menu
    {

        public MainMenu() 
        {
            
        }


        public override void showMenu()
        {
            string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("CodingTracker [cyan]MainMenu[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to choose option)[/]")
            .AddChoices(new[] {
            "Start session", "Edit sessions", "See sessions", "EXIT"
            }));

            switch (option)
            {
                case "Start session":
                    MenuController.SwitchMenu(new StartSessionMenu());
                    break;
                case "Edit sessions":
                    MenuController.SwitchMenu(new EditMenu());
                    break;
                case "See sessions":
                    MenuController.SwitchMenu(new SeeSessionsMenu());
                    break;
                case "EXIT":
                    AnsiConsole.Markup("[bold red]Exiting...[/]");
                    break;
            }
        }
    }
}
