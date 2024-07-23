using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StopTimerGoNextLevel : MonoBehaviour
{
    public int level;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("map" + level.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            NextLevel();
        }
    }
}
