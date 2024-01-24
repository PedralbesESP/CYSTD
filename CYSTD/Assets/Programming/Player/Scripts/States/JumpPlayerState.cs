using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerState : PlayerState
{
    Vector3 _direction;
    bool _isGrounded;
    bool _hasPassedFirstFrame = false;
    bool _isOnJumpAtStart = false;

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
            pm.StartCoroutine(WaitJump());
        }
        else
        {
            _isOnJumpAtStart = true;
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
    }

    IEnumerator WaitJump()
    {
        yield return new WaitForSecondsRealtime(.2f);
        _hasPassedFirstFrame = true;
    }

    public override PlayerState CheckTransition()
    {
        if (_isGrounded || _isOnJumpAtStart)
        {
            //Debug.Log("IsGROUNDED");
            if (_playerMovement.Direction != Vector3.zero)
            {
                if (_playerMovement.IsRunning)
                {
                    return new RunPlayerState();
                }
                if (_playerMovement.IsCrouching)
                {
                    return new CrouchPlayerState();
                }
                return new WalkPlayerState();
            }
            return new IdlePlayerState();
        }
        return null;
    }

    bool _IsGrounded()
    {
        return Physics.OverlapBox(_gameObject.transform.position, new Vector3(0.4f, 0.1f, 0.4f), Quaternion.identity, _playerMovement.WalkableLayer).Length > 0;
    }
}
