using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectMazelike {
    class Enemy : IEntity {
        public Enemy(Tile currentTile) {
            this.currentTile = currentTile;
        }

        public event EntityDiedDelegate OnDeath;

        public Tile currentTile;

        protected int health = 100;

        public void ApplyDamage(int damage) {
            health -= damage;
            if(health <= 0) {
                Die();
            }
        }

        public void Die() {
            OnDeath?.Invoke();
        }

        public int GetHealth() {
            return health;
        }

        public void Move(Vector2 direction) {
            throw new NotImplementedException();
        }
    }
}
