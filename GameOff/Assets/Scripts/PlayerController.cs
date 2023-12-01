using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float jumpForce;
    public float gravityModifier;
    public bool onGround = true;
    public bool gameOver = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public int scoreVal = 0;
    public Button restartButton;
    public Button startButton;
    public GameObject titleScreen;
    public GameObject background;
    //the 3 variables below, we were trying to use this to incorporate the start button
/*    private MoveLeft moveLeftBack;
    //private MoveLeft moveLeftObs;
    private RepeatBackground repeatBackground;
    private SpawnManager spawn;*/
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
/*        moveLeftBack = GameObject.Find("Background").GetComponent<MoveLeft>();
        moveLeftBack.enabled = false;
        repeatBackground = GameObject.Find("Background").GetComponent<RepeatBackground>();
        repeatBackground.enabled = false;
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spawn.enabled = false;*/
        /*        moveLeftObs = spawn.obstaclePrefab.GetComponent<MoveLeft>(); //this line gives a nullreference error for some reason.
                moveLeftObs.enabled = false;*/
        //spawn.obstaclePrefab.gameObject.SetActive(false);

        //playerRb.AddForce(Vector3.up * 1000);
        Physics.gravity *= gravityModifier;
        dirtParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //change to this if start button doesn't work
        titleScreen.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            scoreVal++;
            scoreText.text = "Score: " + scoreVal;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            onGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticles.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //onGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dirtParticles.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f);

            explosionParticle.Play();
            dirtParticles.Stop();
            
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

/*    public void StartGame()
    {

        moveLeftBack.enabled = true;
        repeatBackground.enabled = true;
        spawn.enabled=true;
        //moveLeftObs.enabled = true;
        //spawn.obstaclePrefab.gameObject.SetActive(true);
        titleScreen.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            scoreVal++;
            scoreText.text = "Score: " + scoreVal;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            onGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticles.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }*/
}
