using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera virtualCamera;
	[SerializeField] Transform camRoot;
	[SerializeField] float sensivility = 30f;
	[SerializeField] float scrollSensivility = 10f;
	[SerializeField] bool follow;
	[SerializeField] float doubleClickTime = 0.5f;

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
		if (follow == false) return;
		if (isEmptyClicked == false) return;

		xAngle += lookInput.x * Time.deltaTime * sensivility;
		yAngle += lookInput.y * Time.deltaTime * sensivility;
		yAngle = Mathf.Clamp(yAngle, -50f, 50f);

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

	private void OnPosition(InputValue value)
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
					//print(mousePosInput);
					//print(Camera.main.WorldToScreenPoint(new Vector3(mousePosInput.x, mousePosInput.y, 0f)));
					playerMove.Move(Camera.main.WorldToScreenPoint(new Vector3(mousePosInput.x, mousePosInput.y, 0f)).normalized);
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
}