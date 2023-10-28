using UnityEngine;

//Trigger for game start in lobby
namespace Ultracat
{
    public class GameStartPortal : MonoBehaviour
    {
        private FadeUI _fade;
        private void Start()
        {
            _fade = FindAnyObjectByType<FadeUI>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerEntity>())
            {
                GameStarter.StartGame();
                _fade.Fade();
            }
        }
    }
}