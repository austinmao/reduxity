#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
    public class AddToNewGameObjectComponentProvider : AddToGameObjectComponentProviderBase
    {
        readonly GameObjectCreationParameters _gameObjectBindInfo;

        public AddToNewGameObjectComponentProvider(
            DiContainer container, Type componentType,
            object concreteIdentifier, List<TypeValuePair> extraArguments, GameObjectCreationParameters gameObjectBindInfo)
            : base(container, componentType, concreteIdentifier, extraArguments)
        {
            _gameObjectBindInfo = gameObjectBindInfo;
        }

        protected override GameObject GetGameObject(InjectContext context)
        {
            if (_gameObjectBindInfo.Name == null)
            {
                _gameObjectBindInfo.Name = ConcreteIdentifier as string ?? ComponentType.Name();
            }

            return Container.CreateEmptyGameObject(_gameObjectBindInfo);
        }
    }
}

#endif
