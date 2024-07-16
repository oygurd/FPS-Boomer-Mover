using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ShotgunAnim : MonoBehaviour
{
    //also the hands animations here!!!//

    Animator shotgunAnims;
    public ShotgunMechanic shotgunScript;
    [SerializeField] bool canReload = false;

    public TMP_Text AmmoCountUI;


    [Header("WeaponSwaySettings")]
    [SerializeField] float _transitionTime;
    [SerializeField] float velMagnitude;
    [SerializeField] Transform _handsAndGun;
    [SerializeField] Vector3 _handsAndGunDefaultPosition;
    [SerializeField] Rigidbody _playerRb;
    [SerializeField] Vector3 _rbVector;
    Vector3 maxWeaponSwayY = new Vector3(-0.01199977f, -0.5f, 0.15f);
    Vector3 minWeaponSwayY = new Vector3(-0.01199977f, -0.77f, 0.29f);
    /* Vector3 minWeaponSway = new Vector3()*/




    private void Awake()
    {
        _handsAndGunDefaultPosition = new Vector3(-0.01199977f, -0.675f, 0.3340015f);
    }
    // Start is called before the first frame update
    void Start()
    {
        //_playerRb = GetComponentInParent<Rigidbody>();
        /*_rbVector = _playerRb.velocity;
        velMagnitude = _playerRb.velocity.magnitude;*/

        shotgunAnims = GetComponent<Animator>();
        AmmoCountUI.text = shotgunScript.Ammo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _rbVector = _playerRb.velocity;
        velMagnitude = _playerRb.velocity.magnitude;




        JumpSwayControl();


        AmmoCountUI.text = shotgunScript.Ammo.ToString();


        if (Input.GetKeyDown(KeyCode.Mouse0) && shotgunScript.Ammo > 0 && shotgunScript.canShoot == true)
        {
            shotgunAnims.SetBool("shoot", true);

            shotgunScript.Ammo -= 1;
            shotgunScript.canShoot = false;

        }

        if (shotgunScript.Ammo == 0 && shotgunScript.Ammo != shotgunScript.maxAmmo /*&& shotgunAnims.GetBool("Idle") == true*/ || Input.GetKeyDown(KeyCode.R))
        {
            shotgunAnims.SetBool("reload", true);

            shotgunScript.canShoot = false;
        }
        /*else
        {
            shotgunAnims.SetTrigger("lever");
            shotgunAnims.SetTrigger("EndLoad");
        }*/
    }

    public void IdleWeapon()
    {
        shotgunScript.canShoot = true;
        shotgunAnims.SetTrigger("EndLoad");
        shotgunAnims.ResetTrigger("lever");
        shotgunAnims.SetBool("shoot", false);
    }

    public void EndLoadCheck()
    {
        shotgunAnims.ResetTrigger("EndLoad");
    }



    public void CantShoot()
    {
        shotgunScript.canShoot = false;
        canReload = false;

    }

    public void CanShoot()
    {
        shotgunScript.canShoot = true;

    }


    public void ActivateLeverPull()
    {
        shotgunAnims.SetTrigger("lever");
    }

    public void ReloadBullets()
    {
        shotgunScript.canShoot = true;

        shotgunScript.Ammo = 2;
        AmmoCountUI.text = shotgunScript.Ammo.ToString();
        shotgunAnims.SetBool("reload", false);
        //shotgunAnims.SetTrigger("EndLoad");


    }


    public void JumpSwayControl()
    {
        if (_rbVector.y > 0)
        {
            _handsAndGun.DOLocalMove(maxWeaponSwayY, _transitionTime);

        }
        else if(_rbVector.y < 0)
        {
            _handsAndGun.DOLocalMove(minWeaponSwayY, _transitionTime);

        }
        else
        {
            _handsAndGun.DOLocalMove(_handsAndGunDefaultPosition, _transitionTime);

        }
    }


}
