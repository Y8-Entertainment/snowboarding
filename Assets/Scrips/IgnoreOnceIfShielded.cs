using UnityEngine;

public class IgnoreTriggerOnceIfShielded : MonoBehaviour
{
    private bool hasIgnoredPlayer = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Kiểm tra khiên vĩnh cửu trước
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null && controller.IsPermanentShield())
        {
            Debug.Log("🛡️ Khiên vĩnh cửu: Miễn nhiễm va chạm với item: " + gameObject.name);
            return; // Không xử lý va chạm
        }

        PlayerShield shield = other.GetComponent<PlayerShield>();
        if (shield != null && shield.IsShieldActive)
        {
            if (!hasIgnoredPlayer)
            {
                Debug.Log("Player có khiên - miễn nhiễm 1 lần với item: " + gameObject.name);
                hasIgnoredPlayer = true;        // Không xử lý va chạm lần này
                return;
            }
        }

        // Nếu không có khiên hoặc đã hết khiên thì xử lý bình thường
        HandleImpact(other.gameObject);
    }

    private void HandleImpact(GameObject player)
    {
        Debug.Log("Player bị ảnh hưởng bởi item: " + gameObject.name);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowGameOverWithDelay(0.5f);
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
}
