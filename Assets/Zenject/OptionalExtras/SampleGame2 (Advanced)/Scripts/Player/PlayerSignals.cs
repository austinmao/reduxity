using System;

namespace Zenject.SpaceFighter
{
    public static class PlayerSignals
    {
        // Fired when a bullet hits the player
        public class Hit : Signal<Hit, Bullet>
        {
        }
    }
}

