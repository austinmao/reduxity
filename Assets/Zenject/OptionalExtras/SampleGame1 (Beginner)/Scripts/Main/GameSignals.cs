using System;

namespace Zenject.Asteroids
{
    public static class Signals
    {
        public class ShipCrashed : Signal<ShipCrashed>
        {
        }
    }
}
