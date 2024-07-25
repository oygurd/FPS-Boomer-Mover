using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    public GameObject hitMarker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator HudHitMarker()
    {     
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitMarker.SetActive(false);
    }
}
