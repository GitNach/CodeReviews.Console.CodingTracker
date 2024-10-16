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
    internal class SeeSessionsMenu : Menu
    {
        public override void ShowMenu()
        {
            AnsiConsole.Markup("[Cyan]SeeSessionsMenu\n[/]");

            List<CodingSession> sessions = DatabaseController.GetSessions();

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
                table.AddRow($"{session.Id}", $"{session.StartTime}", $"{session.EndTime}", $"{session.Duration}");
                
            }
            
            
            // Render the table to the console
            AnsiConsole.Write(table);

            AnsiConsole.Markup("Press [green]Enter[/] to go back to the main menu...");
            Console.ReadLine();
            Console.Clear();
            MenuController.SwitchMenu(new MainMenu());

        }
    }
}
