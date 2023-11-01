using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ultracat
{
    public class LevelLoader : MonoBehaviour
    {
        public static void RestartCurrentLevel()
        {
            TimeControl.SetPause(false);
            PlayerControl.EnableIngameControls = true;
            Score.ResetScore();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        public static void CloseGame()
        {
            Application.Quit();
        }
    }
}