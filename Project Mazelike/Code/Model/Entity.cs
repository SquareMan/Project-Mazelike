using System;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model
{
    class Entity
    {
        public Tile CurrentTile;

        protected int health = 100;

        public Map CurrentMap
        {
            get { return CurrentTile?.map; }
        }

        public Point position
        {
            get { return CurrentTile.position; }
        }

        public event Action OnDeath;

        public virtual void ApplyDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            CurrentTile.LeaveTile(this);
            OnDeath?.Invoke();
        }

        public virtual int GetHealth()
        {
            return health;
        }

        public virtual void Move(Vector2 direction)
        {
            direction.Normalize();
            Point newPosition = position + direction.ToPoint();
            Tile newTile = CurrentMap.GetTile(newPosition.X, newPosition.Y);

            if (newTile != null)
            {
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

        public Entity(Tile tile)
        {
            CurrentTile = tile;
        }
    }
}