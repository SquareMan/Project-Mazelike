using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectMazelike {
    class Entity : IEntity {
        public event EntityDiedDelegate OnDeath;

        public void ApplyDamage(int damage) {
            throw new NotImplementedException();
        }

        public void Die() {
            throw new NotImplementedException();
        }

        public int GetHealth() {
            throw new NotImplementedException();
        }

        public void Move(Vector2 direction) {
            throw new NotImplementedException();
        }
    }
}
