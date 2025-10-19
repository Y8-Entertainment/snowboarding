using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float loadDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            // Check if god mode is active (cheat system)
            if (CheatManager.Instance != null && CheatManager.Instance.IsGodModeActive())
            {
                Debug.Log("[CHEAT] God Mode prevented crash!");
                return; // Skip crash logic
            }

            Debug.Log("game over");

            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null!");
                return;
            }

            AudioManager.Instance.PlayMusic(AudioManager.Instance.loseMusic);
            GameManager.Instance.ShowGameOverWithDelay(loadDelay);
        }
    }
}
