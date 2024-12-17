using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TongueShoot : Singleton<TongueShoot>
{
    [SerializeField] private GameObject targetPos;
    [SerializeField] private GameObject startPos;
    [SerializeField] private float tongueShootSpeed;
    public float tongueStrength;

    public bool isShooting = false;
    public Button shootButton;

    private void Update()
    {



    }

    IEnumerator Shoot(float strength)
    {
        isShooting = true;
        shootButton.interactable = false;
        tongueStrength = strength;
        while (isShooting == true)
        {
            yield return StartCoroutine(MoveObject(transform, transform.position, targetPos.transform.position, tongueShootSpeed, tongueStrength));
            yield return StartCoroutine(MoveObject(transform, transform.position, startPos.transform.position, tongueShootSpeed, 1));
            isShooting = false;
            shootButton.interactable = true;
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

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPosition, Vector3 endPosition, float time, float strength)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f * strength)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPosition, endPosition, i);
            yield return null;
        }
    }

    public void ShootTongue(float strength)
    {
        StartCoroutine(Shoot(strength));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.TakeDamage(1);
        }

        if (other.TryGetComponent<RareEnemyController>(out var rareEnemy))
        {
            rareEnemy.TakeDamage(1);
        }

        if (other.TryGetComponent<BadEnemyController>(out var badEnemy))
        {
            badEnemy.TakeDamage(1);
        }

    }

}
