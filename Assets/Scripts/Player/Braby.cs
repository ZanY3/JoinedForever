using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braby : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody2D rb;
    static bool isGrounded;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight;
    
    [Header("Sounds")]
    public AudioSource source;
    public List<AudioClip> jumpSounds;
    public List<AudioClip> brabyDestroySounds;
    BgMusicManager musicManager;
    public AudioClip buttonSound;
    [Header("Text")]
    public GameObject pressMouse1Text;
    [Header("mechanics")]
    static bool isReadyToPunch = false;
    public GameObject destroyWall;
    public GameObject wallDestroyParticle;
    public GameObject wallDestroyOnButton;
    public GameObject onButtonWallParticles;
    [Header("Win")]
    public GameObject winWindow;
    public AudioClip winSound;
    public GameObject destroyText;
    public AudioSource bgMusic;
    [Header("Pause")]
    public GameObject pauseWindow;
    [Header("Anims")]
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        musicManager = FindAnyObjectByType<BgMusicManager>();
    }
    private void Update()
    {
        if(moveInput != 0)
        {
            animator.SetBool("IsRuning", true);
        }
        else
        {
            animator.SetBool("IsRuning", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        moveInput = Input.GetAxis("Horizontal");
        if (facingRight == false && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput > 0)
        {
            Flip();
        }
        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.Play("BrabyJump");
                rb.velocity += Vector2.up * jumpForce;
                isGrounded = false;
            }
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (isReadyToPunch == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(destroyWall);
                Instantiate(wallDestroyParticle, destroyWall.transform.position, Quaternion.identity);
                var randomSound = brabyDestroySounds[Random.Range(0, brabyDestroySounds.Count)];
                source.PlayOneShot(randomSound);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("TriggerForPunch"))
        {
            pressMouse1Text.gameObject.SetActive(true);
            isReadyToPunch = true;
        }
        if(collision.gameObject.name.Contains("Button"))
        {
            source.PlayOneShot(buttonSound);
            Instantiate(onButtonWallParticles, wallDestroyOnButton.transform.position, Quaternion.identity);
            Destroy(wallDestroyOnButton);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("TriggerForPunch"))
        {
            pressMouse1Text.gameObject.SetActive(false);
            isReadyToPunch = false;
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
    void Flip() //rotate at move
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        
    }


}
