using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetInteractable : XRGrabInteractable
{

    private Vector3 _initialAttachLocalPos;
    private Quaternion _initialAttachLocalRot;
    private void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Pivot");
            grab.transform.SetParent(transform,false);
            attachTransform = grab.transform;
        }

        _initialAttachLocalPos = attachTransform.position;
        _initialAttachLocalRot = attachTransform.rotation;
    }


    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        if (interactor.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = interactor.interactorObject.transform.position;
            attachTransform.rotation = interactor.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = _initialAttachLocalPos;
            attachTransform.localRotation= _initialAttachLocalRot;
        }
        
        base.OnSelectEntered(interactor);
    }
}
