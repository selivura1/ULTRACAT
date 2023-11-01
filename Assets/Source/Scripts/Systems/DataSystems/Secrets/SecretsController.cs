using UnityEngine;
namespace Ultracat
{
    public class SecretsController : MonoBehaviour
    {
        DungeonGenerator _dungeonGenerator;
        Database _database;
        [SerializeField] int[] _stageSecrets;
        [SerializeField] ScoreSecretEntry[] _scoreSecrets;
        private void Start()
        {
            _database = GameManager.Database;
            _dungeonGenerator = GameManager.DungeonGenerator;
            Sub();
        }
        private void OnDestroy()
        {
            Unsub();
        }
        private void Sub()
        {
            if(_dungeonGenerator)
            _dungeonGenerator.onStageCompleted += CheckOnStageCompleteUnlocks;
            Score.onScoreChanged += CheckScoreSecrets;
        }
        private void Unsub()
        {
            if (_dungeonGenerator)
                _dungeonGenerator.onStageCompleted -= CheckOnStageCompleteUnlocks;
            Score.onScoreChanged -= CheckScoreSecrets;
        }
        private void CheckScoreSecrets()
        {
            foreach (var secret in _scoreSecrets)
            {
                if (Score.CurrentScore >= secret.Score)
                    _database.UnlockSecret(secret.SecretID);
            }
        }
        private void CheckOnStageCompleteUnlocks()
        {
            _database.UnlockSecret(_stageSecrets[_dungeonGenerator.Stage]);

        }
    }
    [System.Serializable]
    class ScoreSecretEntry
    {
        public int Score;
        public int SecretID;
    }
}