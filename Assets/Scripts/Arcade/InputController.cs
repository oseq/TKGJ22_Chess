using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private string _horizontalAxisName;
    [SerializeField]
    private string _verticalAxisName;
    [SerializeField]
    private KeyCode _actionBtnA;
    [SerializeField]
    private KeyCode _actionBtnB;

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(_actionBtnA))
        {
            //peform skill action
            TryToPerformSkill(GetInputDirection());
        }

        if (Input.GetKeyDown(_actionBtnB))
        {
            //perform power-up action
            TryToPerformPowerUp(GetInputDirection());
        }

        Vector3 GetInputDirection() => new Vector3(Input.GetAxis(_horizontalAxisName), 0f, Input.GetAxis(_verticalAxisName));
    }

    private void TryToPerformSkill(Vector3 inputDirection)
    {
        //call skill
    }

    private void TryToPerformPowerUp(Vector3 inputDirection)
    {
        //call power-up
    }
}