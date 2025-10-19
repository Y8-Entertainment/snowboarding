using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// CheatUI - Visual feedback system for the cheat system
/// Displays active cheats and cheat mode indicator
/// 
/// UI Setup Instructions:
/// 1. Create a Canvas called "CheatOverlayCanvas" with Screen Space - Overlay
/// 2. Add this script to the canvas
/// 3. Create UI elements and assign references in inspector
/// </summary>
public class CheatUI : MonoBehaviour
{
    #region Singleton
    public static CheatUI Instance { get; private set; }
    #endregion

    #region UI References
    [Header("Cheat Mode Indicator")]
    [SerializeField] private GameObject cheatModeIndicator;
    [SerializeField] private TextMeshProUGUI cheatModeText;
    [SerializeField] private Image cheatModeBorder;

    [Header("Active Cheats Display")]
    [SerializeField] private GameObject activeCheatsPanel;
    [SerializeField] private TextMeshProUGUI activeCheatsHeader;
    [SerializeField] private TextMeshProUGUI activeCheatsListText;

    [Header("Screen Border Effect")]
    [SerializeField] private Image screenBorder;

    [Header("Cheat Activation Feedback")]
    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private TextMeshProUGUI feedbackText;
    #endregion

    #region Configuration
    [Header("Visual Settings")]
    [SerializeField] private Color cheatModeColor = new Color(1f, 1f, 0f, 0.8f); // Yellow
    [SerializeField] private Color borderColor = new Color(0f, 1f, 0f, 0.3f); // Green
    [SerializeField] private float borderPulseSpeed = 2f;
    [SerializeField] private float feedbackDuration = 2f;
    #endregion

    #region Private Variables
    private List<string> activeCheatNames = new List<string>();
    private float feedbackTimer = 0f;
    private bool isFeedbackActive = false;
    private CanvasGroup canvasGroup;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get canvas group for fade effects
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Initialize UI state
        HideAllUI();
    }

    private void Start()
    {
        // Subscribe to cheat manager events
        if (CheatManager.Instance != null)
        {
            CheatManager.Instance.OnCheatActivated += HandleCheatActivated;
        }

        // Initial update
        UpdateUI();
    }

    private void Update()
    {
        // Update UI every frame to reflect current cheat state
        UpdateUI();

        // Handle feedback timer
        if (isFeedbackActive)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f)
            {
                HideFeedback();
            }
        }

        // Animate border pulse if active
        if (screenBorder != null && screenBorder.gameObject.activeSelf)
        {
            AnimateBorderPulse();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (CheatManager.Instance != null)
        {
            CheatManager.Instance.OnCheatActivated -= HandleCheatActivated;
        }
    }
    #endregion

    #region UI Update Methods
    /// <summary>
    /// Main UI update method - called every frame
    /// </summary>
    private void UpdateUI()
    {
        if (CheatManager.Instance == null)
        {
            HideAllUI();
            return;
        }

        bool cheatModeEnabled = CheatManager.Instance.IsCheatModeEnabled();

        if (cheatModeEnabled)
        {
            ShowCheatModeIndicator();
            UpdateActiveCheatsDisplay();
            ShowScreenBorder();
        }
        else
        {
            HideAllUI();
        }
    }

    /// <summary>
    /// Show the cheat mode indicator
    /// </summary>
    private void ShowCheatModeIndicator()
    {
        if (cheatModeIndicator != null)
        {
            cheatModeIndicator.SetActive(true);
        }

        if (cheatModeText != null)
        {
            cheatModeText.text = "CHEAT MODE: ON";
            cheatModeText.color = cheatModeColor;
        }

        if (cheatModeBorder != null)
        {
            cheatModeBorder.color = cheatModeColor;
        }
    }

    /// <summary>
    /// Update the list of active cheats
    /// </summary>
    private void UpdateActiveCheatsDisplay()
    {
        if (CheatManager.Instance == null)
            return;

        // Build list of active cheats
        activeCheatNames.Clear();

        if (CheatManager.Instance.IsSuperSpeedActive())
            activeCheatNames.Add("✓ Super Speed");

        if (CheatManager.Instance.IsMegaJumpActive())
            activeCheatNames.Add("✓ Mega Jump");

        if (CheatManager.Instance.IsNoClipActive())
            activeCheatNames.Add("✓ No-Clip Mode");

        if (CheatManager.Instance.IsGodModeActive())
            activeCheatNames.Add("✓ God Mode");

        if (CheatManager.Instance.IsInfiniteShieldActive())
            activeCheatNames.Add("✓ Infinite Shield");

        if (CheatManager.Instance.IsSlowMotionActive())
            activeCheatNames.Add("✓ Slow Motion");

        if (CheatManager.Instance.IsGravityDisabled())
            activeCheatNames.Add("✓ No Gravity");

        // Update UI
        if (activeCheatsPanel != null)
        {
            activeCheatsPanel.SetActive(activeCheatNames.Count > 0);
        }

        if (activeCheatsHeader != null)
        {
            activeCheatsHeader.text = $"Active Cheats ({activeCheatNames.Count}):";
        }

        if (activeCheatsListText != null)
        {
            if (activeCheatNames.Count > 0)
            {
                activeCheatsListText.text = string.Join("\n", activeCheatNames);
            }
            else
            {
                activeCheatsListText.text = "None";
            }
        }
    }

    /// <summary>
    /// Show screen border effect
    /// </summary>
    private void ShowScreenBorder()
    {
        if (screenBorder != null)
        {
            screenBorder.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Hide all UI elements
    /// </summary>
    private void HideAllUI()
    {
        if (cheatModeIndicator != null)
            cheatModeIndicator.SetActive(false);

        if (activeCheatsPanel != null)
            activeCheatsPanel.SetActive(false);

        if (screenBorder != null)
            screenBorder.gameObject.SetActive(false);
    }
    #endregion

    #region Feedback System
    /// <summary>
    /// Handle cheat activation event
    /// </summary>
    private void HandleCheatActivated(string cheatName)
    {
        ShowCheatActivationFeedback(cheatName);
    }

    /// <summary>
    /// Show visual feedback when a cheat is activated
    /// </summary>
    public void ShowCheatActivationFeedback(string cheatName)
    {
        if (feedbackPanel != null && feedbackText != null)
        {
            feedbackPanel.SetActive(true);
            feedbackText.text = $"[CHEAT] {cheatName}";
            feedbackTimer = feedbackDuration;
            isFeedbackActive = true;
        }
    }

    /// <summary>
    /// Hide feedback panel
    /// </summary>
    private void HideFeedback()
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(false);
        }
        isFeedbackActive = false;
    }
    #endregion

    #region Visual Effects
    /// <summary>
    /// Animate border pulse effect
    /// </summary>
    private void AnimateBorderPulse()
    {
        if (screenBorder == null)
            return;

        float alpha = Mathf.Lerp(0.2f, 0.5f, (Mathf.Sin(Time.time * borderPulseSpeed) + 1f) / 2f);
        Color newColor = borderColor;
        newColor.a = alpha;
        screenBorder.color = newColor;
    }
    #endregion

    #region Public Helper Methods
    /// <summary>
    /// Manually trigger UI refresh
    /// </summary>
    public void RefreshUI()
    {
        UpdateUI();
    }

    /// <summary>
    /// Check if UI is currently visible
    /// </summary>
    public bool IsUIVisible()
    {
        return CheatManager.Instance != null && CheatManager.Instance.IsCheatModeEnabled();
    }
    #endregion
}

