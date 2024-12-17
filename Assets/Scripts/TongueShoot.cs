using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShoot : Singleton<TongueShoot>
{
    [SerializeField] private GameObject targetPos;
    [SerializeField] private GameObject startPos;
    [SerializeField] private float tongueShootSpeed;

    public bool isShooting = false;


    private void Update()
    {



    }

    IEnumerator Shoot()
    {
        isShooting = true;
        Vector3 pointA = transform.position;
        while (isShooting == true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, targetPos.transform.position, tongueShootSpeed));
            yield return StartCoroutine(MoveObject(transform, transform.position, startPos.transform.position, tongueShootSpeed));
            isShooting = false;

        }

        /* float _time = 0;
        Vector3 target = targetPos.transform.position;

        while (_time <= 1f/tongueShootSpeed)
        {
            isShooting = true;
            isRetracting = false;

            _time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos.transform.position, target, _time * tongueShootSpeed);
            yield return null;

        }

        isRetracting = true;

        */
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPosition, Vector3 endPosition, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPosition, endPosition, i);
            yield return null;
        }
    }

    public void ShootTongue()
    {
        StartCoroutine(Shoot());

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.TakeDamage(1);
        }
    }
    
}
