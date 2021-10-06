using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    public float speed;
    public int maxHealth;

    private Animator animator;
    private Slider slider;
    private int health;

    public bool Running { get; set; }

    private void Awake()
    {
        Running = false;
        animator = GetComponent<Animator>();
        slider = transform.Find("Canvas").Find("HealthBar").GetComponent<Slider>();
        health = maxHealth;

        slider.maxValue = maxHealth;
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
            int damage = collision.transform.GetComponent<BulletController>().damage;
            Hurt(damage);

            if (health <= 0)
            {
                // Debe morir
                animator.SetTrigger("isDead");
                Running = false;
            }
            Destroy(collision.transform.gameObject);
        }   
    }

    public void Hurt(int damage)
    {
        health -= damage;
        slider.value = health;
    }
}
