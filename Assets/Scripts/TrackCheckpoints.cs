using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private List<Checkpoint> checkpointList;
    private int nextCheckpointSingleIndex;

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointList = new List<Checkpoint>();

        foreach (Transform checkpoint in checkpointsTransform)
        {
            Checkpoint checkpointSingle = checkpoint.GetComponent<Checkpoint>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndex = 0;
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        if(checkpointList.IndexOf(checkpoint) == nextCheckpointSingleIndex)
        {
            Debug.Log("Correct");
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointList.Count;
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
