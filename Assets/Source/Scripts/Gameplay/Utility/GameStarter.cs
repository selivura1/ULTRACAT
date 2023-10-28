using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ultracat
{
    public class GameStarter : MonoBehaviour
    {
        public static void StartGame()
        {
            Destroy(FindAnyObjectByType<Room>().gameObject);
            GameManager.DungeonGenerator.CreateDungeon();
            GameManager.PlayerSpawner.GetPlayer().Heal(9999);
        }
        public static void Restart()
        {
            GameManager.PlayerSpawner.ResetGame();
            TimeControl.SetPause(false);
            PlayerControl.enableIngameControls = true;
            Score.ResetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void CloseGame()
        {
            Application.Quit();
        }
    }
}