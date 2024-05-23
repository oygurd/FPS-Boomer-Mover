using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraChanges : MonoBehaviour
{
    public PlayerMovement playerMovementScript;

    [SerializeField] public float maxFov;

    [SerializeField] public float MinFov;
    // Start is called before the first frame update
    void Awake()
    {
        //maxFov = MinFov;
        //Camera.main.fieldOfView = MinFov;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftShift) && maxFov != 120)
        {
            maxFov += 1 * Time.deltaTime * 10;  
            Camera.main.fieldOfView = maxFov;
        }

        else
        {
            if(Camera.main.fieldOfView != MinFov)
            {
                Camera.main.fieldOfView -= 1 * Time.deltaTime * 10;
            }
        }*/

        if (Input.GetKey(KeyCode.LeftShift) && playerMovementScript.isGrounded == true)
        {
            maxFov = MinFov + 10;
            CameraFovChangUp();

        }
        else if (playerMovementScript.wasOnGroundBefore && Camera.main.fieldOfView != maxFov)
        {
            maxFov = MinFov + 10;
            CameraFovChangUp();
        }
        else
        {
            //Camera.main.fieldOfView = maxFov;
            CameraFovChangeDown();
        }


    }

    /*private void OnGUI()
    {
        MinFov = EditorGUILayout.Slider(MinFov, 60, 110);
    }*/

    public void CameraFovChangUp()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, maxFov, 10 * Time.deltaTime);
    }

    public void CameraFovChangeDown()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, MinFov, 10 * Time.deltaTime);
    }
}
