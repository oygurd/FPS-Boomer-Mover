using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMechanic : MonoBehaviour
{
    [SerializeField] int maxAmmo = 2;
    [SerializeField] float reloadTime;
    [SerializeField] int Ammo;


    [Header("Projectile Properties")]
    [SerializeField] float ProjectileSpeed;
    public GameObject mainProjectile;
    public GameObject InstantiatedProjectile;
    public GameObject Exploder;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo >= 0)
        {
            RaycastHit hitAnywhere;
            Vector3 hitAnywhereVector;
            //Physics.Raycast(Camera.main.transform.forward, Camera.main.transform.forward ,  out hitAnywhere, Mathf.Infinity , raycastLayer);
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitAnywhere, 1000);
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
            mainProjectile.GetComponent<Rigidbody>().AddForce(mainProjectile.transform.forward * ProjectileSpeed * Time.deltaTime, ForceMode.Impulse);
            //mainProjectile.GetComponent<Rigidbody>().AddForce(hitEmpty.transform.forward * ProjectileSpeed * Time.deltaTime, ForceMode.Impulse);





            //mainProjectile.transform.position = Vector3.MoveTowards(transform.position, hitEmpty.transform.position, ProjectileSpeed * Time.deltaTime);
            //Physics.CheckSphere(transform.position, 2, raycastLayer);
            //newProjectile.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward, ForceMode.Impulse);
        }







        //trash, maybe use later if needed
        /*Physics.CheckSphere(InstantiatedProjectile.transform.position, 1);

        mainProjectile.GetComponent<Collider>().gameObject.layer = raycastLayer;
        Debug.Log(raycastLayer);

        Physics.Raycast(mainProjectile.transform.position, mainProjectile.transform.TransformDirection(Vector3.forward), out hit, 1, raycastLayer);

        //hit.rigidbody.AddExplosionForce(explosionForce, newProjectile.transform.position, explosionRadius, explosionUpward, ForceMode.Impulse);
        hit.rigidbody.AddForce(Vector3.up);*/

    }


    public void Projectile(GameObject newprojectile)
    {
        newprojectile = new GameObject("newprojectile");


    }




    public void Reload()
    {
        if (Ammo == 0 || Input.GetKeyDown(KeyCode.R) && Ammo <= maxAmmo)
        {
            //do animation
        }
    }

}
