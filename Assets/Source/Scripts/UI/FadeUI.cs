using Ultracat;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeUI : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string _fadeKeyAnimation = "FadeIn";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Fade()
    {
        animator.Play(_fadeKeyAnimation);
    }
}
