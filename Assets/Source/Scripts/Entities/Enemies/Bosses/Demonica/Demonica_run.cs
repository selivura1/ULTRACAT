using UnityEngine;
namespace Ultracat
{
    public class Demonica_run : StateMachineBehaviour
    {
        PlayerEntity _player;
        public float BarrageChance = 25;
        public float ZoneAttackChance = 50;
        public float RangedAttackRange = 10;
        public string TeleportAttack = "Attack1";
        public string ZoneAttack = "ZoneAttack";
        public string BarrageAttack = "Barrage";
        public string RangeAttack = "RangeAttack";
        public float Stamina = 3;
        public float StaminaMax = 3;
        public float StaminaRegen = 1;
        public float AttackCost = 1;
        string lastAttack;
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = FindAnyObjectByType<PlayerEntity>();
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var dist = Vector2.Distance(_player.transform.position, animator.transform.position);
            Stamina += Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0, StaminaMax);
            if (Stamina >= AttackCost)
            {
                Stamina -= AttackCost;
                if (Random.Range(0f, 100f) <= BarrageChance)
                {
                    Stamina = 0;
                    lastAttack = BarrageAttack;
                }
                else if (Random.Range(0f, 100f) <= BarrageChance)
                {
                    lastAttack = ZoneAttack;
                }
                else if (dist < RangedAttackRange)
                {
                    lastAttack = RangeAttack;
                }
                else
                {
                    lastAttack = TeleportAttack;
                }
            }
            animator.SetTrigger(lastAttack);
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(lastAttack);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
