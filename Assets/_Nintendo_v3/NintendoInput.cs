#if UNITY_SWITCH
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nn.hid;
using System.Text;
using UnityEngine.EventSystems;

public static class NintendoInput
{
    public static bool isEditorInputActiv = false;
    
    enum NpadBTStyle
    {
        None,
        Down,
        Up,
    }

    internal enum NpadAxis
    {
        None,
        LeftHorizontal,
        LeftVertical,
        RightHorizontal,
        RightVertical,
    }


    public static NpadId[] npadIds = { NpadId.Handheld, NpadId.No1, NpadId.No2, NpadId.No3, NpadId.No4, NpadId.No5, NpadId.No6, NpadId.No7, NpadId.No8 };
    public static NpadId npadId = NpadId.Invalid;
    public static NpadStyle npadStyle = NpadStyle.Invalid;
    public static NpadStyle npadStyleTemp = NpadStyle.Invalid;
    public static NpadState npadState = new NpadState();
    public static ControllerSupportArg controllerSupportArg = new ControllerSupportArg();
    public static long[] preButtons;


    public static int playerCount = 1;
    public static bool isActivInput = true;

    
    private static HidNintendoInputSingle hid_NDI_Single;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void InitializeNintendoInput()
    {
        Debug.LogError("init -> NintendoInput");

        GameObject main = new GameObject("HidNintendoInputSingle");
        main.AddComponent<HidNintendoInputSingle>();
        main.AddComponent<UI_Nintendo_Navigation>();
        hid_NDI_Single = main.GetComponent<HidNintendoInputSingle>();
        GameObject.DontDestroyOnLoad(main);
#if !UNITY_EDITOR
        isEditorInputActiv = true;
#endif
    }


    
    //NintendoInput.InputNpadButton(NpadButton.X, playerNumber)
    internal static bool InputNpadButton(NpadButton npadButton, int player = 1)
    {
        return CheckInputNpadStyle(npadButton, player, NpadBTStyle.None);
    }

    internal static bool InputNpadButtonDown(NpadButton npadButton, int player = 1)
    {
        return CheckInputNpadStyle(npadButton, player, NpadBTStyle.Down);
    }

    internal static bool InputNpadButtonUp(NpadButton npadButton, int player = 1)
    {
        return CheckInputNpadStyle(npadButton, player, NpadBTStyle.Up);
    }
    
    internal static float InputNpadAxis(NpadAxis NpadAxis, int player = 1)
    {
        return CheckInputNpadStyleAxis(player, NpadAxis);
    }

    #region BackInput
    private static float CheckInputNpadStyleAxis(int player = 1, NpadAxis npadAxis = NpadAxis.None)
    {
        if (playerCount < player || !isActivInput)
            return 0;

        switch (npadAxis)
        {
            case NpadAxis.LeftHorizontal:
                return npadState.analogStickL.fx;
                break;
            case NpadAxis.LeftVertical:
                return npadState.analogStickL.fy;
                break;
            case NpadAxis.RightHorizontal:
                return npadState.analogStickR.fx;
                break;
            case NpadAxis.RightVertical:
                return npadState.analogStickR.fy;
                break;
            default:
                return 0;
                break;

        }
    }

    private static bool CheckInputNpadStyle(NpadButton npadButton, int player = 1, NpadBTStyle npadBTStyle = NpadBTStyle.None)
    {
        if (playerCount < player || !isActivInput)
            return false;
        

        #region NpadStyle JoyLeft/JoyRight
        //if (npadStyle == NpadStyle.JoyLeft)
        //{
        //    switch (npadButton)
        //    {
        //        case NpadButton.StickLLeft:
        //            npadButton = NpadButton.StickLUp;
        //            break;
        //        case NpadButton.StickLRight:
        //            npadButton = NpadButton.StickLDown;
        //            break;
        //        case NpadButton.ZR:
        //            npadButton = NpadButton.Down;
        //            break;
        //        case NpadButton.ZL:
        //            npadButton = NpadButton.Left;
        //            break;
        //        case NpadButton.X:
        //            npadButton = NpadButton.Right;
        //            break;
        //        case NpadButton.Plus:
        //            npadButton = NpadButton.Minus;
        //            break;
        //    }
        //}
        //else if (npadStyle == NpadStyle.JoyRight)
        //{
        //    switch (npadButton)
        //    {
        //        case NpadButton.StickLLeft:
        //            npadButton = NpadButton.StickRDown;
        //            break;
        //        case NpadButton.StickLRight:
        //            npadButton = NpadButton.StickRUp;
        //            break;
        //        case NpadButton.ZR:
        //            npadButton = NpadButton.X;
        //            break;
        //        case NpadButton.ZL:
        //            npadButton = NpadButton.A;
        //            break;
        //        case NpadButton.X:
        //            npadButton = NpadButton.Y;
        //            break;
        //    }
        //}
        #endregion

        if ((npadStyle == NpadStyle.FullKey) || (npadStyle == NpadStyle.JoyDual) || (npadStyle == NpadStyle.Handheld)
            || npadStyle == NpadStyle.JoyLeft || npadStyle == NpadStyle.JoyRight || controllerSupportArg.enableSingleMode)
        {
            switch (npadBTStyle)
            {
                case NpadBTStyle.None:
                    return npadState.GetButton(npadButton);
                    break;
                case NpadBTStyle.Down:
                    return npadState.GetButtonDown(npadButton);
                    break;
                case NpadBTStyle.Up:
                    return npadState.GetButtonUp(npadButton);
                    break;
                default:
                    return false;
                    break;
            }
        }
        else
            return false;
    }
    #endregion
}
#endif