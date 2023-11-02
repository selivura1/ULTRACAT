using UnityEngine;
namespace Ultracat
{
    public class ScoreSecretsController : MonoBehaviour
    {
        Database _database;
        [SerializeField] ScoreSecretEntry[] _scoreSecrets;
        private void Start()
        {
            _database = GameManager.Database;
            Sub();
        }
        private void OnDestroy()
        {
            Unsub();
        }
        private void Sub()
        {
            Score.onScoreChanged += CheckScoreSecrets;
        }
        private void Unsub()
        {
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
    }
    [System.Serializable]
    class ScoreSecretEntry
    {
        public int Score;
        public int SecretID;
    }
}