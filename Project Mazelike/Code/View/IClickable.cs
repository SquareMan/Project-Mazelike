using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.View {
    interface IClickable {
        event ScreenComponent.ClickedDelegate OnClicked;
    }
}
