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
    bool doneReady = false;

    public UIController uiController;

    public float speedMultiplier;

    private float obstacleCooldown = .7f;
    private float coinCooldown = 0f;
    private float generationInterval = 2f;

    public float minYPosition = 0.5f;
    public float minGapBetweenObstacles = 5f;

    void Awake()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void Update()
    {
        if (!uiController.ready) return;


        if (doneReady == true)
        {
            currentSpeed = minSpeed;
            GenerateObstacle();
            doneReady = false;

        }

        if (doneReady == false)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += speedMultiplier * Time.deltaTime;
            }

            obstacleCooldown -= Time.deltaTime;
            coinCooldown -= Time.deltaTime;

            if (obstacleCooldown <= 0)
            {
                GenerateObstacle();
                obstacleCooldown = generationInterval + Random.Range(1f, 3f);
            }

            if (coinCooldown <= 0)
            {
                GenerateRandomItem();
                coinCooldown = generationInterval + Random.Range(1f, 3f);
            }

        }
    }

    public void GenerateRandomItem()
    {
        float randomValue = Random.value;
        if (randomValue > 0.5)
        {
            GenerateCoinPattern();
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

        Vector3 spawnPosition = transform.position;
        if (IsSpaceAvailable(spawnPosition, minGapBetweenObstacles))
        {
            GameObject obstacleGameObject = Instantiate(selectedObstacle, spawnPosition, transform.rotation);
            obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this;
        }
    }

    public void GenerateCoinPattern()
    {
        Vector3 startPos = transform.position;
        if (IsSpaceAvailable(startPos))
        {
            int pattern = Random.Range(0, 3);
            switch (pattern)
            {
                case 0:
                    GenerateStraightLine();
                    break;
                case 1:
                    GenerateArc();
                    break;
                case 2:
                    GenerateZigZag();
                    break;
            }
        }
    }

    public void GenerateStraightLine()
    {
        Vector3 startPos = transform.position;
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = startPos + new Vector3(i * 1.5f, 0, 0);
            if (IsSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void GenerateArc()
    {
        Vector3 startPos = transform.position;
        float arcRadius = 3f;
        for (int i = 0; i < 5; i++)
        {
            float angle = i * Mathf.PI / 4;
            float x = Mathf.Cos(angle) * arcRadius;
            float y = Mathf.Sin(angle) * arcRadius;
            Vector3 spawnPosition = startPos + new Vector3(x, y, 0);
            if (IsSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void GenerateZigZag()
    {
        Vector3 startPos = transform.position;
        for (int i = 0; i < 5; i++)
        {
            float yOffset = (i % 2 == 0) ? 1.5f : -1.5f;
            Vector3 spawnPosition = startPos + new Vector3(i * 1.5f, yOffset, 0);
            if (IsSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void GenerateJumpBoost()
    {
        Vector3 spawnPosition = transform.position;
        if (IsSpaceAvailable(spawnPosition))
        {
            Instantiate(jumpBoost, spawnPosition, transform.rotation);
        }
    }

    private bool IsSpaceAvailable(Vector3 position, float minGap = 0f)
    {
        if (position.y < minYPosition)
        {
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("Coin") || collider.CompareTag("JumpBoost"))
            {
                return false;
            }
        }

        if (minGap > 0f)
        {
            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(position, minGap);
            foreach (Collider2D nearbyCollider in nearbyColliders)
            {
                if (nearbyCollider.CompareTag("Obstacle"))
                {
                    return false;
                }
            }
        }

        return true;
    }
}