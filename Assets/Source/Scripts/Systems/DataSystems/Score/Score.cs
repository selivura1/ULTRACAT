using UnityEngine;

namespace Ultracat
{
    public class Score : MonoBehaviour
    {
        public static int CurrentScore { get; private set; } = 0;

        public const string HighScoreKey = "HighScore";
        public static int LevelUps { get; private set; } = 0;
        public static int HighScore { get; internal set; }

        public static System.Action onScoreChanged;
        private void Start()
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey);
            onScoreChanged?.Invoke();
        }
        public static void AddScore(int amount)
        {
            CurrentScore += amount;
            if (HighScore < CurrentScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt(HighScoreKey, HighScore);
            }
            onScoreChanged?.Invoke();
        }
        public static void ResetScore()
        {
            CurrentScore = 0;
        }
        public static void ResetHighScore()
        {
            HighScore = 0;
            PlayerPrefs.SetInt(HighScoreKey, HighScore);
            onScoreChanged?.Invoke();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                ResetHighScore();
        }
#endif
    }
}