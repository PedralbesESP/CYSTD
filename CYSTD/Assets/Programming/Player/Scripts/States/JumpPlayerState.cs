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
        if (_IsGrounded())
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
        if (_isGrounded)
        {
            return new IdlePlayerState();
        }
        return null;
    }

    bool _IsGrounded()
    {
        return Physics.OverlapSphere(_gameObject.transform.position, 0.2f, _playerMovement.WalkableLayer).Length > 0;
    }
}
