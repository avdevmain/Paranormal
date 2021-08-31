using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
[SerializeField]
	private float speed = 2f;
	[SerializeField]
	private float lookSensitivity = 3f;


	// Component caching
	private PlayerMotor motor;

	private Animator animator;

	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
		animator = transform.GetChild(0).GetComponent<Animator>();
	}

	void Update ()
	{
        /*
		if (PauseMenu.IsOn)
		{
			if (Cursor.lockState != CursorLockMode.None)
				Cursor.lockState = CursorLockMode.None;

			motor.Move(Vector3.zero);
			motor.Rotate(Vector3.zero);
			motor.RotateCamera(0f);

			return;
		} */

		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

	

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

		// Animate movement
		animator.SetFloat("DirX", x);
        animator.SetFloat("DirZ", z);
        animator.SetBool("isWalking", move != Vector3.zero);

		//Apply movement
		motor.Move(move);

		//Calculate rotation as a 3D vector (turning around)
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply rotation
		motor.Rotate(_rotation);

		//Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

        if (move.x == 0 && move.z ==0f)
        {
        float turnValue = 0;
        if (_yRot>0.5f)
            turnValue = _yRot;
        else
        if (_yRot<-0.5f)
            turnValue = _yRot;
        animator.SetFloat("Turn", turnValue);
        }

		//Apply camera rotation
		motor.RotateCamera(_cameraRotationX);

	}

}
