using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerState : PlayerState
{
    Vector3 _direction;
    bool _isGrounded;
    bool _hasPassedFirstFrame = false;

    public JumpPlayerState(Vector3 direction)
    {
        _direction = direction;
    }

    public override void Start(GameObject go, Rigidbody rb, PlayerMovement pm)
    {
        base.Start(go, rb, pm);
        if (LauchRays())
        {
            _isGrounded = false;
            Vector3 upForce = _rigidbody.transform.up * _playerMovement.JumpForce;
            Vector3 dest = _direction + upForce;
            _rigidbody.AddForce(dest);
        }
    }

    public override void Update()
    {
        if (_hasPassedFirstFrame)
        {
            _isGrounded = LauchRays();
            if (!_isGrounded && _rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity.SetY(_rigidbody.velocity.y * 10);
            }
        }
        else
        {
            _hasPassedFirstFrame = true;
        }
    }

    public override PlayerState CheckTransition()
    {
        if (_isGrounded)
        {
            return new IdlePlayerState();
        }
        return null;
    }

    bool LauchRays()
    {
        float x = (_gameObject.transform.localScale.x / 2) + 0.2f;
        float y = (_gameObject.transform.localScale.y / 2) + 0.1f;
        float z = (_gameObject.transform.localScale.z / 2) + 0.2f;

        (Vector3 direction, float length)[] rays = 
        {
            (Vector3.down, y),
            (new Vector3(x, -1, 0).normalized, Mathf.Sqrt(Mathf.Pow(y, 2) + Mathf.Pow(x, 2))),
            (new Vector3(0, -1, z).normalized, Mathf.Sqrt(Mathf.Pow(y, 2) + Mathf.Pow(z, 2)))
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (i == 0)
            {
                if (Physics.Raycast(_gameObject.transform.position, rays[i].direction, out _, rays[i].length + 0.1f))
                {
                    return true;
                }
            }
            else
            {
                if (Physics.Raycast(_gameObject.transform.position, rays[i].direction, out _, rays[i].length + 0.1f) ||
                    Physics.Raycast(_gameObject.transform.position, -rays[i].direction, out _, rays[i].length + 0.1f))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
