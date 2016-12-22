using System.Collections.Generic;

namespace Zenject.SpaceFighter
{
    public class EnemyRegistry
    {
        readonly List<EnemyFacade> _enemies = new List<EnemyFacade>();

        public IEnumerable<EnemyFacade> Enemies
        {
            get
            {
                return _enemies;
            }
        }

        public int NumEnemies
        {
            get
            {
                return _enemies.Count;
            }
        }

        public void AddEnemy(EnemyFacade facade)
        {
            _enemies.Add(facade);
        }

        public void RemoveEnemy(EnemyFacade facade)
        {
            _enemies.Remove(facade);
        }
    }
}
