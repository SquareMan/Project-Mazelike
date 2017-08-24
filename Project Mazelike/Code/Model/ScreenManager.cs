using System;
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
                Screens[n].Enabled = false;
            }

            if (Screens.Keys.Contains(name)) {
                ActiveScreen = Screens[name];
                ActiveScreen.Enabled = true;
            }

            Debug.WriteLine(String.Format("Screen with name {0} was attempted to be set as active but does not exist", name));
        }

        public void AddScreen(String name) {
            Screen newScreen = new Screen(GameManager.Game);
            newScreen.Enabled = false;
            Screens.Add(name, newScreen);
            GameManager.Game.Components.Add(newScreen);
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
