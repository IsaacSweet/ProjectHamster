using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSettings : MonoBehaviour {
    public static CharacterSettings Instance;

    [Header("Movement Settings")]
    public float characterGroundSpeed;
    public float characterAirSpeed;
    public bool gravity;
    public float gravitySpeed;
    public float gravityPercent;

    [Header("Character Controller Settings")]
    public float slopeLimit;
    public float stepOffset;
    public float skinWidth;
    public float minMoveDist;
    public Vector3 center;
    public float radius;
    public float height;

    public CharacterController controller;

    [Header("Rotation Settings")]
    public float rotationSpeed;
    public float directionZX;

    public float cameraUpMax;
    public float cameraDownMax;
    public float cameraVerticalAngle;

    [Header("Interaction Settings")]
    public UnityEvent mainAction;

    public GameObject head;
	// Use this for initialization
	void Start () {
        Initialize();
    }
	
    public void Initialize()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Destroy(this.gameObject);
        }

        CharacterControllerSetUp();

        if(head != null)
        {
            head.transform.rotation =   Quaternion.AngleAxis(directionZX, Vector3.up) * 
                                        Quaternion.AngleAxis(cameraVerticalAngle, Vector3.left) ;
        }

        gravity = true;

        Instance = this;
    }

    public void CharacterControllerSetUp()
    {
        if (slopeLimit != 0)
        {
            controller.slopeLimit = slopeLimit;
        }
        else
        {
            slopeLimit = controller.slopeLimit;
        }
        if (stepOffset != 0)
        {
            controller.stepOffset = stepOffset;
        }
        else
        {
            stepOffset = controller.stepOffset;
        }
        if (skinWidth != 0)
        {
            controller.skinWidth = skinWidth;
        }
        else
        {
            skinWidth = controller.skinWidth;
        }
        if (minMoveDist != 0)
        {
            controller.minMoveDistance = minMoveDist;
        }
        else
        {
            minMoveDist = controller.minMoveDistance;
        }
        if (center != Vector3.zero)
        {
            controller.center = center;
        }
        else 
        {
            center = controller.center;
        }
        if (radius != 0)
        {
            controller.radius = radius;
        }
        else
        {
            radius = controller.radius;
        }
        if (height != 0)
        {
            controller.height = height;
        }
        else
        {
            height = controller.height;
        }
    }

	// Update is called once per frame
	void Update () {
        if (head != null)
        {
            head.transform.rotation =   Quaternion.AngleAxis(directionZX, Vector3.up) *
                                        Quaternion.AngleAxis(cameraVerticalAngle, Vector3.left);
        }

        if(controller.isGrounded)
        {
            gravityPercent = 0;
        }
        else if(!controller.isGrounded && gravity)
        {
            gravityPercent += Time.deltaTime;
            gravityPercent = Mathf.Clamp01(gravityPercent);
            controller.Move(Vector3.down * gravitySpeed * gravityPercent * Time.smoothDeltaTime);
        }
    }

    public void Move(Vector3 direction)
    {
        Vector3 worldDirection = Quaternion.AngleAxis(directionZX, Vector3.up) * direction;
        float speedMod = 0;
        if(controller.isGrounded)
        {
            speedMod = characterGroundSpeed;
        }
        else
        {
            speedMod = characterAirSpeed;
        }
        controller.Move(worldDirection * speedMod * Time.smoothDeltaTime);
    }

    public void Rotate(float yRot, float xRot = 0)
    {
        directionZX += yRot * rotationSpeed * Time.smoothDeltaTime;

        cameraVerticalAngle += xRot * rotationSpeed * Time.smoothDeltaTime;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, cameraDownMax, cameraUpMax);
    }
}
