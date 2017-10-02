using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectMazelike.Model {
    class Entity {
        public Entity(Tile tile) {
            currentTile = tile;
        }

        public delegate void EntityDiedDelegate();
        public event EntityDiedDelegate OnDeath;

        public Tile currentTile;
        public Map currentMap {
            get {
                return currentTile?.map;
            }
        }
        public Point position {
            get {
                return currentTile.position;
            }
        }

        protected int health = 100;

        public virtual void ApplyDamage(int damage) {
            health -= damage;
            if (health <= 0) {
                Die();
            }
        }

        public virtual void Die() {
            currentTile.LeaveTile(this);
            OnDeath?.Invoke();
        }

        public virtual int GetHealth() {
            return health;
        }

        public virtual void Move(Vector2 direction) {
            direction.Normalize();
            Point newPosition = position + direction.ToPoint();
            Tile newTile = currentMap.GetTile(newPosition.X, newPosition.Y);

            if (newTile != null) {
                if (newTile.CanEnter()) {
                    currentTile.LeaveTile(this);
                    currentTile = newTile;
                    currentTile.EnterTile(this);
                } else if (newTile.EntityInTile != null && newTile.EntityInTile.GetType() == typeof(Enemy)) {
                    newTile.EntityInTile.ApplyDamage(60);
                }
            }
        }
    }
}
