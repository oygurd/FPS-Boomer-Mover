using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotgunAnim : MonoBehaviour
{
    //also the hands animations here!!!//

    Animator shotgunAnims;
    public Animator HandsAnims;
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
            shotgunAnims.SetBool("WeaponShoot", true);
            HandsAnims.SetBool("handShoot", true);
            /*if(shotgunScript.Ammo == 0)
            {
                shotgunAnims.SetBool("OutOfAmmo", true);
                shotgunAnims.SetBool("WeaponReload", true);
                shotgunAnims.SetBool("Idle", false);
            }*/
        }

        if (shotgunScript.Ammo == 0 && shotgunScript.Ammo != shotgunScript.maxAmmo  || Input.GetKeyDown(KeyCode.R))
        {
            shotgunAnims.SetBool("WeaponReload", true);
            HandsAnims.SetTrigger("HandsReload");
            shotgunAnims.SetBool("Idle", false);
            shotgunScript.canShoot = false;

        }




    }

    public void IdleWeapon()
    {     
        shotgunScript.canShoot = true;

        shotgunAnims.SetBool("WeaponReload", false);
        shotgunAnims.SetBool("WeaponShoot", false);
        shotgunAnims.SetBool("HandsReload", false);


        shotgunAnims.SetBool("Idle", true);
        shotgunScript.Ammo = shotgunScript.maxAmmo;
    }
    
    public void CantShoot()
    {
        shotgunScript.canShoot = false;

    }

    public void CanShoot()
    {
        shotgunScript.canShoot = true;
        HandsAnims.SetBool("handShoot", false);

    }


    public void ActivateLeverPull()
    {
        shotgunAnims.SetBool("WeaponShoot", false);

    }

    public void ReloadBullets()
    {
        shotgunScript.Ammo = 2;
        AmmoCountUI.text = shotgunScript.Ammo.ToString();

    }
}
