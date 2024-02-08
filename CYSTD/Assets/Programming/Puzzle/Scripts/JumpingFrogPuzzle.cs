using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFrogPuzzle : Puzzle
{
    [SerializeField]
    GameObject _base;
    [SerializeField]
    InteractiveButton _button;
    [SerializeField]
    List<GameObject> _workingArms;
    bool _isMoving;
    float maxAngle;
    float minAngle;

    protected override void Start()
    {
        base.Start();
        _isMoving = false;
    }

    void Update()
    {
        if (_isMoving) Move();
    }

    void Move()
    {
        Rotate();
        MoveArms();
    }

    void Rotate()
    {
        
    }

    void MoveArms()
    {
        foreach (GameObject arm in _workingArms)
        {

        }
    }
}
