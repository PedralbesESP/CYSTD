using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerState : PlayerState
{
    Vector3 _direction;
    bool _isGrounded;
    bool _hasPassedFirstFrame = false;
    bool _isOnJump = false;

    public JumpPlayerState(Vector3 direction)
    {
        _direction = direction;
    }

    public override void Start(GameObject go, Rigidbody rb, PlayerMovement pm)
    {
        base.Start(go, rb, pm);
        if (_IsGrounded())
        {
            _isGrounded = false;
            Vector3 upForce = _rigidbody.transform.up * _playerMovement.JumpForce;
            Vector3 dest = _direction + upForce;
            _rigidbody.AddForce(dest);
        }
        else
        {
            _isOnJump = true;
        }
    }

    public override void Update()
    {
        if (_hasPassedFirstFrame)
        {
            _isGrounded = _IsGrounded();
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
        if (_isGrounded || _isOnJump)
        {
            return new IdlePlayerState();
        }
        return null;
    }

    bool _IsGrounded()
    {
        return Physics.OverlapBox(_gameObject.transform.position, new Vector3(0.4f, 0.1f, 0.4f), Quaternion.identity, _playerMovement.WalkableLayer).Length > 0;
    }
}
