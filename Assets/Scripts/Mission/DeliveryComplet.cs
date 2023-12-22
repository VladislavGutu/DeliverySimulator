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
        _animator.SetTrigger("Open");
        _moneyText.text = money.ToString();
        _feedbackText.text = _feedbackTexts[stars];
        _starsImage.sprite = _starsSprites[stars];
        this.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_deliveryCompletButton);
    }
    
    public void CloseDeliveryComplet()
    {
        StartCoroutine(CloseDeliveryCompletCoroutine());
    }
    
    private IEnumerator CloseDeliveryCompletCoroutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
