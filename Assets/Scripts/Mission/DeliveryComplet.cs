using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeliveryComplet : MonoBehaviour
{
    [SerializeField]
    private GameObject _deliveryCompletButton;
    [SerializeField]
    private TextMeshProUGUI _moneyText, _feedbackText;
    [SerializeField]
    private Image _starsImage;
    [SerializeField]
    private Sprite[] _starsSprites;
    [SerializeField]
    private string[] _feedbackTexts;
    
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }
    
    public void SetDeliveryComplet(int stars, int money)
    {
        this.gameObject.SetActive(true);
        _moneyText.text = money.ToString();
        _feedbackText.text = _feedbackTexts[stars];
        _starsImage.sprite = _starsSprites[stars];
        //_animator.SetTrigger("Open");
        Debug.LogError("SetDeliveryComplet");
        _animator.SetBool("isOpen", true);
        StartCoroutine(OpenDeliveryCompletCoroutine());
    }
    
    public void CloseDeliveryComplet()
    {
        Debug.LogError("CloseDeliveryComplet");
        StartCoroutine(CloseDeliveryCompletCoroutine());
    }
    
    private IEnumerator CloseDeliveryCompletCoroutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //_animator.SetTrigger("Close");
        _animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
    private IEnumerator OpenDeliveryCompletCoroutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(1f);
        EventSystem.current.SetSelectedGameObject(_deliveryCompletButton);
    }
}
