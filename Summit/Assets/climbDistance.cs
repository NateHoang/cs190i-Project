using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class climbDistance : MonoBehaviour
{
    public XRBaseInteractor interactor;

    void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        if (interactor != null)
            {interactor.hoverEntered.AddListener(OnHoverEntered);}
    }

    void OnDestroy()
    {
        if (interactor != null)
            {interactor.hoverEntered.RemoveListener(OnHoverEntered);}
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.CompareTag("climb"))
        {
            Debug.Log("Ray hit climbable object");
        }
    }
}
