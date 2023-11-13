using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackCheckpoints : MonoBehaviour
{
    private List<Checkpoint> checkpointList;
    private int nextCheckpointSingleIndex;

    [SerializeField] private Transform carTransform;

    public event EventHandler<CarCheckpointEventArgs> OnCarCorrectCheckpoint;
    public event EventHandler<CarCheckpointEventArgs> OnCarIncorrectCheckpoint;

    public class CarCheckpointEventArgs : EventArgs
    {
        public Transform carTransform;
    }
    
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
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointSingleIndex)
        {
            Debug.Log("Correct");
            OnCarCorrectCheckpoint?.Invoke(this, new CarCheckpointEventArgs { carTransform = carTransform });
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointList.Count;
        }
        else
        {
            OnCarIncorrectCheckpoint?.Invoke(this, new CarCheckpointEventArgs {carTransform = carTransform});
            Debug.Log("Incorrect");
        }
    }

    public void ResetCheckpoint()
    {
        nextCheckpointSingleIndex = 0;
    }

    public Checkpoint GetNextCheckpoint()
    {
        return checkpointList[nextCheckpointSingleIndex];
    }
}
