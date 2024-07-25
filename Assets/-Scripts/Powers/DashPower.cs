using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPower : MonoBehaviour
{
   
    [SerializeField] float coolDownTimer = 6;


    public PlayerMovement _playerMoveScript;
    [SerializeField] float _dashStrength;
    [SerializeField] Camera forwardCam;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && coolDownTimer == 6 )
        {
            coolDownTimer -= 1 * Time.deltaTime;
            _playerMoveScript.rb.AddForce(forwardCam.transform.forward * _dashStrength, ForceMode.Impulse);

        }
        if (coolDownTimer < 6)
        {
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0)
            {
                coolDownTimer = 6;
            }

        }
        Debug.Log(coolDownTimer);
    }
}
