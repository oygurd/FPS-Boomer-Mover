using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemdrag : MonoBehaviour
{

    SpringJoint testSpring;
    Collider coll;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        testSpring = GetComponent<SpringJoint>();
    }
    private void OnMouseDrag()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        testSpring.connectedAnchor = cursorPos;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
