namespace ProjectMazelike.View
{
    internal interface IClickable
    {
        event ScreenComponent.ClickedDelegate OnClicked;
    }
}