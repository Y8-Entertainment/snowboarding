using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private string animationTriggerName = "Play";

  
    [SerializeField] private AudioClip triggerSound;

    private Animator animator;
    private bool hasTriggered = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag(targetTag))
        {
            return;
        }

        hasTriggered = true;

        
        if (triggerSound != null)
        {
            AudioManager.Instance.PlaySFX(triggerSound);
        }

        animator.SetTrigger(animationTriggerName);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}