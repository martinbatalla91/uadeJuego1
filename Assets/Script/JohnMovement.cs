using System.Text.RegularExpressions;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    public GameObject BulletsPrefab;
    private Rigidbody2D rb2D;
    private float horizontal;
    private bool Grounded;
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
        horizontal = Input.GetAxisRaw("horizontal");
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

            Animator.SetBool("running", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
   
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f) 
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        rb2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot() 
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

            GameObject Bullets = Instantiate(BulletsPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            Bullets.GetComponent<Bullets>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(horizontal, rb2D.linearVelocity.y);
    }

    public void Hit() 
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
