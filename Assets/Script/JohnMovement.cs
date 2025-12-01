using System.Text.RegularExpressions;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float JumpForce = 10;
    public float Speed;
    public float speedBullet = 10f;
    public GameObject Bullet;
    private Rigidbody2D rb2D;
    private float horizontal;
    private Animator Animator;
    private float LastShoot;
    private int Health;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        horizontal = Input.GetAxis("horizontal");
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        Animator.SetBool("running", horizontal != 0.0f);

        //jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            Animator.SetBool("jump", true);
            rb2D.AddForce(new Vector2(0,JumpForce));
           
        }

        //Shoot
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f) 
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

   
    private void Shoot() 
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;


            GameObject Bullets = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
            Bullets.GetComponent<Bullets>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(horizontal * Speed, rb2D.linearVelocity.y);
    }

    public void Hit() 
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") 
        {
            Animator.SetBool("jump", false);
        }
    }
}
