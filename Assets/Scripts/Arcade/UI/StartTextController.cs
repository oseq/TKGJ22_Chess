using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class StartTextController : MonoBehaviour
{
    [SerializeField]
    private string[] _textsToShowArray;
    [SerializeField]
    private float _scaleUpAnimTime = 0.2f;
    [SerializeField]
    private float _scaleDownAnimTime = 0.8f;
    [SerializeField]
    private float _maxScale = 1.5f;

    private TMP_Text _startText;

    private void Start()
    {
        _startText = GetComponent<TMP_Text>();

        if (_textsToShowArray.Length == 0)
            return;

        ShowText();
    }

    private void ShowText()
    {
        _startText.text = _textsToShowArray[0];

        var sequence = DOTween.Sequence();
        _startText.transform.localScale = Vector3.zero;
        foreach (string nextText in _textsToShowArray)
        {
            sequence.Append(_startText.transform.DOScale(_maxScale, _scaleUpAnimTime).SetEase(Ease.OutQuad));
            sequence.AppendCallback(() => _startText.text = nextText);
            sequence.Append(_startText.transform.DOScale(1f, _scaleDownAnimTime));
        }
        sequence.Append(_startText.transform.DOScale(0f, _scaleUpAnimTime));
    }
}