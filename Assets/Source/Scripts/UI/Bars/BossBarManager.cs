using System.Collections.Generic;
using UnityEngine;
namespace Ultracat
{
    public class BossBarManager : MonoBehaviour
    {
        [SerializeField] BossBar _barPref;
        List<BossBar> _spawnedBars = new List<BossBar>();
        public void CreateBar(EntityBase entity)
        {
            BossBar spawned = Instantiate(_barPref, transform);
            spawned.Initialize(entity);
            _spawnedBars.Add(spawned);
        }
    }
}