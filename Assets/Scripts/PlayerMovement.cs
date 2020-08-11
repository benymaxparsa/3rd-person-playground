using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    [SerializeField]
    private float _moveSpeed = 100;
    [SerializeField]
    private float _turnSpeed = 5f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontal, 0, vertical);
        _characterController.SimpleMove(movement * Time.deltaTime * _moveSpeed);

        _animator.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * _turnSpeed);
        }

    }
}
