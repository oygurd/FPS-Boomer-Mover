using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public TMP_Text _time;
    public bool startTimer = false;
    [SerializeField] float _timer;

    private void Awake()
    {
        _time = GetComponent<TMP_Text>();
        
        _time.text = "Time:" + _timer.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            _timer += 1 * Time.deltaTime;
        }
           

        _time.text = "Time:" + _timer.ToString("F3");
        


    }
}
