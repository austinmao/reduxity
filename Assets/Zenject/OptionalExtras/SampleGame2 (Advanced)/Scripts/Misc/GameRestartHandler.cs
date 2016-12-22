using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class GameRestartHandler : IInitializable, IDisposable, ITickable
    {
        readonly Settings _settings;

        PlayerKilledSignal _killedSignal;
        bool _isDelaying;
        float _delayStartTime;

        public GameRestartHandler(
            Settings settings,
            PlayerKilledSignal killedSignal)
        {
            _killedSignal = killedSignal;
            _settings = settings;
        }

        public void Initialize()
        {
            _killedSignal += OnPlayerKilled;
        }

        public void Dispose()
        {
            _killedSignal -= OnPlayerKilled;
        }

        public void Tick()
        {
            if (_isDelaying)
            {
                if (Time.realtimeSinceStartup - _delayStartTime > _settings.RestartDelay)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        void OnPlayerKilled()
        {
            // Wait a bit before restarting the scene
            _delayStartTime = Time.realtimeSinceStartup;
            _isDelaying = true;
        }

        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}
