using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    private bool _isInited;
    private CanvasGroup _group;
    private Sequence _sequence;
    private List<MessageBoxData> _dataList = new List<MessageBoxData>();
    private MessageBoxData _currentData;

    [SerializeField] private TextMeshProUGUI _yesText;
    [SerializeField] private TextMeshProUGUI _cancelText;

    private void Awake()
    
    {
        DOTween.defaultTimeScaleIndependent = true;
        Init();
    }

    private void Init()
    {
        if (_isInited)
            return;

        _isInited = true;

        //SceneManager.sceneLoaded += OnSceneLoaded;
        _group = GetComponent<CanvasGroup>();

        _sequence = DOTween.Sequence().SetAutoKill(false).Append(_group.DOFade(1f, 0.5f));
        
        _okButton.onClick.AddListener(OnClickOk);
        _cancelButton.onClick.AddListener(OnClickCancel);

        // _yesText = _okButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        // _cancelText = _cancelButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowMessageBox(string title, string message, bool useCancelButton = false, Action onClickOk = null,
        Action onClickCancel = null, string yesText = "Yes",string cancelText = "Cancel")
    {
        Init();
        _dataList.Add(
            new MessageBoxData()
            {
                Title = title,
                Message = message,
                UseCancelButton = useCancelButton,
                OnClickOk = onClickOk,
                OnClickCancel = onClickCancel,
                YesText = yesText,
                CancelText = cancelText
                //Tcs = null
            });
        if (gameObject.activeSelf == false)
            Show(Pop());
    }
    
    // public async UniTask<bool> ShowMessageBoxAsync(string title, string message, bool useCancelButton = false, Action onClickOk = null,  Action onClickCancel = null)
    // {
    //     Init();
    //     var tcs = new UniTaskCompletionSource<bool>();
    //     _dataList.Add(
    //         new MessageBoxData()
    //         {
    //             Title = title,
    //             Message = message,
    //             UseCancelButton = useCancelButton,
    //             OnClickOk = onClickOk,
    //             OnClickCancel = onClickCancel,
    //             Tcs = tcs
    //         });
    //     if(gameObject.activeSelf == false)
    //         Show(Pop());
    //     return await tcs.Task;
    // }
    
    
    private void Show(MessageBoxData data)
    {
        _currentData = data;

        if (_currentData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _titleText.text = _currentData.Title;
        _messageText.text = _currentData.Message;

        _yesText.text = _currentData.YesText;
        _cancelText.text = _currentData.CancelText;
        _cancelButton.gameObject.SetActive(_currentData.UseCancelButton);
        _group.alpha = 0f;


        DOVirtual.DelayedCall(2f, () =>
        {
            gameObject.SetActive(false);
        });
        
        _sequence.Restart();
    }
    
    private MessageBoxData Pop()
    {
        if (_dataList.Count == 0)
            return null;

        var value = _dataList[0];
        _dataList.RemoveAt(0);
        return value;
    }

    private void Next(bool isOk)
    {
        //_currentData?.Tcs?.TrySetResult(isOk);
        Show(Pop());
    }

    private void OnClickOk() // Unit unit
    {
        _currentData?.OnClickOk?.Invoke();
        Next(true);
    }

    private void OnClickCancel() // Unit unit
    {
        _currentData?.OnClickCancel?.Invoke();
        Next(false);
    }

    public void ClearAll()
    {
        _dataList.Clear();
        
        _currentData = null;
        
        gameObject.SetActive(false);
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     ClearAll();
    // }
    
}
