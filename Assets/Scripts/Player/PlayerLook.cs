using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

	[SerializeField] Transform camRoot;
	[SerializeField] float sensivility = 10f;
	[SerializeField] float characterRotationSpeed = 10f;
	[SerializeField] bool follow;

	Vector2 lookInput;
	float yAngle;
	float xAngle;

	private void Awake()
	{
		//GameManager.Instance.OnFocus.AddListener(AutoEnable);
	}

	private void OnDestroy()
	{
		//GameManager.Instance.OnFocus.RemoveListener(AutoEnable);
	}

	private void AutoEnable(bool focus)
	{
		enabled = focus;
	}

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
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
}
