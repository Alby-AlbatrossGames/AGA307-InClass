using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    private GameObject activeProjectile;

    public float projSpeed;
    public Transform firingPos;

    [Header("Raycast Projectile")]
    public GameObject laserHitSparks;
    public LineRenderer laser;

    private void Start()
    {
        activeProjectile = projectilePrefab1;
        laser.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
            activeProjectile = projectilePrefab1;

        if (Input.GetKeyDown("2"))
            activeProjectile = projectilePrefab2;

        if (Input.GetButtonDown("Fire1")) 
            fireProjectile();

        if (Input.GetButtonDown("Fire2"))
            FireRaycast();
    }

    void fireProjectile()
    {
        //instantiate projectile prefab
        GameObject projectileInstance = Instantiate(activeProjectile, firingPos.position, firingPos.rotation);
        //add force forwards
        projectileInstance.GetComponent<Rigidbody>().AddForce(firingPos.forward * projSpeed);
    }

    private void FireRaycast()
    {
        //create the ray
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        //store info on ray hit
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            StopAllCoroutines();
            StartCoroutine(ShowLaser() );

            Debug.Log("Hit [" + hit.collider.name + "] at point [" + hit.point + "] which was [" + hit.distance + "] units away.");
            GameObject laserInstance = Instantiate(laserHitSparks, hit.point, hit.transform.rotation);

            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
            
        IEnumerator ShowLaser()
        {
            laser.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            laser.gameObject.SetActive(false);
        }

    }
}
