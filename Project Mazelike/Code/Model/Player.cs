namespace ProjectMazelike.Model
{
    class Player : Entity
    {
        public void SetMap(Map newMap)
        {
            if (CurrentMap != null)
            {
                CurrentMap.Player = null;
            }

            if (CurrentTile != null)
            {
                CurrentTile.LeaveTile(this);
            }

            newMap.Player = this;
            CurrentTile = newMap.GetTile(newMap.PlayerStart.X, newMap.PlayerStart.Y);
            CurrentTile.EnterTile(this);
        }

        public Player(Tile tile) : base(tile)
        {
        }
    }
}