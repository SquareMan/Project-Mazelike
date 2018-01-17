namespace ProjectMazelike.Model {
    class Player : Entity {
        public Player(Tile tile) : base(tile) {
        }

        public void SetMap(Map newMap) {
            if (currentMap != null) {
                currentMap.Player = null;
            }

            if(currentTile != null) {
                currentTile.LeaveTile(this);
            }

            newMap.Player = this;
            currentTile = newMap.GetTile(newMap.PlayerStart.X, newMap.PlayerStart.Y);
            currentTile.EnterTile(this);
        }
    }
}
