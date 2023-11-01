using UnityEngine;
using UnityEngine.Events;

namespace Ultracat
{
    public class Trigger : MonoBehaviour
    {
        public UnityEvent onTouch;
        public virtual void OnTouch(Collision2D collision)
        {

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerEntity>())
            {
                onTouch?.Invoke();
                OnTouch(collision);
            }
        }
    }
}
