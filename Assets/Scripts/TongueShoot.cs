using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShoot : MonoBehaviour
{
    [SerializeField] private GameObject targetPos;
    [SerializeField] private GameObject startPos;
    [SerializeField] private float tongueShootSpeed;
    [SerializeField] private float tongueRetractSpeed;

    public bool isShooting = false;
    public bool isRetracting = false;


    private void Update()
    {
        if (isRetracting == true)
        {
            float _time = 0;
            _time += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, _time * tongueRetractSpeed);
            isShooting = false;
        }




    }

    IEnumerator Shoot()
    {
        float _time = 0;
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
    }

    public void ShootTongue()
    {
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
    
}
