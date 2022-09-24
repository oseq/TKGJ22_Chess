using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PossibleDirections
{
    Straight,
    Diagonal,
    All
}


public class SpecialActionController : MonoBehaviour
{
    public Vector3 _direction;
    public float   _velocity;
    public bool    _isJump;
    public Rigidbody _rb;
    public KeyCode _button;
    private PossibleDirections _possibleDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        checkSpecialAction();

    }

    private void checkSpecialAction()
    {
        if (Input.GetKeyDown(_button))
        {
            SpecialAction();
        }
    }

    public void SpecialAction()
    {
        float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(_direction.x, _direction.y, 0.0f));
        if (_direction.x < 0.0f)
        {
            angle = -angle;
            angle = angle + 360;
        }


        switch (_possibleDirection) {
            case PossibleDirections.Straight:

                break;
            case PossibleDirections.Diagonal:

                break;
            case PossibleDirections.All:

                break;
        }
        _rb.AddForce(_direction.normalized * _velocity * Time.deltaTime, ForceMode.Impulse);
        _rb.velocity = _direction.normalized * _velocity;
    }

    public Vector3 getActionDirection()
    {
        return new Vector3();
    }

}
