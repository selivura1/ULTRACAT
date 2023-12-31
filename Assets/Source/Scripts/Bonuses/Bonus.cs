using UnityEngine;
namespace Ultracat
{
    public class Bonus : MonoBehaviour
    {
        protected EntityBase _target;
        [SerializeField] protected float _value = 5;
        [SerializeField] protected float _speed = 5;
        [SerializeField] protected float _range = 2;
        bool _collected;
        public void Initialize()
        {
            _collected = false;
            _target = FindAnyObjectByType<PlayerEntity>();
        }
        void FixedUpdate()
        {
            if (!_target) return;

            if (Vector2.Distance(_target.transform.position, transform.position) <= _range)
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);

            if (Vector2.Distance(transform.position, _target.transform.position) <= .4f)
            {
                if (!_collected)
                {
                    OnCollected();
                }
                _collected = true;
            }
        }

        public virtual void OnCollected()
        {
            _target.Heal(_target.EntityStats.Health.Value * _value / 100);
            gameObject.SetActive(false);
        }
    }
}