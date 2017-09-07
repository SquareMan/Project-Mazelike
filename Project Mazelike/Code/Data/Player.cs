using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike {
    class Player : IEntity {
        public Player(Point position) {
            this.position = position;
        }

        public Map currentMap;
        public Point position;

        //This will likely become more complicated when factoring in leveling up, weapons, etc.
        int health = 100;

        public event EntityDiedDelegate OnDeath;

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
            throw new NotImplementedException();
        }
        
        public void Move(int deltaX, int deltaY) {
            if (currentMap.CanEnter(position.X + deltaX, position.Y + deltaY))
                position += new Point(deltaX, deltaY);
        }

        public void Move(Vector2 direction) {
            throw new NotImplementedException();
        }

        public void SetMap(Map newMap) {
            currentMap = newMap;
            position = newMap.PlayerStart;
        }
    }
}
