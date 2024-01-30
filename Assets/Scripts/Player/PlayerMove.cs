using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	[SerializeField] float moveSpeed = 100f;
	[SerializeField] float rotateSpeed = 100f;
	private Transform rendererTrans;
	private Rigidbody rb;

	private void Awake()
	{
		rendererTrans = GetComponentInChildren<MeshRenderer>().transform;
		rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 dir)
	{
		rb.velocity = dir * moveSpeed;
		rendererTrans.forward = dir;
		print(dir);
	}
}
