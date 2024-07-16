using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    [SerializeField] bool respawnAtMySpot = false;
    [SerializeField] public Vector3 _checkpointPosition;
    CheckpointsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        _checkpointPosition = transform.position;
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointsManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager._LastCheckpoint = transform.position;
        }
    }
}
