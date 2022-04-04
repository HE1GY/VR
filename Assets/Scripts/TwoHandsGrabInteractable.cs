using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandsGrabInteractable : XRGrabInteractable
{
    [SerializeField] private List<XRSimpleInteractable> _seconsInteractablePoints = new List<XRSimpleInteractable>();
    private IXRSelectInteractor _secondInteractor; 

    private void Start()
    {
        foreach (var item in _seconsInteractablePoints)
        {
                item.selectEntered.AddListener(OnSecondHandGrab);
                item.selectExited.AddListener(OnSecondHandRelease);
        }
    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (_secondInteractor != null && firstInteractorSelecting != null)
        {
            transform.forward = _secondInteractor.transform.position - firstInteractorSelecting.transform.position;
        }
        base.ProcessInteractable(updatePhase);
    }

    private void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        _secondInteractor = args.interactorObject;
    }

    private void OnSecondHandRelease(SelectExitEventArgs args)
    {
        _secondInteractor =null;
    }

    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _secondInteractor = null;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isalreadyGrab = firstInteractorSelecting!=null && !interactor.Equals(firstInteractorSelecting);
        return base.IsSelectableBy(interactor)&&!isalreadyGrab;
    }
}
