using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    public LayerMask pushLayer;
    public Vector2 checkSize;
    Collider2D doorCollider;
    bool pushed;
    public Sprite noSprite, yesSprite;
    SpriteRenderer sr;
    SpriteRenderer doorSr;
    public Sprite doorYes, doorNo;

    void Start()
    {
        doorCollider = door.GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        doorSr = door.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        pushed = Physics2D.OverlapBox(transform.position, checkSize, 0f, pushLayer);
        doorCollider.enabled = !pushed;
        sr.sprite = pushed ? yesSprite : noSprite;
        doorSr.sprite = pushed ? doorYes : doorNo;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, checkSize);
    }
}
