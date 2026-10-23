using UnityEngine;

public class PlayerMerger : MonoBehaviour
{
    public LayerMask dudeLayer;
    public float senseRange;
    public int size = 1;
    public LayerMask groundLayer;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dude dude = GetNearbyDude();
            if (dude != null) Merge(dude);
        }
    }

    public void ChangeSize(int change)
    {
        if (size + change <= 0) return;

        transform.position += Vector3.up * (change * 0.5f);

        size += change;
        transform.localScale = Vector3.one * size;
    }

    void Merge(Dude dude)
    {
        Vector2 realSize = GetRealSize(size);
        Vector2 newSize = GetRealSize(size + 1);
        Vector2 point = transform.position + new Vector3(0, realSize.y / 2, 0f) + (Vector3.up * 0.1f);


        if (!Physics2D.OverlapBox(point, newSize, 0f, groundLayer))
        {
            size += 1;
            transform.position = point;
            transform.localScale = GetRealSize(size);
            Destroy(dude.gameObject);
        }
        else
        {
            Debug.Log("not enough room to merge");
        }

    }

    Vector2 GetRealSize(int size)
    {
        return Vector2.one * size;
    }


    Dude GetNearbyDude()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, senseRange, dudeLayer);
        foreach (var col in cols)
        {
            Dude dude = col.GetComponent<Dude>();
            if (dude.size == size)
            {
                return dude;
            }
        }

        return null;
    }
}
