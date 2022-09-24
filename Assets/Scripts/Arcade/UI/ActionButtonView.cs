using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonView : MonoBehaviour
{
    [SerializeField]
    private bool _hasTimer;
    [SerializeField]
    private GameObject _cooldownContainer;
    [SerializeField]
    private Image _cooldownImage;
    [SerializeField]
    private TMP_Text _cooldownText;
    [SerializeField]
    private GameObject _readyContainer;

    private void Start()
    {
        _cooldownText.gameObject.SetActive(_hasTimer);
    }

    public void UpdateTimer(float timeLeft, float cooldownTime)
    {
        bool isReady = timeLeft <= float.Epsilon;
        _cooldownContainer.gameObject.SetActive(!isReady);
        _readyContainer.gameObject.SetActive(isReady);

        if (!isReady)
        {
            _cooldownImage.fillAmount = 1f - timeLeft / cooldownTime;
            _cooldownText.text = Mathf.Ceil(timeLeft).ToString();
        }
    }

    public void SetReady(bool isReady)
    {
        UpdateTimer(isReady ? 0f : 1f, 1f);
    }
}