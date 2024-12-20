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

    public float hungerDuration = 30f;
    public float hungerTimer = 30f;
    private bool isHungry = true;
    [SerializeField] Image _hungerFill;
    public float dyingTimer = 0f;
    public float dyingDuration = 10f;

    public Animator _animator;

    [SerializeField] private AudioClip shootSoundClip;

    private void Update()
    {
        if (isHungry == true)
        {
            hungerTimer -= Time.deltaTime;
            _hungerFill.fillAmount = hungerTimer / hungerDuration;
        }

        if (hungerTimer <= 0)
        {
            hungerTimer = 0;
            dyingTimer += Time.deltaTime;
            if (dyingTimer >= dyingDuration)
            {
                Kill();
            }
        }


    }

    public void Kill()
    {
        if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.AssignSurvivalTime();
            GameManager.Instance.AssignEnemiesKilled();
            GameManager.Instance.GameOver();
        }
    }

    IEnumerator Shoot(float strength)
    {
        isShooting = true;
        shootButton.interactable = false;
        tongueStrength = strength;

        _animator.SetBool("isEating", true);

        while (isShooting == true)
        {
            SoundFXManager.Instance.PlaySoundFXClip(shootSoundClip, transform, 1f);

            yield return StartCoroutine(MoveObject(transform, transform.position, targetPos.transform.position, tongueShootSpeed, tongueStrength));
            yield return StartCoroutine(MoveObject(transform, transform.position, startPos.transform.position, tongueShootSpeed, 1));
            isShooting = false;
            shootButton.interactable = true;

            _animator.SetBool("isEating", false);
            

        }


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
            hungerTimer++;
            dyingTimer = 0f;
        }

        if (other.TryGetComponent<RareEnemyController>(out var rareEnemy))
        {
            rareEnemy.TakeDamage(1);
            hungerTimer = hungerDuration;
            dyingTimer = 0f;

        }

        if (other.TryGetComponent<BadEnemyController>(out var badEnemy))
        {
            badEnemy.TakeDamage(1);
            hungerTimer+=0.5f;
            dyingTimer = 0f;

        }

    }

}
