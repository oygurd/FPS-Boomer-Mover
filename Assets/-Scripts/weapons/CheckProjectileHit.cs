using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProjectileHit : MonoBehaviour
{

    [Header("Peojectile Properties")]
    [SerializeField] GameObject ExplosionPoint;
    Rigidbody rb;
    MeshRenderer self;
    Collider selfCol;
    public float groundSphereCheckRad;
    public float sphereCastRad;
    public float sphereCastMaxDis;
    TrailRenderer ProjectileTrail;

    [Header("Enemy Properties")]
    [SerializeField] LayerMask enemyHitLayer;
    [SerializeField] LayerMask hitEnvironmentLayer;
    [SerializeField] bool hitEnemy = false;
    [SerializeField] bool hitEnvironment = false;
    bool hitEnemyRange;
    public EnemyBehaviour enemyScript;
    RaycastHit hit;
    RaycastHit environmentHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        self = GetComponent<MeshRenderer>();
        ProjectileTrail = GetComponentInChildren<TrailRenderer>();
        selfCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // hitEnemy = Physics.CheckSphere(transform.position, 1, RayLayerHit);
        //hitEnemy = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, RayLayerHit);
        hitEnemy = Physics.SphereCast(transform.position, sphereCastRad, transform.forward, out hit, sphereCastMaxDis, enemyHitLayer);
        hitEnvironment = Physics.SphereCast(transform.position, groundSphereCheckRad, transform.forward, out environmentHit, sphereCastMaxDis, hitEnvironmentLayer);

        Debug.Log(hitEnemy);

        HitSomething();


    }

    private void OnDestroy()
    {
        transform.DetachChildren();

    }


    public void HitSomething()
    {
        if (hitEnemy)
        {
            Destroy(gameObject);

            EnemyBehaviour lowerEnemyHp = hit.collider.GetComponent<EnemyBehaviour>();
            lowerEnemyHp.enemyHealth -= 75;
            hit.rigidbody.AddForce(Vector3.up * 20 + transform.forward * 5, ForceMode.Impulse);

        }

        if (hitEnvironment)
        {

            rb.velocity = new Vector3(0, 0, 0);
            self.enabled = false;
            Collider[] enemyCol = Physics.OverlapSphere(transform.position, 4, enemyHitLayer);

            foreach (Collider enemy in enemyCol)
            {
                if (enemy.GetComponent<EnemyBehaviour>() /*&& sphereCastRad <= 4*/)
                {
                    transform.LookAt(enemy.transform.position);
                    RaycastHit hitenemydis;

                    Debug.Log("yes!, an enemy!");
                    // RaycastHit hit;

                    hitEnemyRange = Physics.Raycast(transform.position, transform.forward, out hitenemydis, Mathf.Infinity, enemyHitLayer);
                    Debug.DrawRay(transform.position, enemy.transform.position * hitenemydis.distance, Color.red);
                    if (hitEnemyRange)
                    {

                        hitenemydis.rigidbody.AddForce(Vector3.up * 20 + transform.forward * 5, ForceMode.Impulse);

                    }
                    Debug.Log(hitEnemyRange);
                    Debug.Log(enemyCol.Length);
                    // sphereCastRad += 30 * Time.deltaTime;


                    //enemy.GetComponent<Rigidbody>().AddForce(enemy. * 2.3f + transform.forward * 1.3f,ForceMode.Impulse);
                    //enemy.attachedRigidbody.AddForce(Vector3.up * 20 + transform.forward * 5, ForceMode.Impulse);

                }

            }

            if (enemyCol.Length == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {

    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, sphereCastRad);
    }
}
