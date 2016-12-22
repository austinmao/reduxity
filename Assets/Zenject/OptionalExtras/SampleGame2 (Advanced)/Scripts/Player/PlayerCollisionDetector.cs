using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        PlayerSignals.Hit _hitTrigger;

        [Inject]
        public void Construct(PlayerSignals.Hit hitTrigger)
        {
            _hitTrigger = hitTrigger;
        }

        public void OnTriggerEnter(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();

            if (bullet != null && bullet.Type != BulletTypes.FromPlayer)
            {
                _hitTrigger.Fire(bullet);
            }
        }
    }
}

