using CodingTracker.Model;
using CodingTracker.View;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
    public static class SessionController
    {
        private static Stopwatch stopwatch;
        private static DateTime startTime;
        private static bool isRunning;

        public static async Task StartSession()
        {
            
            if (stopwatch == null)
            {
                stopwatch = new Stopwatch();
            }

            stopwatch.Start(); 
            startTime = DateTime.Now; 
            AnsiConsole.Markup("Session [green]started![/]");
            isRunning = true; 

            while (isRunning)
            {
                Console.Clear();
                //Console.WriteLine($"Tiempo transcurrido: {stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}");
                AnsiConsole.Write(
                    new FigletText($"Time elapsed")
                        .LeftJustified()
                        .Color(Color.Yellow));

                AnsiConsole.Write(
                    new FigletText($"{stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}")
                        .LeftJustified()
                        .Color(Color.Red));


                AnsiConsole.Markup("[red]Pause[/] (Enter)");
                AnsiConsole.Markup("\n[underline red]Stop[/] (Spacebar)");


                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key; 

                    
                    if (key == ConsoleKey.Enter)
                    {
                        PauseSession();
                        AnsiConsole.Markup("\nSession [red]paused[/]! Press (Enter) to resume or (Space) to stop.");
                        
                        while (!isRunning) 
                        {
                            if (Console.KeyAvailable)
                            {
                                var pauseKey = Console.ReadKey(true).Key;
                                if (pauseKey == ConsoleKey.Enter)
                                {
                                    ResumeSession();
                                    break; 
                                }
                                else if (pauseKey == ConsoleKey.Spacebar)
                                {
                                    StopSession();
                                    break; 
                                }
                            }
                        }
                    }
                    
                    else if (key == ConsoleKey.Spacebar)
                    {
                        StopSession();
                        break; 
                    }
                }

                await Task.Delay(1000); 
            }
        }

        public static void ResumeSession()
        {
            if (!isRunning)
            {
                stopwatch.Start(); 
                isRunning = true; 
            }
        }

        public static void PauseSession()
        {
            if (stopwatch != null && stopwatch.IsRunning)
            {
                stopwatch.Stop();
                isRunning = false;
            }
        }

        public static void StopSession()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                isRunning = false;
            }
            DateTime endTime = DateTime.Now;

            string formattedStartTime = startTime.ToString("M/d/yyyy h:mm:ss tt");
            string formattedEndTime = endTime.ToString("M/d/yyyy h:mm:ss tt");


            CodingSession session = new CodingSession(startTime, endTime);
            DatabaseController.InsertSession(session);


            isRunning = false;
            Console.WriteLine($"\nSession stopped at {stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}");
            stopwatch.Reset();
            AnsiConsole.Markup("\nPress [green]Enter[/] to go back to the main menu...");
            Console.ReadLine();
            Console.Clear();
            MenuController.SwitchMenu(new MainMenu());
            
        }
    }
}

