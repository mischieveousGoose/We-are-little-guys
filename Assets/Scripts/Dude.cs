using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;

public class Dude : MonoBehaviour
{
    public int size;

    void Update()
    {
        Vector3 scale = Vector3.one;
        scale *= size;
        scale.z = 1;

        transform.localScale = scale;
    }

    public void ChangeSize(int change)
    {
        Debug.Log("Size Change");
        if (size + change <= 0) return;

        transform.position += Vector3.up * (change * 0.5f);

        size += change;
        transform.localScale = Vector3.one * size;
    }
}
