using UnityEngine;

public class Grunt : MonoBehaviour
{
    public GameObject John;
    public GameObject Bullet;
    private float LastShoot;
    private int Health = 3;
    private void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 0.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);
        if (distance < 1.0f && Time.time > LastShoot + 0.25f) 
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot() 
    {
        Debug.Log("Shoot");
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject Bullets = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
        Bullets.GetComponent<Bullets>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
