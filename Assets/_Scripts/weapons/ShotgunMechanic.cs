using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMechanic : MonoBehaviour
{

    [Header("Ammo")]
    [SerializeField] int maxAmmo = 2;
    [SerializeField] float reloadTime;
    [SerializeField] int Ammo;


    [Header("Player")]
    [SerializeField] Vector3 playerMomentum;
    public Rigidbody rb;


    [Header("Projectile Properties")]
    [SerializeField] float ProjectileSpeed;
    [SerializeField] float projectileSpeedMultiplier;
    public GameObject mainProjectile;
    public GameObject InstantiatedProjectile;
    public GameObject Exploder;


    bool fire = false;


    int instancedGameobjectId = 0;
    string rayPointInstance;

    [SerializeField] LayerMask raycastLayer;

    RaycastHit hit;
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Shoot();

    }


    public void Shoot()
    {
        //GameObject newProjectile;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0)
        {

            bool rayHitCollider = false;
            RaycastHit hitAnywhere;
            Vector3 hitAnywhereVector;
            //Physics.Raycast(Camera.main.transform.forward, Camera.main.transform.forward ,  out hitAnywhere, Mathf.Infinity , raycastLayer);
            rayHitCollider = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitAnywhere, Mathf.Infinity);

            if (rayHitCollider)
            {
                hitAnywhereVector = hitAnywhere.point;
                Debug.Log(hitAnywhere.transform);

                GameObject hitEmpty = new GameObject();


                instancedGameobjectId += 1;

                hitEmpty.name = "Ray Point Instance " + instancedGameobjectId.ToString();
                GameObject.Find(hitEmpty.name);
                Debug.Log(hitEmpty.name);
                Instantiate(hitEmpty, hitAnywhere.point, transform.rotation);
                mainProjectile = Instantiate(InstantiatedProjectile, this.transform.position, this.transform.rotation);
                //mainProjectile.transform.rotation = Quaternion.LookRotation(hitAnywhere.point);
                mainProjectile.GetComponent<Transform>().LookAt(hitAnywhere.point);
                mainProjectile.GetComponent<Rigidbody>().AddForce(mainProjectile.transform.forward * ProjectileSpeed * projectileSpeedMultiplier + Camera.main.transform.InverseTransformVector(rb.velocity).x / 3 * Camera.main.transform.right, ForceMode.Impulse);

            }
        }


    }





    public void Reload()
    {
        if (Ammo == 0 || Input.GetKeyDown(KeyCode.R) && Ammo <= maxAmmo)
        {
            //do animation
        }
    }

}
