using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    // Responsibilities:
    // - Interpolate rotation of enemy towards the current desired
    // direction
    public class EnemyRotationHandler : IFixedTickable
    {
        readonly Settings _settings;
        readonly EnemyModel _model;

        public EnemyRotationHandler(
            EnemyModel model,
            Settings settings)
        {
            _settings = settings;
            _model = model;
        }

        public void FixedTick()
        {
            var lookDir = _model.LookDir;
            var goalDir = _model.DesiredLookDir;

            var error = Vector3.Angle(lookDir, goalDir);

            if (Vector3.Cross(lookDir, goalDir).z < 0)
            {
                error *= -1;
            }

            _model.AddTorque(error * _settings.TurnSpeed);
        }

        [Serializable]
        public class Settings
        {
            public float TurnSpeed;
        }
    }
}
