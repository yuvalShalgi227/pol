using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    public float speed = 2;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float addtionalHeight = 0.2f;

    private float fallingSpeed;
    private Vector2 inputAxis;
    private CharacterController character;
    private XRRig rig;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();

        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        Debug.Log("speed: " + speed);
        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        

        Debug.Log("Before Move");

        Vector3 fall = Vector3.up * fallingSpeed * Time.fixedDeltaTime;

        Debug.Log("Move" + fall.ToString());

        character.Move(fall);
    }
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + addtionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z); 
    }
    bool CheckIfGrounded()
    {
        //tells us if we are on the ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
