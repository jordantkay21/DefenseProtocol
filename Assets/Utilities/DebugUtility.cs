using System;
using System.Collections.Generic;
using UnityEngine;

public class EnumFlagsAttribute : PropertyAttribute
{

}

[Flags]
public enum DebugTag
{
    None = 0,
    PlayerController = 1 << 0,
    InteractionSystem = 1 << 1,
    WeaponSystem = 1 << 2,
    Pistol = 1 << 3,
    All = ~0 // This enables all flags
}

public static class DebugUtility
{
    //Toggle this flag to enable/disable debug logging
    public static bool IsDebugMode { get; set; } = true;

    //Set of enabled tags
    private static HashSet<DebugTag> enabledTags = new HashSet<DebugTag>();

    /// <summary>
    /// Enable a specific tag
    /// </summary>
    /// <param name="tag">Tag to enable</param>
    public static void EnableTag(DebugTag tag)
    {
        if (!enabledTags.Contains(tag))
            enabledTags.Add(tag);
    }

    /// <summary>
    /// Disable a specific tag
    /// </summary>
    /// <param name="tag">Tag to disable</param>
    public static void DisableTag(DebugTag tag)
    {
        if (enabledTags.Contains(tag))
            enabledTags.Remove(tag);
    }

    /// <summary>
    /// Logs a debug message with an optional tag
    /// </summary>
    /// <param name="message"></param>
    public static void Log(DebugTag tag, string message)
    {
        if (IsDebugMode && enabledTags.Contains(tag))
            Debug.Log($"[{tag}] {message}");
    }

    /// <summary>
    /// Trigger logs based on specific game events
    /// </summary>
    /// <typeparam name="TEvent">Event that will trigger log</typeparam>
    /// <param name="eventArgs">data passed from the event</param>
    /// <param name="message">message to display when event is triggered</param>
    public static void LogOnEvent<TEvent>(TEvent eventArgs, DebugTag tag, string message)
    {
        if (IsDebugMode && enabledTags.Contains(tag))
        {
            //Log message when the event occurs
            Debug.Log($"[{tag}] Event Triggered: {typeof(TEvent).Name} - {message}");
        }
    }

    public static void ClearTags()
    {
        enabledTags.Clear();
    }
}
