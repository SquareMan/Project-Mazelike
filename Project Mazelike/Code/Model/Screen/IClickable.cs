﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    interface IClickable {
        event ScreenComponent.ClickedDelegate OnClicked;
    }
}
