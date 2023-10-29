using UnityEngine;

namespace Ultracat
{
    public class GameStartPortal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerEntity>())
            {
                GameStarter.StartGame();
            }
        }
    }
}