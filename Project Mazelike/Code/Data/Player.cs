﻿using Microsoft.Xna.Framework;
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
        public Tile currentTile;

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
            return health;
        }
        
        //public void Move(int deltaX, int deltaY) {
        //    if (currentMap.CanEnter(position.X + deltaX, position.Y + deltaY))
        //        position += new Point(deltaX, deltaY);
        //}

        public void Move(Vector2 direction) {
            direction.Normalize();
            Point newPosition = position + direction.ToPoint();
            Tile newTile = currentMap.GetTile(newPosition.X, newPosition.Y);

            if (newTile != null) {
                if (newTile.CanEnter()) {
                    currentTile.LeaveTile(this);
                    position = newPosition;
                    currentTile = newTile;
                    currentTile.EnterTile(this);
                } else if (newTile.EntityInTile.GetType() == typeof(Enemy)) {
                    newTile.EntityInTile.ApplyDamage(60);
                }
            }
        }

        public void SetMap(Map newMap) {
            if (currentMap != null) {
                currentMap.Player = null;
            }
            currentMap = newMap;

            if(currentTile != null) {
                currentTile.LeaveTile(this);
            }

            newMap.Player = this;
            position = newMap.PlayerStart;
            currentTile = newMap.Tiles[position.X, position.Y];
            currentTile.EnterTile(this);
        }
    }
}
