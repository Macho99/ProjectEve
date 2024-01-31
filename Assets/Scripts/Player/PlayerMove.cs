using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	[SerializeField] float moveSpeed = 100f;
	//[SerializeField] float rotateSpeed = 100f;
	[SerializeField] float lerpSpeed = 1f;
	private Transform rendererTrans;
	private Rigidbody rb;
	private Vector3 dir;

	private void Awake()
	{
		rendererTrans = GetComponentInChildren<MeshRenderer>().transform;
		rb = GetComponentInChildren<Rigidbody>();
	}

	public void SetDir(Vector3 dir)
	{
		this.dir = dir;
	}

	private void Update()
	{
		if (dir.sqrMagnitude < 0.1f) return;
		rendererTrans.forward = Vector3.Lerp(rendererTrans.forward, dir, lerpSpeed * Time.deltaTime);
		rb.velocity = Vector3.Lerp(rb.velocity, dir * moveSpeed, lerpSpeed * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		dir = Vector3.zero;
	}
}
