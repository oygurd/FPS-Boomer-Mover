using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXanimationsController : MonoBehaviour
{

    [Header("Explosion")]
    [SerializeField] Animator ExplosionEffect;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ExplosionStart()
    {
        ExplosionEffect.SetBool("explode", true);
    }
    public void ExplosionEnd()
    {
        ExplosionEffect.SetBool("explode", false);
        Destroy(gameObject);
    }


}
