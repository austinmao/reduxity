using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerBulletHitHandler : IInitializable, IDisposable
    {
        readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly PlayerModel _model;

        PlayerSignals.Hit _hitSignal;

        public PlayerBulletHitHandler(
            PlayerModel model,
            Settings settings,
            AudioPlayer audioPlayer,
            PlayerSignals.Hit hitSignal)
        {
            _hitSignal = hitSignal;
            _audioPlayer = audioPlayer;
            _settings = settings;
            _model = model;
        }

        public void Initialize()
        {
            _hitSignal += OnHit;
        }

        public void Dispose()
        {
            _hitSignal -= OnHit;
        }

        void OnHit(Bullet bullet)
        {
            _audioPlayer.Play(_settings.HitSound, _settings.HitVolume);
            _model.AddForce(-bullet.MoveDirection * _settings.HitForce);

            _model.TakeDamage(_settings.HealthLoss);

            GameObject.Destroy(bullet.gameObject);
        }

        [Serializable]
        public class Settings
        {
            public float HealthLoss;
            public float HitForce;
            public AudioClip HitSound;
            public float HitVolume;
        }
    }
}
