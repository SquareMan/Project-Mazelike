﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class ScreenManager {
        public Dictionary<String, Screen> Screens {get; protected set;}

        public Screen ActiveScreen { get; protected set; }

        public ScreenManager() {
            Screens = new Dictionary<string, Screen>();
        }

        public void SetActiveScreen(String name) {
            foreach (string n in Screens.Keys) {
                Screens[n].Visible = false;
                Screens[n].Enabled = false;
            }

            if (Screens.Keys.Contains(name)) {
                ActiveScreen = Screens[name];
                ActiveScreen.Visible = true;
                ActiveScreen.Enabled = true;
            } else {
                Debug.WriteLine(String.Format("Screen with name {0} was attempted to be set as active but does not exist", name));
            }
        }

        public Screen AddScreen(String name, Boolean moveable = true, Boolean rotatable = true, Boolean scaleable = true) {
            Screen newScreen = new Screen(GameManager.Game, moveable, rotatable, scaleable);
            newScreen.Visible = false;
            newScreen.Enabled = false;
            Screens.Add(name, newScreen);
            GameManager.Game.Components.Add(newScreen);
            return newScreen;
        }

        public Screen GetScreen(String name) {
            if (Screens.Keys.Contains(name)) {
                return Screens[name];
            }

            Debug.WriteLine(String.Format("Screen with name {0} was attempted to be retrieved but does not exist", name));
            return null;
        }
    }
}
