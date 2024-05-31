using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1; 
    public float parallaxSpeed = 0.25f; 

    private void Update()
    {
        float newPositionX = transform.position.x - parallaxSpeed * Time.deltaTime * depth;

      
        if (newPositionX <= -50)
        {
            newPositionX = 80; 
        }

        
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
}
