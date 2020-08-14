using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    [SerializeField]
    private float _forwardMoveSpeed = 7.5f;

    [SerializeField]
    private float _backwardMoveSpeed = 3;

    [SerializeField]
    private float _turnSpeed = 150f;

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

        _animator.SetFloat("Speed", vertical);

        transform.Rotate(Vector3.up, horizontal * _turnSpeed * Time.deltaTime);

        if (vertical != 0)
        {
            float moveSpeedToUse = vertical > 0 ? _forwardMoveSpeed : _backwardMoveSpeed;
            _characterController.SimpleMove(transform.forward * moveSpeedToUse * vertical);
        }

    }
}
