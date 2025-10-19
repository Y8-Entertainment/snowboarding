using System;
using UnityEngine;

/// <summary>
/// CheatManager - Central hub for all cheat functionality
/// Provides developer tools for testing and debugging gameplay
/// 
/// Master Toggle: CTRL+C (Enable/Disable cheat system)
/// All cheats require cheat mode to be enabled first
/// </summary>
public class CheatManager : MonoBehaviour
{
    #region Singleton
    public static CheatManager Instance { get; private set; }
    #endregion

    #region Configuration
    [Header("Debug Settings")]
    [SerializeField] private bool debugMode = true;
    #endregion

    #region Cheat State
    private bool cheatModeEnabled = false;

    // Movement Cheats
    private bool isSuperSpeedActive = false;
    private bool isMegaJumpActive = false;
    private bool isNoClipActive = false;
    #endregion

    #region Cheat Multipliers
    [Header("Movement Cheat Settings")]
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float jumpMultiplier = 3f;
    #endregion

    #region Events
    /// <summary>
    /// Event fired when any cheat is activated or deactivated
    /// </summary>
    public event Action<string> OnCheatActivated;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        // Singleton pattern with persistence across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LogCheat("CheatManager initialized and will persist across scenes");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Master cheat toggle - CTRL+C
        if (IsControlPressed() && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCheatMode();
        }

        // Only process other cheats if cheat mode is enabled
        if (!cheatModeEnabled)
            return;

        // === MOVEMENT CHEATS ===
        // CTRL+SHIFT+S: Super Speed
        if (IsCheatKeyPressed(KeyCode.S))
        {
            ToggleSuperSpeed();
        }

        // CTRL+SHIFT+J: Mega Jump
        if (IsCheatKeyPressed(KeyCode.J))
        {
            ToggleMegaJump();
        }

        // CTRL+SHIFT+N: No-Clip Mode
        if (IsCheatKeyPressed(KeyCode.N))
        {
            ToggleNoClip();
        }
    }
    #endregion

    #region Master Cheat Toggle
    /// <summary>
    /// Toggle the master cheat mode on/off
    /// All individual cheats require this to be enabled
    /// </summary>
    public void ToggleCheatMode()
    {
        cheatModeEnabled = !cheatModeEnabled;
        
        string status = cheatModeEnabled ? "ON" : "OFF";
        LogCheat($"=== CHEAT MODE: {status} ===");
        
        // Notify listeners
        NotifyCheatActivated($"Cheat Mode {status}");
        
        // Visual/audio feedback can be added here in future commits
    }

    /// <summary>
    /// Check if cheat mode is currently enabled
    /// </summary>
    public bool IsCheatModeEnabled()
    {
        return cheatModeEnabled;
    }
    #endregion

    #region Key Detection Utilities
    /// <summary>
    /// Check if Control key is pressed (left or right)
    /// </summary>
    private bool IsControlPressed()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    /// <summary>
    /// Check if Shift key is pressed (left or right)
    /// </summary>
    private bool IsShiftPressed()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    /// <summary>
    /// Check if a cheat key combination is pressed (CTRL+SHIFT+key)
    /// </summary>
    /// <param name="key">The specific key to check</param>
    /// <returns>True if the full combination is pressed</returns>
    private bool IsCheatKeyPressed(KeyCode key)
    {
        // Only process if cheat mode is enabled
        if (!cheatModeEnabled)
            return false;

        return IsControlPressed() && IsShiftPressed() && Input.GetKeyDown(key);
    }
    #endregion

    #region Event System
    /// <summary>
    /// Notify all listeners that a cheat has been activated
    /// </summary>
    /// <param name="cheatName">Name of the cheat that was activated</param>
    private void NotifyCheatActivated(string cheatName)
    {
        OnCheatActivated?.Invoke(cheatName);
    }
    #endregion

    #region Debug System
    /// <summary>
    /// Log cheat-related messages to console (only if debug mode is enabled)
    /// </summary>
    /// <param name="message">Message to log</param>
    private void LogCheat(string message)
    {
        if (debugMode)
        {
            Debug.Log($"[CHEAT] {message}");
        }
    }
    #endregion

    #region Movement Cheats
    /// <summary>
    /// Toggle Super Speed cheat (2x movement speed)
    /// Key: CTRL+SHIFT+S
    /// </summary>
    public void ToggleSuperSpeed()
    {
        isSuperSpeedActive = !isSuperSpeedActive;
        
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            if (isSuperSpeedActive)
            {
                player.SetSpeedMultiplier(speedMultiplier);
                LogCheat($"Super Speed ACTIVATED (x{speedMultiplier})");
            }
            else
            {
                player.SetSpeedMultiplier(1f);
                LogCheat("Super Speed DEACTIVATED");
            }
            
            NotifyCheatActivated($"Super Speed {(isSuperSpeedActive ? "ON" : "OFF")}");
        }
        else
        {
            LogCheat("ERROR: PlayerController not found!");
        }
    }

    /// <summary>
    /// Toggle Mega Jump cheat (3x jump force)
    /// Key: CTRL+SHIFT+J
    /// </summary>
    public void ToggleMegaJump()
    {
        isMegaJumpActive = !isMegaJumpActive;
        
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            if (isMegaJumpActive)
            {
                player.SetJumpMultiplier(jumpMultiplier);
                LogCheat($"Mega Jump ACTIVATED (x{jumpMultiplier})");
            }
            else
            {
                player.SetJumpMultiplier(1f);
                LogCheat("Mega Jump DEACTIVATED");
            }
            
            NotifyCheatActivated($"Mega Jump {(isMegaJumpActive ? "ON" : "OFF")}");
        }
        else
        {
            LogCheat("ERROR: PlayerController not found!");
        }
    }

    /// <summary>
    /// Toggle No-Clip mode (fly through objects)
    /// Key: CTRL+SHIFT+N
    /// </summary>
    public void ToggleNoClip()
    {
        isNoClipActive = !isNoClipActive;
        
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            player.SetNoClipMode(isNoClipActive);
            LogCheat($"No-Clip Mode {(isNoClipActive ? "ACTIVATED" : "DEACTIVATED")}");
            NotifyCheatActivated($"No-Clip {(isNoClipActive ? "ON" : "OFF")}");
        }
        else
        {
            LogCheat("ERROR: PlayerController not found!");
        }
    }

    /// <summary>
    /// Check if super speed is currently active
    /// </summary>
    public bool IsSuperSpeedActive()
    {
        return isSuperSpeedActive;
    }

    /// <summary>
    /// Check if mega jump is currently active
    /// </summary>
    public bool IsMegaJumpActive()
    {
        return isMegaJumpActive;
    }

    /// <summary>
    /// Check if no-clip mode is currently active
    /// </summary>
    public bool IsNoClipActive()
    {
        return isNoClipActive;
    }
    #endregion
}

