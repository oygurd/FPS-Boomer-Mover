using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProjectileHit : MonoBehaviour
{
    public float raycastLength;

    [SerializeField] LayerMask RayLayerHit = LayerMask.GetMask("Enemy");
    [SerializeField] bool hitEnemy = false;

    public EnemyBehaviour enemyScript; 
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // hitEnemy = Physics.CheckSphere(transform.position, 1, RayLayerHit);
        hitEnemy = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, RayLayerHit);
        Debug.Log(hitEnemy);

        if (hitEnemy)
        {
            Destroy(gameObject);
            EnemyBehaviour lowerEnemyHp = hit.collider.GetComponent<EnemyBehaviour>();
            lowerEnemyHp.enemyHealth -= 75;
            hit.rigidbody.AddForce(Vector3.up * 10 , ForceMode.Impulse);
        }

    }


}
