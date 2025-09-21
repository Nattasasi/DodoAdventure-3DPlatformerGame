using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 5f;
    private Vector3 startPos;
    private bool movingForward = true;

    public enum Axis { X, Y, Z }
    public Axis moveAxis = Axis.Z;

    public float startDelay = 0f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < startDelay) return;
        Vector3 direction = Vector3.zero;
        switch (moveAxis)
        {
            case Axis.X:
                direction = Vector3.right;
                break;
            case Axis.Y:
                direction = Vector3.up;
                break;
            case Axis.Z:
                direction = Vector3.forward;
                break;
        }
        if (movingForward)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) >= distance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.Translate(-direction * speed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) <= 0.1f)
            {
                movingForward = true;
            }
        }
    }
}
