using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public float jump;
    public float timeCurrent = 0f;
    public float timeStart = 100f;
    public Text score;
    public Text lives;
    public Text win;
    public Text time;
    public AudioSource musicSource;
    public AudioSource musicSource2;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioClip musicClipFive;

    private int scoreValue = 0;
    private int livesValue = 3;
    private bool facingRight = true;


    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        score.text = "Coins:" + scoreValue.ToString();
        lives.text = "Lives:" + livesValue.ToString();
        timeCurrent = timeStart;
        win.text = "";
        musicSource2.clip = musicClipOne;
        musicSource2.Play();
        musicSource2.loop = true;

    }


    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        rd2d.AddForce(new Vector2(hozMovement * jump, verMovement * jump));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        if (hozMovement == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
        }

        timeCurrent -= 1 * Time.deltaTime;
        time.text = timeCurrent.ToString("0");

        if (timeCurrent <= 0)
        {
            timeCurrent = 0;
            win.text = "Game Over! Game created by Brandon Rodriguez";
            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = true;
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
            Destroy(musicSource2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            musicSource.clip = musicClipFour;
            musicSource.Play();

            if (scoreValue == 4)
            {
                transform.position = new Vector2(81.25f, .502f);
                jump = 1;
                livesValue = 3;
                lives.text = "Lives:" + livesValue.ToString();
            }

        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue == 8)
        {
            win.text = "You Win! Game created by Brandon Rodriguez";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
            Destroy(musicSource2);

        }
        if (livesValue == 0)
        {
            win.text = "Game Over! Game created by Brandon Rodriguez";
            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = true;
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
            Destroy(musicSource2);
            
        }

        if (collision.collider.tag == "JumpPotion")
        {
            jump = 3;
            Destroy(collision.collider.gameObject);

        }
    }
       


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);

                anim.SetBool("Jump", true);

                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            else
            {
                anim.SetBool("Jump", false);
            }

        }

        if (collision.collider.tag == "Tree")
        { 
            if (Input.GetKey(KeyCode.W))
            {
               rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);


                anim.SetBool("Jump", true);

                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }
}
