using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineSelection : MonoBehaviour
{
    public XRBaseInteractor leftController;
    private Transform highlight;
    private RaycastHit raycastHit;

    void Update()
    {
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
            if (highlight.CompareTag("climb"))
            {
                float distance = Vector3.Distance(leftController.transform.position, highlight.position);
                Color outlineColor;
                Color orange = new Color(1f, 0.64f, 0f);
                if (distance < 0.05f)
                {
                    outlineColor = Color.green;
                }
                else if (distance >= 0.05f && distance < 0.1f)
                {
                    outlineColor = Color.yellow;
                }
                else if (distance >= 0.1f && distance < 0.15f)
                {
                    outlineColor = orange;
                }
                else
                {
                    outlineColor = Color.red;
                }

                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }
}
