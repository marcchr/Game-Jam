using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float searchRadius;
    [SerializeField] private float movementFrequency;
    [SerializeField] private Vector3 targetPos;

    private void Start()
    {
        StartCoroutine(NewPoint());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
    }


    IEnumerator NewPoint()
    {
        while (gameObject.activeSelf)
        {
            Vector3 randomPoint = Random.insideUnitCircle.normalized * searchRadius;
            targetPos = new Vector3(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y, 0);
            yield return new WaitForSeconds(movementFrequency);
        }
    }

    
}
