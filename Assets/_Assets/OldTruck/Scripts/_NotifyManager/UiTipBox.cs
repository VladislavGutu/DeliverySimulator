using DG.Tweening;
// using Facebook.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTipBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;

    private bool _isInited;
    private string _text;
    private CanvasGroup _group;
    private Sequence _sequence;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInited)
            return;

        _isInited = true;

        _group = GetComponent<CanvasGroup>();

        _sequence = DOTween.Sequence()
            .SetUpdate(true)
            .SetAutoKill(false)
            .Append(_group.DOFade(1f, 1f))
            .AppendInterval(2f)
            .Append(_group.DOFade(0f, 1f))
            .AppendCallback(() => { gameObject.SetActive(false); });
    }

    public void ShowTip(string tip)
    {
        Init();

        if (gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }

        _text = tip;

        _label.text = _text;
        _group.alpha = 0f;

        gameObject.SetActive(true);
        _sequence.Restart();
    }
}