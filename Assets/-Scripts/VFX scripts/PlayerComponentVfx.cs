using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class PlayerComponentVfx : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] ParticleSystem DoubleJumpEffect;




    public bool showParticle = false;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DoubleJumpEffect = GetComponentInChildren<ParticleSystem>();
        DoubleJumpEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.doubleJumps == 0 && showParticle)
        {
            DoubleJumpEffect.Play();
        }
        

    }
}
