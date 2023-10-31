using UnityEngine;

//Score system of the game, monitor's ingame events to calculate player's score
namespace Ultracat
{
    public class Score : MonoBehaviour
    {
        public static int CurrentScore { get; private set; } = 0;
        [SerializeField] int killScore = 100;
        [SerializeField] int stageCompleteScore = 1000;
        [SerializeField] int levelUpScore = 200;
        [SerializeField] int roomClearScore = 50;
        public static int Kills { get; private set; } = 0;
        public static int LevelUps { get; private set; } = 0;
        public static int StageComplete { get; private set; } = 0;
        public static int RoomsCleared { get; private set; } = 0;
        public static int HighScore { get; internal set; }

        PlayerEntity player;
        PlayerLevels playerLevels;
        Combat playerCombat;
        DungeonGenerator dungeonGenerator;
        public static System.Action onScoreChanged;
        private void Start()
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
            player = FindAnyObjectByType<PlayerEntity>();
            playerLevels = player.GetComponent<PlayerLevels>();
            dungeonGenerator = GameManager.DungeonGenerator;

            player.onKill += OnKill;
            playerLevels.onLevelUp += OnlevelUp;
            onScoreChanged?.Invoke();

            if (!dungeonGenerator) return;
            dungeonGenerator.onRoomPreDestroy += RemoveRoomListener;
            dungeonGenerator.onRoomSpawned += AddRoomListener;
            dungeonGenerator.onStageCompleted += OnStageComplete;
        }
        void RemoveRoomListener()
        {
            dungeonGenerator.CurrentRoom.onRoomClear -= OnRoomClear;
        }
        void AddRoomListener()
        {
            dungeonGenerator.CurrentRoom.onRoomClear += OnRoomClear;
        }
        private void OnDestroy()
        {
            player.onKill -= OnKill;
            playerLevels.onLevelUp -= OnlevelUp;
            if (dungeonGenerator)
            {
                dungeonGenerator.onRoomPreDestroy -= RemoveRoomListener;
                dungeonGenerator.onRoomSpawned -= AddRoomListener;
                dungeonGenerator.onStageCompleted -= OnStageComplete;
            }
        }
        public void OnlevelUp()
        {
            LevelUps++;
            AddScore(levelUpScore);
        }
        public void OnStageComplete()
        {
            StageComplete++;
            AddScore(stageCompleteScore);
        }
        public void OnRoomClear()
        {
            RoomsCleared++;
            AddScore(roomClearScore);
        }
        public void OnKill()
        {
            Kills++;
            AddScore(killScore);
        }
        void AddScore(int amount)
        {
            CurrentScore += amount;
            if (HighScore < CurrentScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt("HighScore", HighScore);
            }
            //if (CurrentScore >= 5000)
            //    GameManager.Database.UnlockSecret(_5kScoreSecret);
            //if (CurrentScore >= 10000)
            //    GameManager.Database.UnlockSecret(_10kScoreSecret);
            onScoreChanged?.Invoke();
        }
        public static void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}