using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class SummaryPanelController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _content;
    [SerializeField]
    private float _scaleUpAnimTime = 0.5f;
    [SerializeField]
    private float _scaleDownAnimTime = 0.5f;
    [SerializeField]
    private float _showTime = 1.5f;
    [SerializeField]
    private TMP_Text _summaryText;

    private string _textFormat;

    private void Awake()
    {
        _textFormat = _summaryText.text;
        _content.gameObject.SetActive(false);
    }

    public void ShowText(string whoWon)
    {
        _summaryText.text = string.Format(_textFormat, whoWon);
        _content.gameObject.SetActive(true);

        var sequence = DOTween.Sequence();
        _summaryText.transform.localScale = Vector3.zero;
        sequence.Append(_summaryText.transform.DOScale(1f, _scaleUpAnimTime).SetEase(Ease.OutBack));
        sequence.AppendInterval(_showTime);
        sequence.Append(_summaryText.transform.DOScale(0f, _scaleDownAnimTime).SetEase(Ease.InBack));
        sequence.AppendCallback(() => _content.gameObject.SetActive(false));
        sequence.SetUpdate(true);
    }
}