using CodingTracker.Controller;
using CodingTracker.View;
using SQLitePCL;

Batteries.Init();
DatabaseController.CreateDatabase();
MenuController.SwitchMenu(new MainMenu());
