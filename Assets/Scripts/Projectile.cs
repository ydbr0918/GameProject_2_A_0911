using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;      // �̵� �ӵ�
    public float lifeTime = 2f;    // ���� �ð� (��)
    public int damage = 1;         // ����ü ������ (�⺻ 1)

    void Start()
    {
        // ���� �ð� �� �ڵ� ���� (�޸� ����)
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // ������ forward ����(��)���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Enemy ������Ʈ ��������
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // ������ 1 ����
            }

            // projectile ����
            Destroy(gameObject);
        }
    }
}
