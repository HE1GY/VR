using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    public static XRController ClimbingHand;
    
    private ContinuousMovement _continuousMovement;
    private CharacterController _characterController;

    void Start()
    {
        _continuousMovement = GetComponent<ContinuousMovement>();
        _characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        if (ClimbingHand)
        {
            _continuousMovement.enabled = false;
            Climbe();
        }
        else
        {
            _continuousMovement.enabled = true;
        }
        
    }

    private void Climbe()
    {
        InputDevices.GetDeviceAtXRNode(ClimbingHand.controllerNode)
            .TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        _characterController.Move(transform.rotation*-velocity * Time.fixedDeltaTime);
    }
}
