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
    BgMusicManager musicManager;
    [Header("Text")]
    public GameObject pressMouse1Text;
    [Header ("Braby&Traby")]
    public bool isTraby;
    public bool isReadyToPunch = false;
    public GameObject brabyWall;
    public GameObject wallDestroyParticle;
    [Header("Win")]
    public GameObject winWindow;
    public AudioClip winSound;
    public GameObject destroyText;
    public AudioSource bgMusic;
    [Header("Pause")]
    public GameObject pauseWindow;
    static AudioClip whatPlaying;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        musicManager = GetComponentInChildren<BgMusicManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
            Win();
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
    public void PauseGame()
    {
        Time.timeScale = 0f;
        destroyText.SetActive(false);
        pauseWindow.gameObject.SetActive(true);
        bgMusic.gameObject.SetActive(false);
        
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        destroyText.SetActive(true);
        pauseWindow.gameObject.SetActive(false);
        bgMusic.gameObject.SetActive(true);
        print(musicManager.playingTime);
        bgMusic.time = musicManager.playingTime;
    }
    public void Win()
    {
        source.PlayOneShot(winSound);
        Destroy(destroyText);
        Time.timeScale = 0f;
        winWindow.gameObject.SetActive(true);
        bgMusic.Stop();
    }
}
