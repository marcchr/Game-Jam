using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private TongueController line;

    private void Start()
    {
        line.SetUpLine(points);
    }

}
