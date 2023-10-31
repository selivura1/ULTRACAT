using TMPro;
using UnityEngine;

namespace Ultracat
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        private void OnEnable()
        {
            Score.onScoreChanged += Refresh;
            Refresh();
        }
        private void OnDisable()
        {
            Score.onScoreChanged -= Refresh;
        }
        private void Refresh()
        {
            text.text = "SCORE\n" + Score.CurrentScore + "\nBEST\n" + Score.HighScore;
        }
    }
}