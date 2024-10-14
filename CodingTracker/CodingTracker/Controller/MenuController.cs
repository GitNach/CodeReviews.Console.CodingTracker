using CodingTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
    public static class MenuController
    {
        public static void SwitchMenu(Menu menu)
        {
            menu.ShowMenu();
        }
    }
}
