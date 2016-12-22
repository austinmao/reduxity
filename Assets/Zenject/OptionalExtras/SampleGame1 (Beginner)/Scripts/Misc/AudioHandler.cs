using System;
using UnityEngine;
using Zenject;

namespace Zenject.Asteroids
{
    public class AudioHandler : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly AudioSource _audioSource;

        Signals.ShipCrashed _shipCrashed;

        public AudioHandler(
            Signals.ShipCrashed shipCrashed,
            AudioSource audioSource,
            Settings settings)
        {
            _settings = settings;
            _audioSource = audioSource;
            _shipCrashed = shipCrashed;
        }

        public void Initialize()
        {
            _shipCrashed += OnShipCrashed;
        }

        public void Dispose()
        {
            _shipCrashed -= OnShipCrashed;
        }

        void OnShipCrashed()
        {
            _audioSource.PlayOneShot(_settings.CrashSound);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip CrashSound;
        }
    }
}
