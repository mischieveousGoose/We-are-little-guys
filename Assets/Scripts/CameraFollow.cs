
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float screenShakeTimer;
    float intensity = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        screenShakeTimer -= Time.deltaTime;
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);
        if (screenShakeTimer < 0f)
            transform.position = targetPos;
        else
        {
            transform.position = targetPos + (Vector3)(Random.insideUnitCircle * intensity);
        }
    }

    public void ScreenShake(float time, float intensity)
    {
        screenShakeTimer = time;
        this.intensity = intensity;
    }
}
