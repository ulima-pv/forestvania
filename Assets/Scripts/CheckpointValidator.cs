using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointValidator : MonoBehaviour
{
    public GhostController ghost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ghost.Running = true;
            Destroy(gameObject);
        }
    }
}
