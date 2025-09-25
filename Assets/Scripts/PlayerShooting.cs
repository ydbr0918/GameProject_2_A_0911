using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [System.Serializable]
    public class WeaponData
    {
        public string weaponName;            // 무기 이름 (디버그용)
        public GameObject projectilePrefab;  // 발사할 탄환 프리팹
        public float projectileSpeed = 10f;  // 투사체 속도
    }

    public Transform firePoint;        // 발사 위치
    public WeaponData[] weapons;       // 무기 리스트 (Inspector에서 등록)
    private int currentWeaponIndex = 0;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        ShowCurrentWeapon();
    }

    void Update()
    {
        // 발사
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // 무기 교체 (Z키)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWeapon();
        }
    }

    void Shoot()
    {
        if (weapons.Length == 0) return;

        WeaponData weapon = weapons[currentWeaponIndex];

        // 마우스 위치로 레이캐스트 → 발사 방향
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // 투사체 생성
        GameObject proj = Instantiate(weapon.projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        // Rigidbody 이용해 속도 부여
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * weapon.projectileSpeed;
        }

        Debug.Log($"발사: {weapon.weaponName} (속도: {weapon.projectileSpeed})");
    }

    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        ShowCurrentWeapon();
    }

    void ShowCurrentWeapon()
    {
        if (weapons.Length > 0)
            Debug.Log("현재 무기: " + weapons[currentWeaponIndex].weaponName);
    }
}
