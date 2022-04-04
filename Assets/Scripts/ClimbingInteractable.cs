
    using UnityEngine.XR.Interaction.Toolkit;

    public class ClimbingInteractable:XRBaseInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            if (args.interactorObject is XRDirectInteractor)
            {
                Climber.ClimbingHand = args.interactorObject.transform.gameObject.GetComponent<XRController>();
            }
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if (args.interactorObject is XRDirectInteractor)
            {
                if (Climber.ClimbingHand&&Climber.ClimbingHand.name==args.interactorObject.transform.gameObject.name)
                {
                    Climber.ClimbingHand = null;
                }
            }
        }
    }