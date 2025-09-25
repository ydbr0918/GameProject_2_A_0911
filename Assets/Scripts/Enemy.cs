using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;

    [Header("Health Settings")]
    public int maxHealth = 5;   // 기본 체력
    private int currentHealth;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 체력을 최대체력으로 초기화
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        // 플레이어를 향해 이동
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 플레이어 쪽으로 회전
        transform.LookAt(player.position);
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}이(가) {damage} 피해를 입음. 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject); // 적 오브젝트 제거
    }
}
