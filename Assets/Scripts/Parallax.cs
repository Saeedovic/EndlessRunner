using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1; 
    public float parallaxSpeed = 0.25f; 
    UIController uiController;

    public void Awake()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();

    }
    private void Update()
    {
        if (!uiController.ready) return;

        float newPositionX = transform.position.x - parallaxSpeed * Time.deltaTime * depth;

      
        if (newPositionX <= -5)
        {
            newPositionX = 50; 
        }

        
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
}
