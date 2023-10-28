using UnityEngine;

namespace Ultracat
{
    public class TutorialTable : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerEntity>(out var player))
            {
                //FindAnyObjectByType<UIManager>().OpenTutorial();
            }
        }
    }
}