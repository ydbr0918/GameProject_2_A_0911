using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3 : MonoBehaviour
{
    public float speed = 10f;      // �̵� �ӵ�
    public float lifeTime = 2f;    // ���� �ð� (��)
    public int damage = 10;         // ����ü ������ (�⺻ 1)

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
        if (other.CompareTag("Player"))
        {
           
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage); // ������ 1 ����
            }

        
        }
    }
}
