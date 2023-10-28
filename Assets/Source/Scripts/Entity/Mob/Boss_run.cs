using UnityEngine;
namespace Ultracat
{
    public class Boss_run : StateMachineBehaviour
    {
        PlayerEntity _player;
        Movement _movement;
        public float MeleeAttackRange = 1.2f;
        public float RangedAttackRange = 10;
        public string MeleeAttack = "Attack1", RangedAttack = "Attack3";
        string lastAttack;
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameManager.PlayerSpawner.GetPlayer();
            _movement = animator.GetComponent<Movement>();
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _movement.Move((_player.transform.position - animator.transform.position).normalized);
            var dist = Vector2.Distance(_player.transform.position, _movement.transform.position);
            if (dist < MeleeAttackRange)
            {
                lastAttack = MeleeAttack;
                animator.SetTrigger(lastAttack);
                _movement.Move(Vector2.zero);
                return;
            }
            else if (dist < RangedAttackRange)
            {
                lastAttack = RangedAttack;
                animator.SetTrigger(lastAttack);
                return;
            }
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