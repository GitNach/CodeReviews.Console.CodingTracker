using CodingTracker.Controller;
using CodingTracker.Model;
using Spectre.Console;

namespace CodingTracker.View
{
    internal class EditMenu : Menu
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
                    AnsiConsole.Markup("[yellow]Updating session\n[/]");
                    int sessionId = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter the [yellow]ID[/] of the coding session to modify:")
                            .Validate(sessionId =>
                            {
                                if (!DatabaseController.DoesSessionExist(sessionId))
                                {
                                    return ValidationResult.Error("[red]Error:[/] The session with the given ID does not exist.");
                                }
                                return ValidationResult.Success();
                            })
                    );

                    var updateDate = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter the new [yellow]starting date[/] (yyyy-MM-dd):")
                            .Validate(updateDate =>
                            {
                                return DateTime.TryParseExact(updateDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _)
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]Invalid date format![/]");
                            }));

                    var updateStartTime = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter the new [yellow]starting time[/] (HH:mm):")
                            .Validate(time =>
                            {
                                return DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid time format![/]");
                            }));

                    DateTime updateStartDate = DateTime.Parse($"{updateDate} {updateStartTime}");

                    var updateEndTime = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter the new [yellow]ending time[/] (HH:mm):")
                            .Validate(time =>
                            {
                                return DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid time format![/]");
                            })
                            .Validate(time =>
                            {
                                var startDateTime = DateTime.ParseExact($"{updateDate} {updateStartTime}", "yyyy-MM-dd HH:mm", null);
                                var endDateTime = DateTime.ParseExact($"{updateDate} {time}", "yyyy-MM-dd HH:mm", null);
                                return endDateTime > startDateTime
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]Ending time cannot be before starting time![/]");
                            }));

                    DateTime updateEndDate = DateTime.Parse($"{updateDate} {updateEndTime}");

                    var updatedSession = new CodingSession(sessionId, updateStartDate, updateEndDate);

                    DatabaseController.UpdateSession(updatedSession);

                    AnsiConsole.Markup("[green]Session was successfully updated!\n[/]");
                    AnsiConsole.Markup("Press [green]Enter[/] to go back to the main menu...");
                    Console.ReadLine();
                    Console.Clear();
                    MenuController.SwitchMenu(new MainMenu());

                    break;
                case "DELETE":
                    AnsiConsole.Markup("[red]Deleting session\n[/]");
                    int deleteId = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter the [yellow]ID[/] of the coding session to [red]delete[/]:")
                            .Validate(deleteId =>
                            {
                                if (!DatabaseController.DoesSessionExist(deleteId))
                                {
                                    return ValidationResult.Error("[red]Error:[/] The session with the given ID does not exist.");
                                }
                                return ValidationResult.Success();
                            })
                    );
                    DatabaseController.DeleteSession(deleteId);

                    AnsiConsole.Markup("[red]Session was successfully deleted!\n[/]");
                    AnsiConsole.Markup("Press [green]Enter[/] to go back to the main menu...");
                    Console.ReadLine();
                    Console.Clear();
                    MenuController.SwitchMenu(new MainMenu());

                    break;
                case "Back to Main Menu":
                    MenuController.SwitchMenu(new MainMenu());
                    break;
            }
        }
    }
}
