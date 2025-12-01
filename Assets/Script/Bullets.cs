using System.Diagnostics.Contracts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Bullets : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 Direction;
    public AudioClip Sound;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb2D.linearVelocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet() 
    {
        Destroy(gameObject);
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement John = collision.GetComponent<JohnMovement>();
        Grunt Grunt = collision.GetComponent<Grunt>();
        if (John != null)
        {
            John.Hit();
        }
        if (Grunt != null)
        {
            Grunt.Hit();
        }
        DestroyBullet();
    }
}
