using System;
using UnityEngine;
using System.Collections;
using Zenject;

namespace Zenject.Asteroids
{
    public enum ShipStates
    {
        Moving,
        Dead,
        WaitingToStart,
        Count,
    }

    public abstract class ShipState
    {
        public abstract void Update();

        public virtual void Start()
        {
            // optionally overridden
        }

        public virtual void Stop()
        {
            // optionally overridden
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            // optionally overridden
        }
    }
}
