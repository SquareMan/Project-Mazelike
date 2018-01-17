using System;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    internal class Entity
    {
        public Tile CurrentTile;

        protected int Health = 100;

        public Map CurrentMap => CurrentTile?.Map;

        public Point Position => CurrentTile.Position;

        public event Action OnDeath;

        public Entity(Tile tile)
        {
            CurrentTile = tile;
        }

        public virtual void ApplyDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) Die();
        }

        public virtual void Die()
        {
            CurrentTile.LeaveTile(this);
            OnDeath?.Invoke();
        }

        public virtual int GetHealth()
        {
            return Health;
        }

        public virtual void Move(Vector2 direction)
        {
            direction.Normalize();
            var newPosition = Position + direction.ToPoint();
            var newTile = CurrentMap.GetTile(newPosition.X, newPosition.Y);

            if (newTile != null)
                if (newTile.CanEnter())
                {
                    CurrentTile.LeaveTile(this);
                    CurrentTile = newTile;
                    CurrentTile.EnterTile(this);
                }
                else if (newTile.EntityInTile != null && newTile.EntityInTile.GetType() == typeof(Enemy))
                {
                    newTile.EntityInTile.ApplyDamage(60);
                }
        }
    }
}