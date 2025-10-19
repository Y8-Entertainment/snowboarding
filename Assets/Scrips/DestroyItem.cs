using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    [SerializeField] private float loadDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Đã va chạm với: " + other.name);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.getHurt);
        if (other.CompareTag("Player"))
        {
            PlayerController controller = other.GetComponent<PlayerController>();

            if (controller != null)
            {
                // Kiểm tra khiên vĩnh cửu trước
                if (controller.IsPermanentShield())
                {
                    Debug.Log("🛡️ Khiên vĩnh cửu: Miễn nhiễm va chạm với chướng ngại vật!");
                    return; // Không game over, không tiêu thụ khiên
                }

                if (controller.IsInvulnerable())
                {
                    Debug.Log("Player có khiên, miễn nhiễm. Khiên bị tiêu thụ.");
                    controller.ForceDeactivateShield(); // Bắt buộc hủy khiên
                    return; // Không gọi GameOver
                }

                Debug.Log("Va chạm với Player! Restarting...");
                Debug.Log("game over");

                if (GameManager.Instance == null)
                {
                    Debug.LogError("GameManager.Instance is null!");
                    return;
                }

                GameManager.Instance.ShowGameOverWithDelay(loadDelay);
            }
        }
    }
}
