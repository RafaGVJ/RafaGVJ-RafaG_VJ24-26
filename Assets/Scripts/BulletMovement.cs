using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f; 
    [SerializeField] private float lifeTime = 5f;

    private ObjectPool bulletPool;

    void OnEnable()
    {
       
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
      
        GetComponent<Rigidbody2D>().linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            bulletPool.ReturnObject(gameObject);
            Destroy(gameObject);
        }

    }
}
