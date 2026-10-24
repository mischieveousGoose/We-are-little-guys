using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    List<GameObject> spawnedAliens = new List<GameObject>();
    public float scrollSpeed;
    public Transform alienParent;
    public GameObject alien;
    public float startX, endX, topY, bottomY;

    [Min(0.01f)] public float spawnTime = 1f;
    float spawnTimer;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        spawnTimer -= deltaTime;
        if (spawnTimer < 0)
        {
            SpawnAlien();
        }

        for (int i = spawnedAliens.Count - 1; i >= 0; i--)
        {
            GameObject spawnedAlien = spawnedAliens[i];
            spawnedAlien.transform.position += Vector3.right * scrollSpeed * deltaTime;
            if (spawnedAlien.transform.position.x > Mathf.Max(startX, endX))
            {
                spawnedAliens.RemoveAt(i);
                Destroy(spawnedAlien);
            }
        }
    }

    void SpawnAlien()
    {
        Vector3 pos = new Vector3(startX, Random.Range(bottomY, topY), 0f);
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject obj = Instantiate(alien, pos, rot, alienParent);
        spawnedAliens.Add(obj);
        spawnTimer = spawnTime;
    }

    void OnDrawGizmos()
    {
        float left = Mathf.Min(startX, endX);
        float right = Mathf.Max(startX, endX);
        float bottom = Mathf.Min(bottomY, topY);
        float top = Mathf.Max(bottomY, topY);

        Vector3 bottomLeft = new Vector3(left, bottom, 0f);
        Vector3 bottomRight = new Vector3(right, bottom, 0f);
        Vector3 topLeft = new Vector3(left, top, 0f);
        Vector3 topRight = new Vector3(right, top, 0f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
