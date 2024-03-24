using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody2D rb;
    public bool isGrounded;
    public float speed;
    public float jumpForce;
    private float moveInput;
    [Header("Sounds")]
    public AudioSource source;
    public List<AudioClip> jumpSounds;
    public List<AudioClip> brabyDestroySounds; 
    [Header("Text")]
    public GameObject pressMouse1Text;

    public bool isTraby;
    public bool isReadyToPunch = false;
    public GameObject brabyWall;
    public GameObject wallDestroyParticle;

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
        if (!isTraby)
        {
            if (isReadyToPunch == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(brabyWall);
                    Instantiate(wallDestroyParticle, brabyWall.transform.position, Quaternion.identity);
                    var randomSound = brabyDestroySounds[Random.Range(0, brabyDestroySounds.Count)];
                    source.PlayOneShot(randomSound);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            print("Win");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTraby)
        {
            if (collision.gameObject.name.Contains("TriggerForPunch"))
            {
                pressMouse1Text.gameObject.SetActive(true);
                isReadyToPunch = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isTraby)
        {
            if (collision.gameObject.name.Contains("TriggerForPunch"))
            {
                pressMouse1Text.gameObject.SetActive(false);
                isReadyToPunch = false;
            }
        }
    }
}
