using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(DungeonGenerator))]
    public class DungeonSecretController : MonoBehaviour
    {
        Database _database;
        DungeonGenerator _dungeonGenerator;
        [SerializeField] int[] _stageSecrets;
        private void Start()
        {
            _database = GameManager.Database;
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            Sub();
        }
        private void OnDestroy()
        {
            Unsub();
        }
        private void Sub()
        {
            _dungeonGenerator.onStageCompleted += CheckOnStageCompleteUnlocks;
        }
        private void Unsub()
        {
            _dungeonGenerator.onStageCompleted -= CheckOnStageCompleteUnlocks;
        }
        private void CheckOnStageCompleteUnlocks()
        {
            _database.UnlockSecret(_stageSecrets[_dungeonGenerator.Stage]);

        }
    }
}
