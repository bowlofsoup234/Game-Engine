
using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour
{
    // Variables for the gun's damage, range, fire rate, and impact force.
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    private float nextTimeTofire = 0f;

    
    // Variables for ammo management, including max ammo, reload time, and current ammo.
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    private int currentAmmo;
    private bool isRealoading = false;
    
    // Reference to the item pickup script.
    public itemPickup itemPickup;
    
    // Reference to the camera and impact effect for shooting.
    public Camera fpsCam;
    public GameObject impactEffect;
    
    // Reference to the muzzle flash particle system and animator component.
    public ParticleSystem muzzleFlash;
    public Animator animator;
    
    // Start is called before the first frame update.
    void Start()
    {
        // Set the current ammo to the max ammo.
        currentAmmo = maxAmmo;
    }
    
    // Called when the gun is enabled.
    void OnEnable()
    {
        // Reset the reloading status and animator bool.
        isRealoading = false;
        animator.SetBool("Reloading", false);
    }
    
    // Update is called once per frame.
    void Update()
    {
        // If reloading, return.
        if (isRealoading)
            return;
        
        // If out of ammo, start reloading.
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        // If the gun is equipped and the fire button is pressed, shoot.
        if (itemPickup.parentObject.activeInHierarchy && itemPickup.isEquipped)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeTofire)
            {
                nextTimeTofire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }
    
    // Coroutine for reloading the gun.
    IEnumerator Reload()
    {
        // Set the reloading status and animator bool.
        isRealoading = true;
        Debug.Log("realoading");
        animator.SetBool("Reloading", true);
        
        // Wait for the reload time minus a quarter second.
        yield return new WaitForSeconds(reloadTime - 0.25f);
        
        // Reset the animator bool and wait a quarter second.
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        
        // Set the current ammo to the max ammo and reset the reloading status.
        currentAmmo = maxAmmo;
        isRealoading = false;
    }
    
    // Method for shooting the gun.
    void Shoot()
    {
        // Play the muzzle flash particle system and decrement the ammo count.
        muzzleFlash.Play();
        currentAmmo--;
        
        // Cast a ray from the camera to the shooting range and check for collision.
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // If the collision has a Target component, damage it.
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            
            // If the collision has a rigidbody, apply
        // force to it based on the impact force.
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
        
        // Instantiate the impact effect and destroy it after two seconds.
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);
    }
}
}

