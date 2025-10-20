using UnityEngine;

// Thêm comment giải thích mục đích của script (Good practice for GitHub)
/// <summary>
/// Controls the behavior of a wood trap item. 
/// It breaks the player's shield or slows them down upon collision.
/// </summary>
public class WoodItem : MonoBehaviour
{
    // GỢI Ý 2: Đưa các giá trị ra Inspector để dễ dàng tùy chỉnh
    [Header("Game Balance Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    private float speedReductionFactor = 0.5f; // Tỷ lệ tốc độ còn lại (0.5f = 50%)

    [SerializeField]
    private float slowDuration = 3f; // Thời gian làm chậm

    [Header("Component References")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip collectionSound; // Tham chiếu trực tiếp đến âm thanh

    private bool hasTriggered = false;

    // Dùng Reset() hoặc Awake() để tự động lấy component, giảm thao tác thủ công
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player"))
        {
            return;
        }

        hasTriggered = true;

        // Kích hoạt animation
        if (animator != null)
        {
            animator.SetTrigger("Activate");
        }

        // Phát âm thanh
        if (collectionSound != null)
        {
            AudioManager.Instance.PlaySFX(collectionSound);
        }

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.IsInvulnerable()) // Kiểm tra có khiên không
            {
                player.ForceDeactivateShield(); // Huỷ khiên
            }
            else
            {
                // GỢI Ý 2: Sử dụng các biến đã khai báo
                player.ReduceSpeedTemporarily(speedReductionFactor, slowDuration);
            }
        }
    }

    // Hàm này sẽ được gọi từ Animation Event cuối clip để tự hủy
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}