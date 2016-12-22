using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    // These values are given as parameters into dynamically created
    // EnemyFacade instances
    [Serializable]
    public class EnemyTunables
    {
        public float Accuracy;
        public float Speed;
        public float AttackDistance;
    }

    // These values are global and used to ramp up the difficulty of our game
    // over time
    [Serializable]
    public class EnemyGlobalTunables
    {
        public int NumAttacking;
        public int DesiredNumEnemies;
    }
}
