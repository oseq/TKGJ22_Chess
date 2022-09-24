using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector3> OnPerformSkillClicked = delegate { };
    public event Action<Vector3> OnPerformPowerUpClicked = delegate { };

    [SerializeField]
    private string _horizontalAxisName;
    [SerializeField]
    private string _verticalAxisName;
    [SerializeField]
    private KeyCode _actionBtnA;
    [SerializeField]
    private KeyCode _actionBtnB;
    [SerializeField]
    private Vector3 _initialInputDirection;

    private Vector3 _lastInputDirection;

    private void Awake()
    {
        _lastInputDirection = _initialInputDirection;
    }

    private void Update()
    {
        CheckInputs();
    }

    public Vector3 GetLastInputDirection()
    {
        var currentDir = new Vector3(Input.GetAxis(_horizontalAxisName), 0f, Input.GetAxis(_verticalAxisName));
        if (Mathf.Abs(currentDir.x) > float.Epsilon || Mathf.Abs(currentDir.z) > float.Epsilon)
        {
            _lastInputDirection = currentDir;
        }
        return _lastInputDirection;
    }

    public Vector3 GetRealInputDirection()
    {
        var currentDir = new Vector3(Input.GetAxis(_horizontalAxisName), 0f, Input.GetAxis(_verticalAxisName));
        if (Mathf.Abs(currentDir.x) > float.Epsilon || Mathf.Abs(currentDir.z) > float.Epsilon)
        {
            _lastInputDirection = currentDir;
        }
        return currentDir;
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(_actionBtnA))
        {
            TryToPerformSkill(GetLastInputDirection());
        }

        if (Input.GetKeyDown(_actionBtnB))
        {
            TryToPerformPowerUp(GetLastInputDirection());
        }
    }

    private void TryToPerformSkill(Vector3 inputDirection)
    {
        OnPerformSkillClicked?.Invoke(inputDirection);
    }

    private void TryToPerformPowerUp(Vector3 inputDirection)
    {
        OnPerformPowerUpClicked?.Invoke(inputDirection);
    }
}