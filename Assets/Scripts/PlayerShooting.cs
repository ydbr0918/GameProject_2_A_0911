using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [System.Serializable]
    public class WeaponData
    {
        public string weaponName;            // ���� �̸� (����׿�)
        public GameObject projectilePrefab;  // �߻��� źȯ ������
        public float projectileSpeed = 10f;  // ����ü �ӵ�
    }

    public Transform firePoint;        // �߻� ��ġ
    public WeaponData[] weapons;       // ���� ����Ʈ (Inspector���� ���)
    private int currentWeaponIndex = 0;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        ShowCurrentWeapon();
    }

    void Update()
    {
        // �߻�
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // ���� ��ü (ZŰ)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWeapon();
        }
    }

    void Shoot()
    {
        if (weapons.Length == 0) return;

        WeaponData weapon = weapons[currentWeaponIndex];

        // ���콺 ��ġ�� ����ĳ��Ʈ �� �߻� ����
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // ����ü ����
        GameObject proj = Instantiate(weapon.projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        // Rigidbody �̿��� �ӵ� �ο�
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * weapon.projectileSpeed;
        }

        Debug.Log($"�߻�: {weapon.weaponName} (�ӵ�: {weapon.projectileSpeed})");
    }

    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        ShowCurrentWeapon();
    }

    void ShowCurrentWeapon()
    {
        if (weapons.Length > 0)
            Debug.Log("���� ����: " + weapons[currentWeaponIndex].weaponName);
    }
}
