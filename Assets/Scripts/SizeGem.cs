using UnityEngine;

public class SizeGem : MonoBehaviour
{
    public int sizeChange;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMerger>().ChangeSize(sizeChange);
        }
        else if (collision.CompareTag("Dude"))
        {
            collision.GetComponent<Dude>().ChangeSize(sizeChange);
        }
        Destroy(gameObject);
    }
}
