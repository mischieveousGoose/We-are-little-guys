using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
