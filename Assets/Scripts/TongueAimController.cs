using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAimController : MonoBehaviour
{
    public float tongueLength;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject target;

    private void Start()
    {
        target.transform.position = new Vector3(transform.position.x, transform.position.y + tongueLength, 0);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAmount * Mathf.Sin(Time.time * rotationSpeed));
    }


}
