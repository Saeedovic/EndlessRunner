using UnityEngine;

public class scrollBackground : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Renderer BgRenderrer;
    private Player player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player != null && player.isDead) return;

        BgRenderrer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}