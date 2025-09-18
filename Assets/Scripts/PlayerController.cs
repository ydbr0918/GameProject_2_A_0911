using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("이동 관련")]
    public float walkSpeed = 5f;
    public float runSpeed = 15f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;

    [Header("카메라 관련")]
    public CinemachineVirtualCamera virtualCam;
    public float runFOV = 90f; // 달리기 시 카메라 시야각
    private float defaultFOV;

    private CinemachinePOV pov;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // 외부에서 가져올 스위처
    private CinemacineSwitcher camSwitcher;

    // 내부 상태
    private float currentSpeed;
    private bool isRunning = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
        camSwitcher = FindObjectOfType<CinemacineSwitcher>();

        defaultFOV = virtualCam.m_Lens.FieldOfView;
        currentSpeed = walkSpeed;
    }

    void Update()
    {
      
        if (camSwitcher != null && camSwitcher.usingFreeLook)
        {
            controller.Move(Vector3.zero); // 강제로 움직임 차단
            return;
        }

        // 땅 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 카메라 기준 방향
        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && move.magnitude > 0.1f)
        {
            isRunning = true;
            currentSpeed = runSpeed;
            virtualCam.m_Lens.FieldOfView = Mathf.Lerp(virtualCam.m_Lens.FieldOfView, runFOV, Time.deltaTime * 5f);
        }
        else
        {
            isRunning = false;
            currentSpeed = walkSpeed;
            virtualCam.m_Lens.FieldOfView = Mathf.Lerp(virtualCam.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 5f);
        }

        // 이동
        controller.Move(move * currentSpeed * Time.deltaTime);

        // 캐릭터 회전
        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);

        // 점프
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        // 중력
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
