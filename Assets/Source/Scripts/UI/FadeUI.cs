using UnityEngine;

//Script for room transition effect
[RequireComponent(typeof(Animator))]
public class FadeUI : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Fade()
    {
        animator.Play("FadeIn");
    }
}
