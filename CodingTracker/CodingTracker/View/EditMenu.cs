using CodingTracker.Controller;
using CodingTracker.Model;
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
                    
                    AnsiConsole.Markup("[green]Inserting method\n[/]");
                    var date = AnsiConsole.Prompt(
                        new TextPrompt<string>("Starting date [yellow](yyyy-MM-dd)[/]:")
                            .Validate(date =>
                            {
                                return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _)
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]Invalid date format![/]");
                            }));
                    var startTime = AnsiConsole.Prompt(
                        new TextPrompt<string>("Starting time [yellow](HH:mm)[/]:")
                            .Validate(time =>
                            {
                                return DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid time format![/]");
                            }));

                    DateTime startDate = DateTime.Parse($"{date} {startTime}");

           
                    var endTime = AnsiConsole.Prompt(
                        new TextPrompt<string>("Ending time [yellow](HH:mm)[/]:")
                            .Validate(time =>
                            {
                                return DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid time format![/]");
                            }));

                    DateTime endDate = DateTime.Parse($"{date} {endTime}");

                    var newSession = new CodingSession(startDate, endDate);

                    DatabaseController.InsertSession(newSession);
                
                    AnsiConsole.Markup("[green]Session was succesfully added!\n[/]");

                    AnsiConsole.Markup("Press [green]Enter[/] to go back to the main menu...");
                    Console.ReadLine();
                    Console.Clear();
                    MenuController.SwitchMenu(new MainMenu());

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
