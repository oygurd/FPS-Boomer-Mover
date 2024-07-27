using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjectileHit : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Rigidbody _playerRb;



    [Header("Explosion")]
    [SerializeField] float _explosionRad;
    [SerializeField] float _explosionForce;

    [SerializeField] float _overSphereSize;

    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public LayerMask GroundsMask;

    [Header("Decals")]
    GameObject _explosionDecal;

    [Header("VFX")]
    [SerializeField] GameObject _explosionVfxHolder;
    [SerializeField] GameObject explosionVfxPoint;



    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDestroy()
    {
        transform.DetachChildren();

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag())
        {
            Explosion();
        }*/

        ExplosionEnemy();
        rb.velocity = Vector3.zero;
        transform.DetachChildren();
        Destroy(this.gameObject);
        explosionVfxPoint = Instantiate(_explosionVfxHolder, transform.position, transform.rotation);
        VFXanimationsController vfx = _explosionVfxHolder.GetComponent<VFXanimationsController>();
        vfx.ExplosionStart();
        Destroy(explosionVfxPoint, 1);
        
        

    }
    public void ExplosionEnemy()
    {
        Collider[] overlapSphereCheckEnemy = Physics.OverlapSphere(transform.position, _overSphereSize, enemyLayer);
        Collider[] overlapSphereCheckPlayer = Physics.OverlapSphere(transform.position, _overSphereSize, playerLayer);

        foreach (Collider Enemy in overlapSphereCheckEnemy)
        {
            Rigidbody EnemyRb = Enemy.GetComponent<Rigidbody>();
            if (EnemyRb)
            {
                EnemyRb.AddExplosionForce(_explosionForce, transform.position, _explosionRad, 1, ForceMode.VelocityChange);
            }
        }
        foreach (Collider player in overlapSphereCheckPlayer)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if(playerRb)
            {
                playerRb.AddExplosionForce(_explosionForce, transform.position, _explosionRad, 0.5F, ForceMode.Impulse);
            }
        }
    }

    public void ExplosionPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f, GroundsMask))
        {
            
        }
    }

}
