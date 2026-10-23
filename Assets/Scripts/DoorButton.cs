using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    public LayerMask pushLayer;
    public Vector2 checkSize;
    Collider2D doorCollider;
    bool pushed;

    void Start()
    {
        doorCollider = door.GetComponent<Collider2D>();
    }

    void Update()
    {
        pushed = Physics2D.OverlapBox(transform.position, checkSize, 0f, pushLayer);
        doorCollider.enabled = !pushed;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, checkSize);
    }
}
