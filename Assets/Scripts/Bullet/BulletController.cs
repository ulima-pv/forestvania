using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float timeTillDestroy; // Tiempo hasta que se destruya la bala (seg)

    private float time = 0f;
    private bool left = false;

    private void Start()
    {
        if (transform.rotation.eulerAngles.z == 180f)
        {
            left = true;
        }
    }

    private void Update()
    {
        if (time > timeTillDestroy)
        {
            // Autodestruccion
            Destroy(transform.gameObject);
        }
        else
        {
            time += Time.deltaTime;
            if (left)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            } else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            
        }
    }
}
