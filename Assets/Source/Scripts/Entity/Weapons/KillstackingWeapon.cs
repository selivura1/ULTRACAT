using UnityEngine;
namespace Ultracat
{
    public enum StackBehaviour
    {
        ResetOnDamage,
        ReduceOnDamage,
        DoNothing
    }
    public class KillstackingWeapon : ProjectileWeapon
    {
        [SerializeField] protected int stacks;
        [SerializeField] protected StackBehaviour stackBehaviour;
        protected void Start()
        {
            user.onDamage += OnUserDamaged;
            user.onKill += OnUserKill;
        }
        protected void OnUserKill()
        {
            stacks++;
            OnStackRecieved();
            OnStackUpdated();
        }
        protected virtual void OnStackUpdated()
        {

        }
        protected virtual void OnStackRecieved()
        {

        }

        protected virtual void OnStackReduced()
        {

        }

        protected virtual void OnStackReset()
        {

        }
        protected void OnUserDamaged(DamageBreakDown damage)
        {
            switch (stackBehaviour)
            {
                case StackBehaviour.ResetOnDamage:
                    stacks = 0;
                    OnStackReset();
                    break;
                case StackBehaviour.ReduceOnDamage:
                    stacks--;
                    OnStackReduced();
                    break;
                case StackBehaviour.DoNothing:
                    break;
                default:
                    break;
            }
            OnStackUpdated();
        }
    }

}