using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject sparkleFx;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            gameManager.UpdateKeyCount(1);
            Sparkle();
            Destroy(gameObject);
        }
    }

    void Sparkle()
    {
        Instantiate(sparkleFx, transform.position, sparkleFx.transform.rotation);
    }
}
