using CodingTracker.Controller;
using CodingTracker.Model;
using Spectre.Console;

namespace CodingTracker.View
{
    internal class SeeSessionsMenu : Menu
    {
        public override void ShowMenu()
        {
            AnsiConsole.Markup("[Cyan]SeeSessionsMenu\n[/]");

            List<CodingSession> sessions = DatabaseController.GetSessions();

            double totalTime = 0;

            // Create a table
            var table = new Table();


            //rows
            table.AddColumn("[bold underline orange3]Id[/]");
            table.AddColumn("[bold underline orange3]StartTime[/]");
            table.AddColumn("[bold underline orange3]EndTime[/]");
            table.AddColumn("[bold underline springgreen1]Duration[/]");

            //columns
            foreach (CodingSession session in sessions)
            {
                table.AddRow($"{session.Id}", $"{session.StartTime}", $"{session.EndTime}", $"{session.Duration.ToString(@"hh\:mm\:ss")}");
                totalTime += session.Duration.TotalHours;
            }


            // Render the table to the console
            AnsiConsole.Write(table);

            AnsiConsole.Write(new BarChart()
                .Width(60)
                .Label("[green bold underline]Total coding hours[/]")
                .CenterLabel()
                .AddItem("Hours", Math.Round(totalTime, 1), Color.Red));

            AnsiConsole.Markup("Press [green]Enter[/] to go back to the main menu...");
            Console.ReadLine();
            Console.Clear();
            MenuController.SwitchMenu(new MainMenu());

        }


    }
}
