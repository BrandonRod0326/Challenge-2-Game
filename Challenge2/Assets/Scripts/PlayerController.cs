using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public float jump;
    public Text score;
    public Text lives;
    public Text win;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;


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
        win.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            transform.position = new Vector2(81.25f, .502f);
            livesValue = 3;
            lives.text = "Lives:" + livesValue.ToString();

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
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
        }
        if (livesValue == 0)
        {
            win.text = "Game Over! Game created by Brandon Rodriguez";
            musicSource.clip = musicClipThree;
            musicSource.Play();
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
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
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }
}
