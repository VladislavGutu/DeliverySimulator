#if UNITY_SWITCH
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Switch;
using UnityEngine.UI;

public class SwitchSavePlayerPrefs : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialise()
    {
        GameObject main = new GameObject("SwitchSave_PlayerPrefs");
        main.AddComponent<SwitchSavePlayerPrefs>();
        DontDestroyOnLoad(main);
    }

    void OnEnable()
    {
#if UNITY_SWITCH && !UNITY_EDITOR
        Debug.Log("Init PlayerPrefs");
        PlayerPrefsSwitch.PlayerPrefsSwitch.Init();
#endif
        Notification.notificationMessageReceived += NotificationMessageHandler;
        Notification.EnterExitRequestHandlingSection();
        Notification.SetPerformanceModeChangedNotificationEnabled(true);
        Notification.SetResumeNotificationEnabled(true);
        Notification.SetOperationModeChangedNotificationEnabled(true);
        Notification.SetFocusHandlingMode(Notification.FocusHandlingMode.Notify);
    }

    private void Save()
    {
        PlayerPrefs.Save();
    }
    
    void RequestToClose()
    {
        Debug.Log("Attempting to close");
#if UNITY_SWITCH && !UNITY_EDITOR
        //Save the Game Data
        PlayerPrefsSwitch.PlayerPrefsSwitch.Term();
        //Say that we can leave the game
        Notification.LeaveExitRequestHandlingSection();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;        
#endif
    }

    void NotificationMessageHandler(Notification.Message message)
    {
        Debug.Log("Received Notifcation Handler: " + message.ToString());
        switch (message)
        {
            case Notification.Message.ExitRequest:
                // Notification to the program of an request to exit.
                // This notification is only sent if nn::oe::EnterExitRequestHandlingSection()
                // was called beforehand. The processes prior to existing are quickly performed, and
                // the application finally exits on the call to nn::oe::LeaveExitRequestHandlingSection().
                RequestToClose();
                break;

            case Notification.Message.FocusStateChanged:
                // Notifies when there has been a change to the program's focus state.
                // Depending on nn::oe::SetFocusHandlingMode(), the notification may not be sent.
                // var state = Notification.GetCurrentFocusState();
                // switch (state)
                // {
                //     case Notification.FocusState.InFocus:
                //         // In-focus state.
                //         break;
                //     case Notification.FocusState.OutOfFocus:
                //         // Out-of-focus state.
                //         break;
                //     case Notification.FocusState.Background:
                //         // BG focus state.
                //         break;
                // }
                Save();
                // Notification.SetResumeNotificationEnabled(false);
                break;
            case Notification.Message.Resume:
                // Notifies when the application resumes from a suspended state.
                // Depending on nn::oe::SetResumeNotificationEnabled(), the notification may not be sent.
                Save();
                break;
            case Notification.Message.OperationModeChanged:
                // Notifies when the operation mode has changed between handheld mode and TV mode.
                // You can get the current operation mode using the nn::oe::GetOperationMode() function.
                // Depending on nn::oe::SetOperationModeChangedNotificationEnabled(), the notification may not be sent.
                Save();
                break;
            case Notification.Message.PerformanceModeChanged:
                // Notifies when the performance mode has changed between Normal mode and Boost mode.
                // You can get the current performance mode using the nn::oe::GetPerformanceMode() function.
                // Depending on nn::oe::SetPerformanceModeChangedNotificationEnabled(), the notification may not be sent.
                Save();
                break;
            default:
                // Unknown messages will be ignored.
                Debug.LogError("Unhandled message");
                Save();
                break;
        }
    }
}
#endif