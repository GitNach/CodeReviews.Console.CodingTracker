using CodingTracker.Controller;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View
{
    internal class EditMenu: Menu
    {
        public override void ShowMenu()
        {
            string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("CodingTracker [Cyan]EditMenu[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to choose option)[/]")
            .AddChoices(new[] {
            "INSERT", "UPDATE", "DELETE", "Back to Main Menu"
            }));

            switch (option)
            {
                case "INSERT":
                    Console.WriteLine("Insert method");
                    break;
                case "UPDATE":
                    Console.WriteLine("Update method");
                    break;
                case "DELETE":
                    Console.WriteLine("Delete method");
                    break;
                case "Back to Main Menu":
                    MenuController.SwitchMenu(new MainMenu());
                    break;
            }
        }
    }
}
