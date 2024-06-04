using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMechanic : MonoBehaviour
{
    [SerializeField] int maxAmmo = 2;
    [SerializeField] float reloadTime;
    [SerializeField] int disksLoaded;

    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject newProjectile = Instantiate(Projectile, this.transform.position, this.transform.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward,ForceMode.Impulse);
        }
    }
}
