using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVertical : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 5f;
    private Vector3 startPos;
    private float randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed + randomOffset) * 1f + startPos.y; // amplitude = distance/2
        y = Mathf.Clamp(y, startPos.y - 1f, startPos.y + 1f); // total range = 2 units
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
