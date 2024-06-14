using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitParticles;
    public float lifetime = 3f;

    // Update is called once per frame
    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    public void DestroyProjectile()
    {
        GameObject particleInstance = Instantiate(hitParticles, transform.position, transform.rotation);
        Destroy(particleInstance, 2);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            Destroy(other.gameObject,1);
        }
        DestroyProjectile();
    }
}
