using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public float speed = 10f;      // �̵� �ӵ�
    public float lifeTime = 5f;    // ���� �ð� (��)
    public int damage = 3;         // ����ü ������ (�⺻ 1)

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
                enemy.TakeDamage(damage); 
            }

            // projectile ����
            Destroy(gameObject);
        }
    }
}
