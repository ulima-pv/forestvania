using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed;

    public bool Running { get; set; }

    private void Awake()
    {
        Running = false;
    }

    private void Update()
    {
        if (Running)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        
    }
}
