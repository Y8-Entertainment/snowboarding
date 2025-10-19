using UnityEngine;

public class FallZone : MonoBehaviour
{
    [SerializeField] private float loadDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if god mode is active (cheat system)
            if (CheatManager.Instance != null && CheatManager.Instance.IsGodModeActive())
            {
                Debug.Log("[CHEAT] God Mode prevented fall death!");
                return; // Skip death logic
            }

            Debug.Log("Player rơi xuống vực - Game Over!");
            AudioManager.Instance.PlayMusic(AudioManager.Instance.loseMusic);

            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null!");
                return;
            }

            GameManager.Instance.ShowGameOverWithDelay(loadDelay);
        }
    }
}