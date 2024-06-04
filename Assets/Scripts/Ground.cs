/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    PlayerScript player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D bcollider;

    bool didGenerateGround = false;

    public Obstcale boxObstcale;
    public GameObject boxPositionHolderPrefab;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        bcollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (bcollider.size.y / 2);

        Camera mainCamera = Camera.main;
        screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (bcollider.size.x / 2);

        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }
        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }
        transform.position = pos;
    }

    void generateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        pos.y = transform.position.y;

        pos.x = groundRight + goCollider.size.x / 2 - bcollider.size.x / 2;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);

        int maxObstacleNum = 1;
        int obstacleNum = Random.Range(0, maxObstacleNum + 1);

        List<GameObject> spawnedObstacles = new List<GameObject>();

        for (int i = 0; i < obstacleNum; i++)
        {
            GameObject box = Instantiate(boxObstcale.gameObject);
            GameObject boxPositionHolder = Instantiate(boxPositionHolderPrefab); 
            boxPositionHolder.transform.position = box.transform.position; 

            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;

            bool obstacleOverlaps = true;
            int attempts = 0;

            float bufferDistance = 2.0f;

            while (obstacleOverlaps && attempts < 10)
            {
                float x = Random.Range(left + bufferDistance, right - bufferDistance);
                Vector2 boxpos = new Vector2(x, y);
                box.transform.position = boxpos;

                obstacleOverlaps = spawnedObstacles.Exists(existingObstacle =>
                    (box.transform.position - existingObstacle.transform.position).magnitude < 1.0f);

                attempts++;
            }

            if (!obstacleOverlaps)
            {
                spawnedObstacles.Add(box);
            }
            else
            {
                Destroy(box);
            }
        }
    }
}
*/