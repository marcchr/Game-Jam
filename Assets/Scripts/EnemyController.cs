using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data;

    [SerializeField] private Vector3 targetPos;

    FlySpawner _spawner;

    float _currentHealth;
    float _currentMovementSpeed;

    [SerializeField] private AudioClip dieSoundClip;

    public void Initialize(FlySpawner spawner, EnemyData data)
    {
        _spawner = spawner;
        Data = data;
    }

    private void OnEnable()
    {
        ResetToDefault();
        StartCoroutine(NewPoint());
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _currentMovementSpeed * Time.deltaTime);
    }

    public void ResetToDefault()
    {
        _currentHealth = Data.maxHealth;
        _currentMovementSpeed = Data.movementSpeed;
    }

    IEnumerator NewPoint()
    {
        while (_currentHealth>0)
        {
            Vector3 randomPoint = Random.insideUnitCircle.normalized * Data.searchRadius;
            targetPos = new Vector3(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y, 0);
            yield return new WaitForSeconds(Data.movementFrequency);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0f)
        {
            Die();
            SoundFXManager.Instance.PlaySoundFXClip(dieSoundClip, transform, 1f);
            GameManager.Instance.killCount++;
            GameManager.Instance.currentScore += Data.pointsWorth;
            _spawner.ReturnEnemyToPool(this);

        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bounds"))
        {
            Die();
            _spawner.ReturnEnemyToPool(this);
        }
    }

}
