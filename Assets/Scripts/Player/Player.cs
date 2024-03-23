using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isGrounded;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public AudioSource source;
    public List<AudioClip> jumpSounds;
    public bool isTraby; //only traby plays sounds

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(isTraby)
                {
                    var randomSound = jumpSounds[Random.Range(0, jumpSounds.Count)];
                    source.PlayOneShot(randomSound);
                }    
                rb.velocity += Vector2.up * jumpForce;
                isGrounded = false;
            }
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
