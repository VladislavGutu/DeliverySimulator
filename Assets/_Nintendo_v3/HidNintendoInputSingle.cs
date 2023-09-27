#if UNITY_SWITCH
using UnityEngine;
using nn.hid;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using UnityEngine.Windows;
using UnityEngine.EventSystems;

///Rewired
//using Rewired;
//

public class HidNintendoInputSingle : MonoBehaviour
{
    private nn.Result result = new nn.Result();
    private GameObject _CanvasWarning;
    private bool _isActivatePP = false;
    private bool _isSwap = false;

    ///Rewired
    //public int playerId;
    //private Player player;
    //

    void Start()
    {
        ///Rewired
        //player = ReInput.players.GetPlayer(playerId);
        //

        Npad.Initialize();
        NpadJoy.SetHoldType(NpadJoyHoldType.Horizontal);
        Npad.SetSupportedIdType(NintendoInput.npadIds);
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
        NintendoInput.preButtons = new long[1];

        if (NintendoInput.npadStyleTemp == NpadStyle.Invalid || Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
        {
            if (Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
            {
                NintendoInput.npadStyle = Npad.GetStyleSet(NpadId.No1);
                NintendoInput.npadId = NpadId.No1;
            }
            else
            {
                NintendoInput.npadStyle = NpadStyle.Handheld;
                NintendoInput.npadId = NpadId.Handheld;
            }
            NintendoInput.npadStyleTemp = NintendoInput.npadStyle;
        }

    }

    void Update()
    {
        NpadButton onButtons = 0;

        if (UpdatePadState())
        {
            NpadId npadIds = NintendoInput.npadId;
            NpadStyle npadStyle = Npad.GetStyleSet(npadIds);
            Npad.GetState(ref NintendoInput.npadState, npadIds, npadStyle);

            onButtons |= ((NpadButton)NintendoInput.preButtons[0] ^ NintendoInput.npadState.buttons) & NintendoInput.npadState.buttons;
            NintendoInput.preButtons[0] = (long)NintendoInput.npadState.buttons;

            NpadStateUP();
        }
        else
        {
            for (int i = 0; i < NintendoInput.npadIds.Length; i++)
            {
                NpadId npadId = NintendoInput.npadIds[i];
                NpadStyle npadStyle = Npad.GetStyleSet(npadId);
                if (npadStyle == NpadStyle.None) { continue; }

                Npad.GetState(ref NintendoInput.npadState, npadId, npadStyle);

                onButtons |= ((NpadButton)NintendoInput.preButtons[0] ^ NintendoInput.npadState.buttons) & NintendoInput.npadState.buttons;
                NintendoInput.preButtons[0] = (long)NintendoInput.npadState.buttons;

                StartCoroutine(ShowPupupGame());
            }
        }

        if (NintendoInput.npadStyle == NpadStyle.Invalid || Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
        {
            if (Npad.GetStyleSet(NpadId.No1) != NpadStyle.None)
            {
                NintendoInput.npadStyle = Npad.GetStyleSet(NpadId.No1);
                NintendoInput.npadId = NpadId.No1;
            }
            else
            {
                NintendoInput.npadStyle = NpadStyle.Handheld;
                NintendoInput.npadId = NpadId.Handheld;
            }

            NintendoInput.npadStyleTemp = NintendoInput.npadStyle;
        }

    }
    private void NpadStateUP()
    {
        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = NintendoInput.npadState;
        Npad.GetState(ref no1State, NpadId.No1, no1Style);

        if ((NintendoInput.npadStyleTemp != NintendoInput.npadStyle && NintendoInput.npadStyle == NpadStyle.Handheld && NintendoInput.npadStyleTemp != NpadStyle.JoyDual && !_isSwap)
            || (NintendoInput.npadStyle == NpadStyle.Handheld && NintendoInput.npadStyleTemp != no1Style && no1Style == NpadStyle.FullKey && !_isSwap)
            || Npad.GetStyleSet(NpadId.No2) != NpadStyle.None && !_isSwap
            || NintendoInput.npadStyle == NpadStyle.Handheld && no1Style == NpadStyle.JoyDual && !_isSwap)
        {
            _isSwap = true;
            StopInput();
            ShowControllerSupport();
        }
        else
        {
            StartCoroutine(ShowPupupGame());

            if (no1Style == NpadStyle.FullKey)
                NintendoInput.npadStyle = NpadStyle.FullKey;
            else if (no1Style == NpadStyle.JoyDual)
                NintendoInput.npadStyle = NpadStyle.JoyDual;
            else
                NintendoInput.npadStyle = NpadStyle.Handheld;

            NintendoInput.npadStyleTemp = NintendoInput.npadStyle;

            if (NintendoInput.npadStyle == NpadStyle.Handheld)
                Npad.Disconnect(NpadId.No1);

            if (_isSwap)
                StartInput();

            _isSwap = false;
        }

    }
    private bool UpdatePadState()
    {

        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = NintendoInput.npadState;
        if (handheldStyle != NpadStyle.None && NintendoInput.npadStyleTemp != handheldStyle)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                NintendoInput.npadId = NpadId.Handheld;
                NintendoInput.npadStyle = handheldStyle;
                NintendoInput.npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = NintendoInput.npadState;
        if (no1Style != NpadStyle.None && NintendoInput.npadStyleTemp != no1Style)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                NintendoInput.npadId = NpadId.No1;
                NintendoInput.npadStyle = no1Style;
                NintendoInput.npadState = no1State;
                return true;
            }
        }

        if ((NintendoInput.npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {
            NintendoInput.npadId = NpadId.Handheld;
            //npadStyle = handheldStyle;
            NintendoInput.npadState = handheldState;
        }
        else if ((NintendoInput.npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {
            NintendoInput.npadId = NpadId.No1;
            //npadStyle = no1Style;
            NintendoInput.npadState = no1State;
        }
        else
        {
            NintendoInput.npadId = NpadId.Invalid;
            NintendoInput.npadStyle = NpadStyle.Invalid;
            NintendoInput.npadState.Clear();
            return false;
        }
        return true;
    }


    void ShowControllerSupport()
    {
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
        NintendoInput.controllerSupportArg.SetDefault();
        NintendoInput.controllerSupportArg.playerCountMax = 1;
        NintendoInput.controllerSupportArg.playerCountMin = 0; //must be set to 0 if you want to allow someone to play in handheld mode only
        NintendoInput.controllerSupportArg.enablePermitJoyDual = true;
        //controllerSupportArg.enableTakeOverConnection = false;
        NintendoInput.controllerSupportArg.enableSingleMode = true; // set to false to remove handheld image from applet

        Debug.Log(NintendoInput.controllerSupportArg);
        UnityEngine.Switch.Applet.Begin();
        result = ControllerSupport.Show(NintendoInput.controllerSupportArg);
        UnityEngine.Switch.Applet.End();
        if (!result.IsSuccess()) { Debug.Log(result); }
    }

    IEnumerator ShowPupupGame()
    {
        if (((NintendoInput.npadState.attributes & NpadAttribute.IsLeftConnected) != 0) != ((NintendoInput.npadState.attributes & NpadAttribute.IsRightConnected) != 0))
        {
            StopInput();
            if (!_isActivatePP)
            {
                _isActivatePP = true;

                yield return new WaitForSecondsRealtime(1f);
                ///Create Info Panel
                CreatePanel();
                //

                if (((NintendoInput.npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((NintendoInput.npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(1f);
                if (((NintendoInput.npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((NintendoInput.npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(1f);
                if (((NintendoInput.npadState.attributes & NpadAttribute.IsLeftConnected) != 0) == ((NintendoInput.npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                    DestroyOBJ();

                yield return new WaitForSecondsRealtime(1f);
                DestroyOBJ();
                if (((NintendoInput.npadState.attributes & NpadAttribute.IsLeftConnected) != 0) != ((NintendoInput.npadState.attributes & NpadAttribute.IsRightConnected) != 0))
                {
                    Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
                    ShowControllerSupport();
                }
            }
        }
    }
    void CreatePanel()
    {
#if true
        _CanvasWarning = new GameObject("Canvas");
        Canvas c = _CanvasWarning.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        _CanvasWarning.AddComponent<CanvasScaler>();
        _CanvasWarning.AddComponent<GraphicRaycaster>();
        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Text i = panel.AddComponent<Text>();
        i.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        i.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        i.text = "The Joy-Con grip accessory is recommended when playing.";
        i.alignment = TextAnchor.MiddleCenter;
        i.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        i.fontSize = 20;
        panel.transform.SetParent(_CanvasWarning.transform, false);
#endif
    }

    void DestroyOBJ()
    {
        _isActivatePP = false;
        StartInput();

        ///Destroy Info Panel
        Destroy(_CanvasWarning);
    }

    private GameObject tempGO;

    void StartInput()
    {
        ///Pornirea Inputului
        ///Test
        Debug.LogError("StartInput");
        //

        //NintendoInput
        NintendoInput.isActivInput = true;
        //

        ///Rewired
        //player.controllers.maps.SetMapsEnabled(true, 0);
        //

        ///Unity EventSystem
        //FindObjectOfType<StandaloneInputModule>().enabled = true;
        //FindObjectOfType<EventSystem>().enabled = true;
        //EventSystem.current.SetSelectedGameObject(tempGO);
        //
    }

    void StopInput()
    {
        ///Oprirea Inputului
        ///Test
        Debug.LogError("StopInput");
        //

        //NintendoInput
        NintendoInput.isActivInput = false;
        //

        ///Rewired
        //player.controllers.maps.SetMapsEnabled(false, 0);
        //

        ///Unity EventSystem
        //tempGO = EventSystem.current.currentSelectedGameObject;
        //FindObjectOfType<EventSystem>().enabled = false;
        //FindObjectOfType<StandaloneInputModule>().enabled = false;
        //
    }
}
#endif