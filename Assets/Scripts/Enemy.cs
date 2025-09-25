using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;

    [Header("Health Settings")]
    public int maxHealth = 5;   // �⺻ ü��
    private int currentHealth;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ü���� �ִ�ü������ �ʱ�ȭ
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        // �÷��̾ ���� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // �÷��̾� ������ ȸ��
        transform.LookAt(player.position);
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}��(��) {damage} ���ظ� ����. ���� ü��: {currentHealth}");

        if (currentHealth <= 0)
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
