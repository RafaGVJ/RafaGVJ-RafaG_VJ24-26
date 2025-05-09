using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ObjectPool bulletPool;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetObject(); 
        if (bullet != null)
        {
          
            bullet.GetComponent<BulletMovement>().enabled = true;
        }
    }
}
