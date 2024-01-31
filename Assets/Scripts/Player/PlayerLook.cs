using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Screen = UnityEngine.Screen;

public class PlayerLook : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera virtualCamera;
	[SerializeField] Transform camRoot;
	[SerializeField] float sensivility = 30f;
	[SerializeField] float scrollSensivility = 10f;
	[SerializeField] float doubleClickTime = 0.5f;
	[SerializeField] float angleMultiple = 0.05f;

	PlayerMove playerMove;
	Cinemachine3rdPersonFollow cine3rdPersonFollow;
	float scrollInput;
	Vector2 mousePosInput;
	Vector2 lookInput;
	float yAngle;
	float xAngle;

	float lastEmptyClickTime;
	bool isEmptyClicked;

	private void Awake()
	{
		playerMove = GetComponent<PlayerMove>();
		cine3rdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
	}

	private void Update()
	{
		Look();
	}

	private void Look()
	{
		if (isEmptyClicked == false) return;

		xAngle += lookInput.x * Time.deltaTime * sensivility;
		yAngle += lookInput.y * Time.deltaTime * sensivility;
		yAngle = Mathf.Clamp(yAngle, -70f, 70f);

		camRoot.rotation = Quaternion.Euler(new Vector3(-yAngle, xAngle, 0f));
	}

	private void OnLook(InputValue value)
	{
		lookInput = value.Get<Vector2>();
	}

	private void OnScroll(InputValue value)
	{
		scrollInput = value.Get<float>();
		cine3rdPersonFollow.CameraDistance -= scrollInput * Time.deltaTime * scrollSensivility;
	}

	private void OnMousePosition(InputValue value)
	{
		mousePosInput = value.Get<Vector2>();
	}

	private void OnLeftClick(InputValue value)
	{
		bool clicked = value.Get<float>() > 0.9f ? true : false;
		if (clicked == true)
		{
			if(EventSystem.current.IsPointerOverGameObject() == false)
			{
				isEmptyClicked = true;

				if(Time.time < lastEmptyClickTime + doubleClickTime)
				{
					//DoubleClicked
				}
				else
				{
					lastEmptyClickTime = Time.time;
				}
			}
		}
		else
		{
			isEmptyClicked = false;
		}
	}

	private void OnRightClick(InputValue value)
	{
		bool clicked = value.Get<float>() > 0.9f ? true : false;
		if (clicked == true)
		{
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				RightClickMove(mousePosInput);
			}
		}
	}

	private void RightClickMove(Vector2 mousePos)
	{
		float diffX = mousePos.x - Screen.width * 0.5f;
		float diffY = mousePos.y - Screen.height * 0.5f;

		Vector3 cameraForward = Camera.main.transform.forward;

		Quaternion rotationY = Quaternion.AngleAxis(diffX * angleMultiple, Camera.main.transform.up);
		Quaternion rotationX = Quaternion.AngleAxis(-diffY * angleMultiple, Camera.main.transform.right);
		playerMove.SetDir(rotationX * rotationY * cameraForward);
	}
}