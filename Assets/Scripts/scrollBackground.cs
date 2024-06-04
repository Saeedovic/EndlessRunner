using UnityEngine;

public class scrollBackground : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Renderer BgRenderer;
    private PlayerScript player;
    private ObstacleGenerator obstacleGenerator;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        obstacleGenerator = GameObject.Find("ObstacleGenerator").GetComponent<ObstacleGenerator>();
    }

    void Update()
    {
        if (player != null && player.isDead) return;

        if (player != null && player.coinCount >= 10)
        {
            speed = 0.3f;
        }

        if (BgRenderer != null)
        {
            BgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
    }
}
