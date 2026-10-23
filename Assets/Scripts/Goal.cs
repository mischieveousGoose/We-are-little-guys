
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public LayerMask dudeLayer;
    public Transform dudeCheck;
    public Vector2 checkSize;
    public int size;

    public bool isComplete;

    bool GetCompletion()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(dudeCheck.position, checkSize * size, 0f, dudeLayer);
        foreach (var col in cols)
        {
            PlayerMerger merger = col.GetComponent<PlayerMerger>();

            if (merger != null)
            {
                if (merger.size == size)
                {
                    return true;
                }
            }
            else
            {
                if (col.GetComponent<Dude>().size == size)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void Start()
    {
        Vector3 newSize = Vector3.one * size;
        newSize.z = 1;
        transform.localScale = newSize;

    }

    void Update()
    {
        isComplete = GetCompletion();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(dudeCheck.position, checkSize * size);
    }
}
