using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    private const float GroundCheckError = 0.01f;
    
    [SerializeField] private float _speed;
    [SerializeField] private XRNode _inputSource;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _additionHeight;

    private CharacterController _characterController;
    private InputDevice _currentDevice;
    private XROrigin _xrOrigin;
    private Vector2 _inputAxis;
    private float _verticalVelocity;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _xrOrigin = GetComponent<XROrigin>();
    }

 
    
    private void Update()
    {
        Scane();
    }

    private void FixedUpdate()
    {
        HandleHorizontalMoving();
        HandleGravity();
        
        CapsuleFollowHeadSet();
    }

    private void HandleHorizontalMoving()
    {
        Quaternion headYaw=Quaternion.Euler(0,_xrOrigin.Camera.transform.eulerAngles.y,0);
        Vector3 moveDirection = headYaw*new Vector3(_inputAxis.x, 0, _inputAxis.y);
        if (moveDirection != Vector3.zero)
        {
            _characterController.Move(moveDirection * _speed * Time.fixedDeltaTime);
        }
    }

    private void HandleGravity()
    {
        if (CheckIfGrounded())
        {
            _verticalVelocity = 0;
        }
        else
        {
            _verticalVelocity -= Physics.gravity.magnitude * Time.fixedDeltaTime;
            _characterController.Move(Vector3.up * _verticalVelocity * Time.fixedDeltaTime);
        }
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayOrigin= transform.TransformPoint(_characterController.center);
        float rayLength = _characterController.center.y + GroundCheckError;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        return Physics.SphereCast(ray, _characterController.radius, rayLength, _groundLayer);
    }

    private void CapsuleFollowHeadSet()
    {
        _characterController.height = _xrOrigin.CameraInOriginSpaceHeight + _additionHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
        _characterController.center = new Vector3(capsuleCenter.x, _characterController.height / 2+_characterController.skinWidth, capsuleCenter.z);
    }

    private void Scane()
    {
        _currentDevice = InputDevices.GetDeviceAtXRNode(_inputSource);
        _currentDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
    }
}
