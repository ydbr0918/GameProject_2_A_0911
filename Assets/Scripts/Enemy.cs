using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, Runaway }

    public EnemyState state = EnemyState.Idle;

    private bool hasRunAway = false;  // �̹� Runaway �ߴ��� üũ

    public float traceRange = 20f;
    public float attackRange = 6f;

    public float attackCooldown = 1.5f;

    public GameObject projectilePrefab;

    public Transform firePoint;

    [Header("Enemy Settings")]
    public float moveSpeed = 2f;

    [Header("Health Settings")]
    public int maxHP = 5;   // �⺻ ü��
    private int currentHP;

    private Transform player;

    private float lastAttackTime;

    public Slider hpSlider;

    






    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lastAttackTime = -attackCooldown;

        // ü���� �ִ�ü������ �ʱ�ȭ
        currentHP = maxHP;
        hpSlider.value = 1f;
    }

    void Update()
    {


        if (player == null) return;

        
   

        // �÷��̾� ������ ȸ��
        transform.LookAt(player.position);

        float dist = Vector3.Distance(player.position, transform.position);

        if (currentHP <= maxHP * 0.2f && state != EnemyState.Runaway && !hasRunAway)
        {
            state = EnemyState.Runaway;
            hasRunAway = true;
        }

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;
            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;
            case EnemyState.Runaway:
                if (dist > traceRange * 2f)
                {

                    state = EnemyState.Idle;
                    StopMoving();
                }

                else
                {
                    RunawayFromPlayer();
                }
                break;
        }

        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void RunawayFromPlayer()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        transform.position += dir * moveSpeed * 2f * Time.deltaTime;
    }

    void StopMoving()
    {
        // ���� ��ġ ���� (�߰� �̵� ����)
        Vector3 pos = transform.position;
        transform.position = pos;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;
    }



    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{gameObject.name}��(��) {damage} ���ظ� ����. ���� ü��: {currentHP}");
        hpSlider.value = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} ���!");
        Destroy(gameObject); // �� ������Ʈ ����
    }
}
