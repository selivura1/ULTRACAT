using UnityEngine;
using UnityEngine.UI;
namespace Ultracat
{
    public enum EffectTypes
    {
        Channel,
        Warning,
        Danger,
        Confusion,
        Stun,
        Charm,
        Freezed,
        Heal,
    }
    public class StatBar : MonoBehaviour
    {
        AIControl _ownerAI;
        EntityBase _owner;
        [SerializeField] ProgressBar _bar;
        [SerializeField] Transform _grid;
        [SerializeField] Sprite[] _icons;
        [SerializeField] Vector2 _offset;
        Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }
        public void Initialize(EntityBase newOwner)
        {
            _owner = newOwner;
            var newAi = _owner.GetComponent<AIControl>();
            _ownerAI = newAi;
            SubToEntityEvents();
            SubToAIEvents();
        }
        private void SubToEntityEvents()
        {
            _owner.onDeath += Terminate;
        }
        private void UnsubFromEntityEvents()
        {
            _owner.onDeath -= Terminate;
        }
        public void SubToAIEvents()
        {
            if (_ownerAI)
                _ownerAI.onAttackPrepare += AIWarningEffectSpawn;
        }
        public void UnsubFromAIEvents()
        {
            if (_ownerAI)
                _ownerAI.onAttackPrepare -= AIWarningEffectSpawn;
        }
        private void Terminate(EntityBase ent)
        {
            UnsubFromEntityEvents();
            UnsubFromAIEvents();
            gameObject.SetActive(false);
        }

        private void AIWarningEffectSpawn(float prepareTime)
        {
            AddEffect(EffectTypes.Warning, prepareTime);
        }
        private void FixedUpdate()
        {
            _bar.Max = _owner.EntityStats.Health.Value;
            _bar.CurrentValue = _owner.GetHealth();
            transform.position = _camera.WorldToScreenPoint(_owner.transform.position + (Vector3)_offset);
        }
        public void AddEffect(EffectTypes type, float time)
        {
            var spawned = new GameObject();
            spawned.transform.SetParent(_grid);
            spawned.transform.localScale = Vector3.one;
            spawned.AddComponent<Image>().sprite = _icons[(int)type];
            Destroy(spawned, time);
        }
    }
}