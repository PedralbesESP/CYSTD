using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrousselPuzzle : Puzzle
{
    [SerializeField]
    GameObject _base;
    [SerializeField]
    InteractiveButton _leftButton;
    [SerializeField]
    InteractiveButton _rightButton;
    [SerializeField]
    List<BoxCollider> pointsToReach;
    float _currentPoint = 0;

    void Rotate()
    {
        
    }
}
