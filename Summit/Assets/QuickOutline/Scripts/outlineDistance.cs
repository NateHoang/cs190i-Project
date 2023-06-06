using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class outlineDistance : MonoBehaviour
{
    public XRBaseInteractor leftController;
    public XRBaseInteractor rightController;
    public VRClimb vrClimbScript;
    public Transform highlight;
    private RaycastHit raycastHit;

    private void OnEnable()
    {
        VRClimb.ClimbActive += OnClimbActive;
    }

    private void OnDisable()
    {
        VRClimb.ClimbActive -= OnClimbActive;
    }

    private void OnClimbActive()
    {
        if (highlight != null)
        {
            Color outlineColor = highlight.gameObject.GetComponent<Outline>().OutlineColor;
            float grabChance = 0f;

            if (outlineColor == Color.green)
            {
                grabChance = 1f;
            }
            else if (outlineColor == Color.yellow)
            {
                grabChance = 0.9f;
            }
            else if (outlineColor == new Color(1f, 0.64f, 0f))
            {
                grabChance = 0.8f;
                Debug.Log("Grabbed Orange Rock!");
            }
            else if (outlineColor == Color.red)
            {
                grabChance = 0f;
                Debug.Log("Grabbed Red Rock!");
            }

            if (UnityEngine.Random.Range(0f, 1f) > grabChance)
            {
                // Player fails to grab rock
                Debug.Log("Faling!!!");
                vrClimbScript.SendMessage("HandDeactivated", "LeftHand");
                vrClimbScript.SendMessage("HandDeactivated", "RightHand");
            }
        }
    }

    void Update()
    {
        // Debug.Log(leftController.transform.position);
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = new Ray(leftController.transform.position, leftController.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Climbable"))
            {
                float distance = Vector3.Distance(leftController.transform.position, highlight.position);
                Color outlineColor;
                Color orange = new Color(1f, 0.64f, 0f);
                if (distance < .5f)
                {
                    outlineColor = Color.green;
                    // Debug.Log("distance Green");
                    // Debug.Log(distance);
                }
                else if (distance <= .75f && distance > .5f)
                {
                    outlineColor = Color.yellow;
                    // Debug.Log("distance yellow");
                    // Debug.Log(distance);
                }
                else if (distance <= 1f && distance > .75f)
                {
                    outlineColor = orange;
                    // Debug.Log("distance orange");
                    // Debug.Log(distance);
                }
                else
                {
                    outlineColor = Color.red;
                    // Debug.Log("distance red");
                    // Debug.Log(distance);
                }

                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
                    // Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    //Debug.Log(highlight.gameObject.GetComponent<Outline>().enabled);
                    outline.OutlineColor = outlineColor;
                    outline.OutlineWidth = 11.0f;
                    // Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
                }
            }
            else
            {
                highlight = null;
            }
        }

        ray = new Ray(rightController.transform.position, rightController.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Climbable"))
            {
                float distance = Vector3.Distance(rightController.transform.position, highlight.position);
                Color outlineColor;
                Color orange = new Color(1f, 0.64f, 0f);
                if (distance < .5f)
                {
                    outlineColor = Color.green;
                    // Debug.Log("distance Green");
                    // Debug.Log(distance);
                }
                else if (distance <= .75f && distance > .5f)
                {
                    outlineColor = Color.yellow;
                    // Debug.Log("distance yellow");
                    // Debug.Log(distance);
                }
                else if (distance <= 1f && distance > .75f)
                {
                    outlineColor = orange;
                    // Debug.Log("distance orange");
                    // Debug.Log(distance);
                }
                else
                {
                    outlineColor = Color.red;
                    // Debug.Log("distance red");
                }

                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
                    // Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    //Debug.Log(highlight.gameObject.GetComponent<Outline>().enabled);
                    outline.OutlineColor = outlineColor;
                    outline.OutlineWidth = 11.0f;
                    // Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
}
