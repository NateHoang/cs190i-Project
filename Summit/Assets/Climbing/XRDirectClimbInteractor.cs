using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDirectClimbInteractor : XRDirectInteractor
{
    public static event Action<string> ClimbHandActivated; //Signal to other scripts if climb hand is activated or deactivated and sends out a string to tell which controller is activated or deactivated
    public static event Action<string> ClimbHandDeactivated;

    private string _controllerName;

    protected override void Start()
    {
        base.Start();
        _controllerName = gameObject.name;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if(args.interactableObject.transform.gameObject.tag == "Climbable") //If grabbing onto a tag climbable, signals to other systems that climbhand is activated
        {
            ClimbHandActivated?.Invoke(_controllerName);  //Passes the controller name that detected climbing
        }
        
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ClimbHandDeactivated?.Invoke(_controllerName); //Grabs which controller exited
    }

}
