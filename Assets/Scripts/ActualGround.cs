using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualGround : MonoBehaviour
{
    public float groundHeight;

    void Start()
    {
        groundHeight = transform.position.y;
    }
}
