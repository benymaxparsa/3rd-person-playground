using UnityEngine;

public class UserInput : MonoBehaviour
{
    private CharacterMovement characterMove;

    [System.Serializable]
    public class InputSettings                             // Settings of input
    {
        public string verticalAxis = "Vertical";
        public string horizontalAxis = "Horizontal";
        public string jumpButton = "Jump";
    }
    [SerializeField]
    private InputSettings input;

    [System.Serializable]
    public class OtherSettings                          // Other settings related to input
    {
        public float lookSpeed = 5.0f;
        public float lookDistance = 10.0f;
        public bool requireInputForTurn = true;
    }
    [SerializeField]
    public OtherSettings other;

    private Camera mainCam;

    void Start()               // Set objs
    {
        characterMove = GetComponent<CharacterMovement>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (characterMove)               // Animate character while moving
        {

            characterMove.Animate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
            {
                characterMove.Jump();
            }
        }

        if (mainCam)        // move camera while moving
        {
            if (other.requireInputForTurn)
            {
                if (Input.GetAxis(input.horizontalAxis) != 0 || Input.GetAxis(input.verticalAxis) != 0)
                {
                    CharacterLook();
                }
            }
            else
            {
                CharacterLook();
            }
        }

    }

    private void CharacterLook()
    {
        Transform mainCamT = mainCam.transform;
        Transform pivotT = mainCamT.parent;
        Vector3 pivotPos = pivotT.position;
        Vector3 lookTarget = pivotPos + (pivotT.forward * other.lookDistance);
        Vector3 thisPos = transform.position;
        Vector3 lookDir = lookTarget - thisPos;
        Quaternion lookRot = Quaternion.LookRotation(lookDir);
        lookRot.x = 0;
        lookRot.z = 0;

        Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * other.lookSpeed);
        transform.rotation = newRotation;
    }

}
