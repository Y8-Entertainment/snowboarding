using System.Collections;
using UnityEngine;

/// <summary>
/// Quản lý chức năng khiên (shield) cho người chơi.
/// Kích hoạt một khiên có thời hạn, có thể bị phá hủy sớm khi va chạm.
/// </summary>
public class PlayerShield : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Prefab của đối tượng khiên sẽ được hiển thị.")]
    [SerializeField] private GameObject helmetShieldPrefab;

    [Tooltip("Thời gian (giây) cho animation xuất hiện.")]
    [SerializeField] private float appearDuration = 1.0f;

    [Tooltip("Thời gian (giây) cho animation biến mất.")]
    [SerializeField] private float fadeOutDuration = 1.0f;

    [Header("Audio")]
    [Tooltip("Âm thanh khi khiên được kích hoạt.")]
    [SerializeField] private AudioClip shieldActivateSound;

    [Tooltip("Âm thanh khi khiên hết hạn hoặc bị phá vỡ.")]
    [SerializeField] private AudioClip shieldDeactivateSound;

    // --- Private Variables ---
    private GameObject helmetShieldInstance;
    private Coroutine shieldCoroutine;

    /// <summary>
    /// Trạng thái hiện tại của khiên. True nếu đang hoạt động.
    /// </summary>
    public bool IsShieldActive { get; private set; } = false;

    /// <summary>
    /// Kích hoạt khiên trong một khoảng thời gian nhất định.
    /// </summary>
    /// <param name="duration">Tổng thời gian khiên tồn tại (tính cả animation).</param>
    public void ActivateShield(float duration)
    {
        // Nếu đã có khiên, hủy coroutine cũ trước khi tạo cái mới
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        // Hủy khiên cũ nếu còn tồn tại
        if (helmetShieldInstance != null)
        {
            Destroy(helmetShieldInstance);
        }

        // Tạo khiên mới
        helmetShieldInstance = Instantiate(helmetShieldPrefab, transform);
        helmetShieldInstance.transform.localPosition = Vector3.zero;

        IsShieldActive = true;

        if (shieldActivateSound != null)
        {
            AudioManager.Instance.PlaySFX(shieldActivateSound);
        }

        // Bắt đầu và lưu lại tham chiếu của coroutine
        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    /// <summary>
    /// Phá hủy khiên ngay lập tức (ví dụ: khi va chạm).
    /// </summary>
    public void ConsumeShield()
    {
        if (!IsShieldActive) return;

        Debug.Log("Shield consumed after impact.");
        DeactivateShield(true); // Gọi hàm hủy khiên với hiệu ứng fade out
    }

    private IEnumerator ShieldRoutine(float totalDuration)
    {
        Animator anim = helmetShieldInstance.GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found in HelmetShield prefab's children!");
            DeactivateShield(false); // Hủy ngay lập tức nếu không có animator
            yield break;
        }

        // 1. Chờ animation xuất hiện kết thúc
        anim.Play("ShieldAppear");
        yield return new WaitForSeconds(appearDuration);

        // 2. Tính toán và chờ thời gian khiên tồn tại (idle)
        float totalAnimationTime = appearDuration + fadeOutDuration;
        float idleTime = totalDuration - totalAnimationTime;
        if (idleTime > 0)
        {
            yield return new WaitForSeconds(idleTime);
        }

        // 3. Kích hoạt animation biến mất và hủy khiên
        DeactivateShield(true);
    }

    /// <summary>
    /// Hàm trung tâm để hủy khiên và dọn dẹp.
    /// </summary>
    /// <param name="playFadeOut">True nếu muốn chạy animation biến mất.</param>
    private void DeactivateShield(bool playFadeOut)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
            shieldCoroutine = null;
        }

        IsShieldActive = false;

        if (shieldDeactivateSound != null)
        {
            AudioManager.Instance.PlaySFX(shieldDeactivateSound);
        }

        if (helmetShieldInstance != null)
        {
            if (playFadeOut)
            {
                Animator anim = helmetShieldInstance.GetComponentInChildren<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("FadeOut");
                }
                Destroy(helmetShieldInstance, fadeOutDuration); // Hủy sau khi animation kết thúc
            }
            else
            {
                Destroy(helmetShieldInstance); // Hủy ngay lập tức
            }
        }

        helmetShieldInstance = null;
    }
}