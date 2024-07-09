using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemyHealth;
    int dmgMult = 1;

    public bool HitByProjectile;

    [SerializeField] bool ground;


    Rigidbody enemyRb;

    RaycastHit groundRay;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ground = Physics.Raycast(transform.position, Vector3.down, out groundRay, 2);


        if (!ground)
        {
            dmgMult = 2;
        }
        else
        {
            dmgMult = 1;
        }



        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetHit()
    {
        
        
            enemyHealth -= 75 * dmgMult;
        
    }


}
