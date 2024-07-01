using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotgunAnim : MonoBehaviour
{
    //also the hands animations here!!!//

    Animator shotgunAnims;
    public ShotgunMechanic shotgunScript;

    public TMP_Text AmmoCountUI;

    // Start is called before the first frame update
    void Start()
    {
        shotgunAnims = GetComponent<Animator>();
        AmmoCountUI.text = shotgunScript.Ammo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        AmmoCountUI.text = shotgunScript.Ammo.ToString();


        if (Input.GetKeyDown(KeyCode.Mouse0) && shotgunScript.Ammo > 0 && shotgunScript.canShoot == true)
        {
            shotgunAnims.SetBool("shoot", true);
            /*if(shotgunScript.Ammo == 0)
            {
                shotgunAnims.SetBool("OutOfAmmo", true);
                shotgunAnims.SetBool("WeaponReload", true);
                shotgunAnims.SetBool("Idle", false);
            }*/
        }
        else
        {
            shotgunAnims.SetBool("shoot", false);

        }

        if (shotgunScript.Ammo == 0 && shotgunScript.Ammo != shotgunScript.maxAmmo && shotgunAnims.GetCurrentAnimatorStateInfo(1).IsName("Idle") || Input.GetKeyDown(KeyCode.R) )
        {
           
            shotgunAnims.SetBool("reload", true);


            shotgunScript.canShoot = false;

        }
        else
        {
            shotgunAnims.SetTrigger("lever");
            shotgunAnims.SetTrigger("EndLoad");

        }




    }

    public void IdleWeapon()
    {
        shotgunScript.canShoot = true;

        shotgunAnims.SetTrigger("EndLoad");
        //shotgunAnims.SetBool("reload", false);

        // shotgunScript.Ammo = shotgunScript.maxAmmo;
    }

    public void CantShoot()
    {
        shotgunScript.canShoot = false;

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
        shotgunScript.Ammo = 2;
        AmmoCountUI.text = shotgunScript.Ammo.ToString();
        shotgunAnims.SetBool("reload", false);
        //shotgunAnims.SetTrigger("EndLoad");


    }
}
