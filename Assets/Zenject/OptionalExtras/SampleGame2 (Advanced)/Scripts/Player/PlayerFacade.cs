using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerFacade : MonoBehaviour
    {
        PlayerModel _model;

        [Inject]
        public void Construct(PlayerModel player)
        {
            _model = player;
        }

        public bool IsDead
        {
            get
            {
                return _model.IsDead;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _model.Position;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return _model.Rotation;
            }
        }
    }
}
