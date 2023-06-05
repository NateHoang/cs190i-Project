using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractorHover : MonoBehaviour
{
    public XRBaseInteractor rayInteractor;

    void OnEnable()
    {
        rayInteractor.hoverEntered.AddListener(HoverEntered);
    }

    void OnDisable()
    {
        rayInteractor.hoverEntered.RemoveListener(HoverEntered);
    }

    private void HoverEntered(HoverEnterEventArgs args)
    {
        // if (args.interactable.CompareTag("climb"))
        // {
            Debug.Log("Ray Interactor is pointing to: " + args.interactable.name);
        // }
    }
}
