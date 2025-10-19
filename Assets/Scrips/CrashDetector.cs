using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float loadDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            if (controller != null && controller.IsPermanentShield())
            {
                AudioManager.Instance.PlayMusic(AudioManager.Instance.loseMusic);
            GameManager.Instance.ShowGameOverWithDelay(loadDelay);
            }

            Debug.Log("game over");

            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null!");
                return;
            }

            Debug.Log("🛡️ Khiên vĩnh cửu: Miễn nhiễm va chạm với đất nguy hiểm!");
                return;
        }
    }
}
