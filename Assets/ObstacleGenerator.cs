using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<GameObject> obstacles;
    public GameObject coin;
    public GameObject jumpBoost; 

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
        float randomValue = Random.value;
        if (randomValue > 0.66)
        {
            GenerateObstacle();
        }
        else if (randomValue > 0.33)
        {
            GenerateCoin();
        }
        else
        {
            GenerateJumpBoost(); 
        }
    }

    public void GenerateObstacle()
    {
        if (obstacles.Count == 0)
        {
            Debug.LogError("No obstacles set in the obstacle list");
            return;
        }

        int randomIndex = Random.Range(0, obstacles.Count);
        GameObject selectedObstacle = obstacles[randomIndex];

        GameObject obstacleGameObject = Instantiate(selectedObstacle, transform.position, transform.rotation);
        obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this;
    }

    public void GenerateCoin()
    {
        GameObject coinObject = Instantiate(coin, transform.position, transform.rotation);
    }

    public void GenerateJumpBoost()
    {
        GameObject jumpBoostObject = Instantiate(jumpBoost, transform.position, transform.rotation);
    }

    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedMultiplier * Time.deltaTime;
        }
    }
}
