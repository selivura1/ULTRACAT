using UnityEngine;
using UnityEngine.UI;

public class BossfightUI : MonoBehaviour
{
    [SerializeField] Image _challengerImage;
    [SerializeField] Animator _versusScreen;

    public void ShowVersus(Sprite challengerIcon)
    {
        _challengerImage.sprite = challengerIcon;
        _versusScreen.Play("Versus");
        TimeControl.SetPause(true);
    }
    public void HideVersus()
    {
        TimeControl.SetPause(false);
    }
}
