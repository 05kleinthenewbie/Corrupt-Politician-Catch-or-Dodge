using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject moneyPrefab;
    public GameObject handcuffPrefab;
    public GameObject bulletPrefab;

    [Header("Spawn Settings")]
    public int laneCount = 5;
    public float spawnInterval = 0.6f;
    public float fallSpeed = 2f;
    public float lanePadding = 0.5f;

    private float[] laneXPositions;

    void Start()
    {
        CalculateLanes();
        InvokeRepeating("SpawnRow", 1f, spawnInterval);
    }

    public void CalculateLanes()
    {
        Camera cam = Camera.main;

        // World-space X edges
        float left = cam.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
        float right = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;

        // Get player width (requires tag "Player")
        float playerWidth = GameObject.FindWithTag("Player")
            .GetComponent<BoxCollider2D>().bounds.size.x;

        // Lane width equals player width
        float laneWidth = playerWidth + lanePadding;

        // Recalculate how many lanes fit on screen
        laneCount = Mathf.FloorToInt((right - left) / laneWidth);

        laneXPositions = new float[laneCount];

        // First lane center
        float startX = left + (laneWidth / 2f);

        // Generate lane positions
        for (int i = 0; i < laneCount; i++)
            laneXPositions[i] = startX + i * laneWidth;
    }

       public void SpawnRow()
    {
        float y = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y + 1f;

        int moneyLane = Random.Range(0, laneCount); // guarantee at least 1 money lane

        for (int i = 0; i < laneCount; i++)
        {
            GameObject prefabToSpawn;

            if (i == moneyLane)
            {
                // Forced money lane
                prefabToSpawn = moneyPrefab;
            }
            else
            {
                // Normal probability roll for other lanes
                int roll = Random.Range(1, 101);

                if (roll <= 70)
                    prefabToSpawn = moneyPrefab;
                else if (roll <= 90)
                    prefabToSpawn = handcuffPrefab;
                else
                    prefabToSpawn = bulletPrefab;
            }

            GameObject obj = Instantiate(prefabToSpawn, new Vector3(laneXPositions[i], y, 0), Quaternion.identity);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
    }
}
