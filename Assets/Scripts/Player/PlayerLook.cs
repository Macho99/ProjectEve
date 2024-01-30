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

	Cinemachine3rdPersonFollow cine3rdPersonFollow;
	float scrollInput;
	Vector2 lookInput;
	float yAngle;
	float xAngle;

	private void Awake()
	{
		cine3rdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
		//GameManager.Instance.OnFocus.AddListener(AutoEnable);
	}

	private void OnDestroy()
	{
		//GameManager.Instance.OnFocus.RemoveListener(AutoEnable);
	}

	private void LateUpdate()
	{
		Look();
	}

	private void Look()
	{
		if (follow == false) return;
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
}
