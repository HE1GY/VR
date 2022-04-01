using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportRayController : MonoBehaviour
{
    [SerializeField] private XRController _rightTeleportRay;
    [SerializeField] private XRController _leftTeleportRay;

    [SerializeField] private InputHelpers.Button _teleportActivationButton;
    [SerializeField] private float _activationthreshold=0.1f;

    [SerializeField] private XRRayInteractor _rightRayInteractor;
    [SerializeField] private XRRayInteractor _lefttRayInteractor;


    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;
    private void Update()
    {
        if (_rightTeleportRay)
        {
            bool isRightRayHovering = _rightRayInteractor.TryGetHitInfo(out Vector3 pos, out Vector3 normal,
                out int positionInLine, out bool isValidTarge);
            _rightTeleportRay.gameObject.SetActive(EnableRightTeleport&&CheckIfActivated(_rightTeleportRay)&&!isRightRayHovering);
        }
        if (_leftTeleportRay)
        {
            bool isLeftRayHovering = _lefttRayInteractor.TryGetHitInfo(out Vector3 pos, out Vector3 normal,
                out int positionInLine, out bool isValidTarge);
            _leftTeleportRay.gameObject.SetActive(EnableLeftTeleport&& CheckIfActivated(_leftTeleportRay)&&!isLeftRayHovering);
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _teleportActivationButton, out bool isActivated,_activationthreshold);
        return isActivated;
    }
}
