using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = .5f;
    private Rigidbody rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((player.transform.position - transform.position).normalized * speed);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
