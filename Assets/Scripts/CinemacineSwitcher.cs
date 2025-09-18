using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemacineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public CinemachineFreeLook freeLookCam;

    public bool usingFreeLook = false;

    // Start is called before the first frame update
    void Start()
    {
        virtualCam.Priority = 10;
        freeLookCam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))         //우클릭
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookCam.Priority = 20;    //FreeLook 활성화
                virtualCam.Priority = 0;
            }
            else
            {
                virtualCam.Priority = 20;  //virtual camera 활성화
                freeLookCam.Priority = 0;
            }
        }
    }
}
