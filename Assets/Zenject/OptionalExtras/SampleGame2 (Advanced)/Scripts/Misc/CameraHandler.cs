using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    // Have the camera follow the player around
    public class CameraHandler : ITickable
    {
        readonly Settings _settings;
        readonly Camera _camera;
        readonly PlayerFacade _player;

        public CameraHandler(
            PlayerFacade player,
            Camera camera,
            Settings settings)
        {
            _settings = settings;
            _camera = camera;
            _player = player;
        }

        public void Tick()
        {
            var currentPos = _camera.transform.position;

            var desiredPos = new Vector3(
                _player.Position.x, _player.Position.y, currentPos.z);

            _camera.transform.position = Vector3.Lerp(currentPos, desiredPos, _settings.FollowSpeed);
        }

        [Serializable]
        public class Settings
        {
            public float FollowSpeed;
        }
    }
}
