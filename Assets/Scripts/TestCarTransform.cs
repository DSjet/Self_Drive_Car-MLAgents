using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCarTransform : MonoBehaviour
{
    [SerializeField] private Transform carTransform;

    private void Update()
    {
        Debug.Log(carTransform.position);
    }
}
