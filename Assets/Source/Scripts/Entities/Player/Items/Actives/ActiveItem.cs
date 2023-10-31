using UnityEngine;
namespace Ultracat
{
    public class ActiveItem : Item
    {
        public int Charge { get; private set; } = 100;
        [SerializeField] protected int maxCharge = 100;
        public float MaxCharge => maxCharge;
        public System.Action onActiveCharged;
        private void Start()
        {
            entity.onKill += AddCharge;
            Charge = maxCharge;
        }

        private void AddCharge()
        {
            Charge++;
            if (Charge == maxCharge)
                onActiveCharged?.Invoke();
        }
        public void ExecuteActive()
        {
            if (Charge < maxCharge) return;
            OnActivate();
            Charge = 0;
        }
        public virtual void OnActivate()
        {

        }
    }
}