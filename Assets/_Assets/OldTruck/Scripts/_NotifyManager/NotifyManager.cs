using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotifyManager : MonoBehaviour
{
    [SerializeField] private UiTipBox _tipBox;
    [SerializeField] private UiMessageBox _messageBox;
    [SerializeField] private UiLoadingIndicator _loadingIndicator;
    [SerializeField] private GameObject _noMoneyOffer;
    [SerializeField] private CanvasGroup _group;
    private float _timer = 8;    
    
    public GameObject _finalPopUp;
    public Image _exit;

    public Animator _animator;
    
    private static NotifyManager _instance;
    public static NotifyManager Instance
    {
        get
        {
            if (_inited == false)
                _instance = FindObjectOfType<NotifyManager>();

            return _instance;
        }
    }
    private static bool _inited = false;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        _inited = true;
    }
    
    public void ShowTip(string tip)
    {
        _tipBox.ShowTip(tip);
    }



    public void ShowMessageBox(string title, string message, bool useCancelButton = false, Action onClickOk = null,
        Action onClickCancel = null, string yesText = "OK",string cancelText = "Cancel")
    {
        _messageBox.ShowMessageBox(title, message, useCancelButton, onClickOk, onClickCancel,yesText,cancelText);
    }

    // public UniTask<bool> ShowMessageBoxAsync(string title, string message, bool useCancelButton = false, Action onClickOk = null, Action onClickCancel = null)
    // {
    //     return _messageBox.ShowMessageBoxAsync(title, message, useCancelButton, onClickOk, onClickCancel);
    // }

    public void ClearAllMessageBox()
    {
        _messageBox.ClearAll();
    }

    public Task ShowLoadingIndicator(Func<Task> action)
    {
        return _loadingIndicator.ShowLoadingIndicator(action);
    }

    public void ShowLoadingIndicator(bool show)
    {
        _loadingIndicator.ShowLoadingIndicator(show);
    }

    public void ShowNoMoneyOffer()
    {
        _noMoneyOffer.SetActive(true);
        _animator.SetTrigger("StartAnim");
    }

    private void GroupFadeIn()
    {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;
        _group.DOFade(1f, 0.5f);
    }
    
    public void OnClosePopUp()
    {
        // _group.DOFade(0f, 0.5f);
        // DOVirtual.DelayedCall(.5f, () =>
        // {
            _noMoneyOffer.SetActive(false);
            _finalPopUp.SetActive(false);
            
            var tempColor = _exit.color;
            tempColor.a = 0;
            _exit.color = tempColor;

        // });
        //AudioManager.Instance.Play("ClickSound");
    }
    
}
