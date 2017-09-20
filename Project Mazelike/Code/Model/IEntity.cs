using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMazelike.Model {
    delegate void EntityDiedDelegate();
    
    interface IEntity {
        event EntityDiedDelegate OnDeath;

        void Move(Vector2 direction);
        void ApplyDamage(int damage);
        void Die();
        int GetHealth();
    }
}
