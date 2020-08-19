using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;

    [System.Serializable]
    public class AnimationSettings
    {
        public string veticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
        public string groundedBool = "isGrounded";
        public string jumpBool = "isJumping";
    }
    [SerializeField]
    public AnimationSettings animations;

    [System.Serializable]
    public class PhysicsSetting
    {
        public float gravityModifier = 9.81f;
        public float baseGravity = 50.0f;
        public float resetGravityValue = 1.2f;
    }
    [SerializeField]
    public PhysicsSetting physics;

    [System.Serializable]
    public class MovementSettings
    {
        public float jumpSpeed = 6;
        public float jumpTime = 0.25f;
    }
    [SerializeField]
    public MovementSettings movement;

    private bool isJumping;
    private bool resetGravity;
    private float gravity;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SetupAnimator();
    }

    void Update()
    {
        ApplyGravity();
        Animate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }


    public void Animate(float forward, float strafe)
    {
        animator.SetFloat(animations.veticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
        animator.SetBool(animations.groundedBool, characterController.isGrounded);
        animator.SetBool(animations.jumpBool, isJumping);
    }

    public void Jump()
    {
        if (isJumping)
        {
            return;
        }

        if (characterController.isGrounded)
        {
            isJumping = true;
            StartCoroutine(StopJump());
        }

    }

    private IEnumerator StopJump()
    {
        yield return new WaitForSeconds(movement.jumpTime);
        isJumping = false;
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            if (!resetGravity)
            {
                gravity = physics.resetGravityValue;
                resetGravity = true;
            }
            gravity += Time.deltaTime * physics.gravityModifier;
        }
        else
        {
            gravity = physics.baseGravity;
            resetGravity = false;
        }

        Vector3 gravityVector = new Vector3();

        if (!isJumping)
        {
            gravityVector.y -= gravity;
        }
        else
        {
            gravityVector.y = movement.jumpSpeed;
        }

        characterController.Move(gravityVector * Time.deltaTime);
    }

    private void SetupAnimator()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();

        if (animators.Length > 0)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                Animator anim = animators[i];
                Avatar avatar = anim.avatar;

                if (anim != animator)
                {
                    animator.avatar = avatar;
                    Destroy(anim);
                }
            }
        }

    }

}
