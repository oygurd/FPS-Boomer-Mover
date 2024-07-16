using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    Checkpoint point;
    public Transform[] checkpointsToTeleportTo;
    public Transform _player;
    public Vector3 _LastCheckpoint;


    private void Awake()
    {
        point = GetComponent<Checkpoint>();
    }

    private void Update()
    {
        //_LastCheckpoint = point._checkpointPosition;

    }

    
}
