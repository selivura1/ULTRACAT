using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ultracat
{
    public class GameStarter : MonoBehaviour
    {
        public static void Restart()
        {
            TimeControl.SetPause(false);
            PlayerControl.EnableIngameControls = true;
            Score.ResetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void CloseGame()
        {
            Application.Quit();
        }
    }
}