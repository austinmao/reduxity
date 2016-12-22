using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public enum BulletTypes
    {
        FromEnemy,
        FromPlayer,
    }

    public class Bullet : MonoBehaviour
    {
        float _startTime;
        BulletTypes _type;
        float _speed;
        float _lifeTime;

        [SerializeField]
        MeshRenderer _renderer = null;

        [SerializeField]
        Material _playerMaterial = null;

        [SerializeField]
        Material _enemyMaterial = null;

        // Since we are a MonoBehaviour, we can't use a constructor
        // So use PostInject instead
        [Inject]
        public void Construct(float speed, float lifeTime, BulletTypes type)
        {
            _type = type;
            _speed = speed;
            _lifeTime = lifeTime;

            _renderer.material = type == BulletTypes.FromEnemy ? _enemyMaterial : _playerMaterial;

            _startTime = Time.realtimeSinceStartup;
        }

        public BulletTypes Type
        {
            get
            {
                return _type;
            }
        }

        public Vector3 MoveDirection
        {
            get
            {
                return transform.right;
            }
        }

        public void Update()
        {
            transform.position -= transform.right * _speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public class Factory : Factory<float, float, BulletTypes, Bullet>
        {
        }
    }
}
