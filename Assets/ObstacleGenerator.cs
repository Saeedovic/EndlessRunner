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
    private List<Vector3> recentPositions = new List<Vector3>();

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
            GenerateObstaclePattern();
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
                GenerateObstaclePattern();
                obstacleCooldown = generationInterval + Random.Range(1f, 3f);
            }

            if (coinCooldown <= 0)
            {
                GenerateRandomItem();
                coinCooldown = generationInterval + Random.Range(1f, 3f);
            }
        }

        // Clean up the recent positions list to keep it from growing indefinitely
        // Remove positions older than 5 seconds
        recentPositions.RemoveAll(pos => (Time.time - pos.z) > 5f);
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

    public void GenerateObstaclePattern()
    {
        if (obstacles.Count == 0)
        {
            Debug.LogError("No obstacles set in the obstacle list");
            return;
        }

        int pattern = Random.Range(0, 3);
        switch (pattern)
        {
            case 0:
                GenerateSingleObstacle();
                break;
            case 1:
                GenerateStackedObstacles();
                break;
            case 2:
                GenerateSideBySideObstacles();
                break;
        }
    }

    public void GenerateSingleObstacle()
    {
        Debug.Log("Im single obstacles");

        int randomIndex = Random.Range(0, obstacles.Count);
        GameObject selectedObstacle = obstacles[randomIndex];

        Vector3 spawnPosition = transform.position;
        if (IsSpaceAvailable(spawnPosition, minGapBetweenObstacles))
        {
            GameObject obstacleGameObject = Instantiate(selectedObstacle, spawnPosition, transform.rotation);
            obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this;
            recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
        }
    }

    public void GenerateStackedObstacles()
    {
        Debug.Log("Im two top obstacles");

        int randomIndex = Random.Range(0, obstacles.Count);
        GameObject selectedObstacle = obstacles[randomIndex];

        Vector3 basePosition = transform.position;
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = basePosition + new Vector3(0, i * 2f, 0);
            if (IsSpaceAvailable(spawnPosition, minGapBetweenObstacles))
            {
                GameObject obstacleGameObject = Instantiate(selectedObstacle, spawnPosition, transform.rotation);
                obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this;
                recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
            }
        }
    }

    public void GenerateSideBySideObstacles()
    {
        Debug.Log("Im side to side obstacles");

        int randomIndex = Random.Range(0, obstacles.Count);
        GameObject selectedObstacle = obstacles[randomIndex];

        Vector3 basePosition = transform.position;
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = basePosition + new Vector3(i * 2f, 0, 0);
            if (IsSpaceAvailable(spawnPosition, minGapBetweenObstacles))
            {
                GameObject obstacleGameObject = Instantiate(selectedObstacle, spawnPosition, transform.rotation);
                obstacleGameObject.GetComponent<Obstcale>().obstacleGenerator = this;
                recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
            }
        }
    }

    public void GenerateCoinPattern()
    {
        Vector3 startPos = transform.position;
        if (IsItemSpaceAvailable(startPos))
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
            if (IsItemSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
                recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
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
            if (IsItemSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
                recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
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
            if (IsItemSpaceAvailable(spawnPosition))
            {
                Instantiate(coin, spawnPosition, Quaternion.identity);
                recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
            }
        }
    }

    public void GenerateJumpBoost()
    {
        Vector3 spawnPosition = transform.position;
        if (IsItemSpaceAvailable(spawnPosition))
        {
            Instantiate(jumpBoost, spawnPosition, transform.rotation);
            recentPositions.Add(new Vector3(spawnPosition.x, spawnPosition.y, Time.time));
        }
    }

    private bool IsSpaceAvailable(Vector3 position, float minGap = 0f)
    {
        Debug.Log("Checking space availability at position: " + position);

        if (position.y < minYPosition)
        {
            Debug.Log("Position too low: " + position);
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Debug.Log("Position occupied by an obstacle: " + collider.tag);
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
                    Debug.Log("Nearby obstacle found at: " + nearbyCollider.transform.position);
                    return false;
                }
            }
        }

        // Check against recent positions
        foreach (var recentPos in recentPositions)
        {
            if (Vector3.Distance(position, recentPos) < minGap)
            {
                Debug.Log("Position too close to a recent obstacle or item: " + recentPos);
                return false;
            }
        }

        return true;
    }

    private bool IsItemSpaceAvailable(Vector3 position, float minGap = 0f)
    {
        Debug.Log("Checking item space availability at position: " + position);

        if (position.y < minYPosition)
        {
            Debug.Log("Position too low: " + position);
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Coin") || collider.CompareTag("JumpBoost") || collider.CompareTag("Obstacle"))
            {
                Debug.Log("Position occupied by an item or obstacle: " + collider.tag);
                return false;
            }
        }

        if (minGap > 0f)
        {
            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(position, minGap);
            foreach (Collider2D nearbyCollider in nearbyColliders)
            {
                if (nearbyCollider.CompareTag("Coin") || nearbyCollider.CompareTag("JumpBoost") || nearbyCollider.CompareTag("Obstacle"))
                {
                    Debug.Log("Nearby item or obstacle found at: " + nearbyCollider.transform.position);
                    return false;
                }
            }
        }

        // Check against recent positions
        foreach (var recentPos in recentPositions)
        {
            if (Vector3.Distance(position, recentPos) < minGap)
            {
                Debug.Log("Position too close to a recent obstacle or item: " + recentPos);
                return false;
            }
        }

        return true;
    }
}