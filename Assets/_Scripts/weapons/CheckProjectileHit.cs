using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProjectileHit : MonoBehaviour
{
    [SerializeField] GameObject ExplosionPoint;



    public float groundSphereCheckRad;


    public float sphereCastRad;
    public float sphereCastMaxDis;

    [SerializeField] LayerMask enemyHitLayer;
    [SerializeField] LayerMask hitEnvironmentLayer;


    [SerializeField] bool hitEnemy = false;
    [SerializeField] bool hitEnvironment = false;

    public EnemyBehaviour enemyScript;
    RaycastHit hit;
    RaycastHit environmentHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // hitEnemy = Physics.CheckSphere(transform.position, 1, RayLayerHit);
        //hitEnemy = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, RayLayerHit);
        hitEnemy = Physics.SphereCast(transform.position, sphereCastRad, transform.forward, out hit, sphereCastMaxDis, enemyHitLayer);
        hitEnvironment = Physics.SphereCast(transform.position, sphereCastRad, transform.forward, out environmentHit, sphereCastMaxDis, hitEnvironmentLayer);
        Debug.Log(hitEnemy);


        if (hitEnemy)
        {    
            Destroy(gameObject);

            EnemyBehaviour lowerEnemyHp = hit.collider.GetComponent<EnemyBehaviour>();
            lowerEnemyHp.enemyHealth -= 75;
            hit.rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);

        }

        if (hitEnvironment)
        {
            sphereCastRad = 2.5f;
            sphereCastMaxDis = 2;
            /*if (hitEnemy)
            {
                EnemyBehaviour lowerEnemyHp = hit.collider.GetComponent<EnemyBehaviour>();
                lowerEnemyHp.enemyHealth -= 75;
                hit.rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
                Destroy(gameObject);

            }*/
            sphereCastRad = 0.1f;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, sphereCastRad);
    }
}
