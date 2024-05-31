using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoxPosition : MonoBehaviour
{
    public GameObject boxToFollow; 


    void Update()
    {
        
        transform.position = boxToFollow.transform.position;
    }
}
