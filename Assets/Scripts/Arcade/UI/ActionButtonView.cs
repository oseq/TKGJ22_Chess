using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonView : MonoBehaviour
{
    [SerializeField]
    private GameObject _cooldownContainer;
    [SerializeField]
    private Image _cooldownImage;
    [SerializeField]
    private TMP_Text _cooldownText;
    [SerializeField]
    private GameObject _readyContainer;

    public void UpdateTimer(float timeLeft, float cooldownTime)
    {
        bool isReady = timeLeft <= 0f;
        _cooldownContainer.gameObject.SetActive(!isReady);
        _readyContainer.gameObject.SetActive(isReady);

        if (!isReady)
        {
            _cooldownImage.fillAmount = timeLeft / cooldownTime;
            _cooldownText.text = timeLeft.ToString();
        }
    }
}