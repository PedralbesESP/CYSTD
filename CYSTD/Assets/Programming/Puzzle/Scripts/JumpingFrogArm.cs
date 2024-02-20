using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFrogArm : MonoBehaviour
{
    [SerializeField] bool _movingUp;
    float _minAngle;
    float _maxAngle;
    float _currentAngle;
    float _forceScaleFactor;

    public void Init(float min, float max, float forceScaleFactor)
    {
        _currentAngle = transform.localEulerAngles.x;
        _minAngle = min;
        _maxAngle = max;
        _forceScaleFactor = forceScaleFactor;
    }

    public void Move(float speed)
    {
        if (_movingUp)
        {
            if (_currentAngle <= _maxAngle)
            {
                _currentAngle = Mathf.Clamp(_currentAngle + speed * Time.deltaTime, _minAngle, _maxAngle);
                transform.Rotate(Vector3.right, speed * Time.deltaTime);
                if (_currentAngle >= _maxAngle)
                {
                    _movingUp = false;
                    transform.localEulerAngles.Set(_maxAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
                }
            }
        }
        else
        {
            if (_currentAngle >= _minAngle)
            {
                _currentAngle = Mathf.Clamp(_currentAngle - speed * Time.deltaTime, _minAngle, _maxAngle);
                transform.Rotate(Vector3.right, -speed * Time.deltaTime);
                if (_currentAngle <= _minAngle)
                {
                    _movingUp = true;
                    transform.localEulerAngles.Set(_minAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out PlayerMovement pm)) pm.ChangeScaleFactor(_forceScaleFactor);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out PlayerMovement pm)) pm.ChangeScaleFactor(-_forceScaleFactor);
    }
}