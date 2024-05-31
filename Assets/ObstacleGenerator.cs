using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject coin; 

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;

    public float speedMultiplier;

    void Start()
    {
        currentSpeed = minSpeed;
        GenerateObstacle(); 
    }

    public void GenerateObstacleWithGap()
    {
        float randomGap = Random.Range(0.01f, 2f);
        Invoke("GenerateObstacleOrCoin", randomGap); 
    }

    public void GenerateObstacleOrCoin() 
    {
        if (Random.value > 0.5) 
        {
            GenerateObstacle();
        }
        else 
        {
            GenerateCoin();
        }
    }

    public void GenerateObstacle()
    {
        GameObject obstacleGameObject = Instantiate(obstacle, transform.position, transform.rotation);
        obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this; 
    }

    public void GenerateCoin() 
    {
        GameObject coinObject = Instantiate(coin, transform.position, transform.rotation);
        
    }

    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedMultiplier * Time.deltaTime;
        }
    }
}
