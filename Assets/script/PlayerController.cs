using System;
using JetBrains.Annotations;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 20;

    public float JumpForce;

    public bool Grounded = true;
    
    public int coinsCollected = 0;

    public TMP_Text coinText;

    public int currentHealth = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Grounded)
        {
            rb.AddForce(Vector2.up * JumpForce);
            Grounded = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    
    {
        if (other.GetComponent<Collectable>())
        {
            coinsCollected++;
            coinText.text = coinsCollected.ToString();
        }
        else if (other.GetComponent<Hazard>())
        {
            currentHealth--;
            healthText.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("SampleScene);
            }
        }
        else if (other.GetComponent<Goall>())
        {
            SceneManager.LoadScene(other.GetComponent<Goall>()NextLevel);
        }
    }
}

