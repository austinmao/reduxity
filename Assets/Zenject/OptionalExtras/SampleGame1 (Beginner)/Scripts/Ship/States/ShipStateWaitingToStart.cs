using System;
using UnityEngine;
using System.Collections;
using Zenject;

namespace Zenject.Asteroids
{
    public class ShipStateWaitingToStart : ShipState
    {
        readonly Settings _settings;
        readonly Ship _ship;

        float _elapsedTime;

        public ShipStateWaitingToStart(Settings settings, Ship ship)
        {
            _ship = ship;
            _settings = settings;
        }

        public override void Start()
        {
            _ship.Position = Vector3.zero;
            _ship.Rotation = Quaternion.AngleAxis(90.0f, Vector3.up) * Quaternion.AngleAxis(90.0f, Vector3.right);
        }

        public override void Stop()
        {
            _ship.MeshRenderer.material.color = Color.white;
        }

        public override void Update()
        {
            _elapsedTime += Time.deltaTime;

            var timeForOneCycle = 1.0f / _settings.blinkRate;
            var theta = 2.0f * Mathf.PI * _elapsedTime / timeForOneCycle;

            var px = (Mathf.Cos(theta) + 1.0f) / 2.0f;

            _ship.MeshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, px);
        }

        [Serializable]
        public class Settings
        {
            public float blinkRate;
        }

        public class Factory : Factory<ShipStateWaitingToStart>
        {
        }
    }
}
