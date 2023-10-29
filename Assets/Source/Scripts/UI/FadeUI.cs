using Ultracat;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeUI : MonoBehaviour
{
    Animator animator;
    [SerializeField] DungeonGenerator _dungeonGenerator;
    private void Start()
    {
        _dungeonGenerator = FindAnyObjectByType<DungeonGenerator>();
        animator = GetComponent<Animator>();
        _dungeonGenerator.onRoomSpawned += Fade;
    }
    private void OnDestroy()
    {
        _dungeonGenerator.onRoomSpawned -= Fade;
    }
    public void Fade()
    {
        animator.Play("FadeIn");
    }
}
