#if UNITY_SWITCH
using nn.hid;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Nintendo_Navigation : MonoBehaviour
{

    void Update()
    {
#if UNITY_SWITCH
        if (!NintendoInput.isActivInput)
            return;
#endif

        EventSystem eventSystem = EventSystem.current;
        AxisEventData data = new AxisEventData(eventSystem);

#if UNITY_SWITCH 
        if (NintendoInput.npadState.GetButtonDown(NpadButton.Up) || NintendoInput.npadState.GetButtonDown(NpadButton.StickLUp) || Input.GetKeyDown(KeyCode.W))
#else
        if (Input.GetKeyDown(KeyCode.W))
#endif
        {
            data.moveDir = MoveDirection.Up;
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.moveHandler);
        }
#if UNITY_SWITCH
        if (NintendoInput.npadState.GetButtonDown(NpadButton.Down) || NintendoInput.npadState.GetButtonDown(NpadButton.StickLDown) || Input.GetKeyDown(KeyCode.S))
#else
        if (Input.GetKeyDown(KeyCode.S))
#endif
        {
            data.moveDir = MoveDirection.Down;
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.moveHandler);
        }
#if UNITY_SWITCH
        if (NintendoInput.npadState.GetButtonDown(NpadButton.Left) || NintendoInput.npadState.GetButtonDown(NpadButton.StickLLeft) || Input.GetKeyDown(KeyCode.A))
#else
        if (Input.GetKeyDown(KeyCode.A))
#endif
        {
            data.moveDir = MoveDirection.Left;
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.moveHandler);
        }
#if UNITY_SWITCH
        if (NintendoInput.npadState.GetButtonDown(NpadButton.Right) || NintendoInput.npadState.GetButtonDown(NpadButton.StickLRight) || Input.GetKeyDown(KeyCode.D))
#else
        if (Input.GetKeyDown(KeyCode.D))
#endif
        {
            data.moveDir = MoveDirection.Right;
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.moveHandler);
        }

#if UNITY_SWITCH
        if (NintendoInput.npadState.GetButtonDown(NpadButton.A) || Input.GetKeyDown(KeyCode.Return))
#else
        if (Input.GetKeyDown(KeyCode.Return))
#endif
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
        }
#if UNITY_SWITCH
        if (NintendoInput.npadState.GetButtonDown(NpadButton.B) || Input.GetKeyDown(KeyCode.Backspace))
#else
        if (Input.GetKeyDown(KeyCode.Backspace))
#endif
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, new BaseEventData(eventSystem), ExecuteEvents.cancelHandler);
        }
    }
}
