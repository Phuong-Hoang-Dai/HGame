using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;

    public Transform cam;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        float gravity;
        if (playerController.isGrounded)
        {
            gravity = -.05f;
        }
        else
        {
            gravity = -9.8f;
        }

        float horizon = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizon, 0, vertical).normalized;

        if(dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle
                                            , ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, gravity, 1);
            playerController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
