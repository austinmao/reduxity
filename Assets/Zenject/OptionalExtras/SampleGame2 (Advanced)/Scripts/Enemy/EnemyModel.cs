using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class EnemyModel
    {
        readonly float _speed;
        readonly MeshRenderer _renderer;
        readonly Collider _collider;
        readonly Rigidbody _rigidBody;

        float _health = 100.0f;

        public EnemyModel(
            Rigidbody rigidBody,
            Collider collider,
            MeshRenderer renderer,
            EnemyTunables tunables)
        {
            _speed = tunables.Speed;
            _renderer = renderer;
            _collider = collider;
            _rigidBody = rigidBody;
        }

        public Vector3 LookDir
        {
            get
            {
                return -_rigidBody.transform.right;
            }
        }

        public Vector3 DesiredLookDir
        {
            get;
            set;
        }

        public float MoveSpeed
        {
            get
            {
                return _speed;
            }
        }

        public Vector3 RightDir
        {
            get
            {
                return _rigidBody.transform.up;
            }
        }

        public Vector3 ForwardDir
        {
            get
            {
                return _rigidBody.transform.right;
            }
        }

        public float Health
        {
            get
            {
                return _health;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _rigidBody.transform.position;
            }
            set
            {
                _rigidBody.transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return _rigidBody.rotation;
            }
            set
            {
                _rigidBody.rotation = value;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return _rigidBody.velocity;
            }
        }

        public Collider Collider
        {
            get
            {
                return _collider;
            }
        }

        public MeshRenderer Renderer
        {
            get
            {
                return _renderer;
            }
        }

        public Vector3 AngularVelocity
        {
            get
            {
                return _rigidBody.angularVelocity;
            }
            set
            {
                _rigidBody.angularVelocity = value;
            }
        }

        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }

        public void AddTorque(float value)
        {
            _rigidBody.AddTorque(Vector3.forward * value);
        }

        public void TakeDamage(float healthLoss)
        {
            _health -= healthLoss;
        }
    }
}
