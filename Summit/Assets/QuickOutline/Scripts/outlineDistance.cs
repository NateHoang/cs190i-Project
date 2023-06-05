using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class outlineDistance : MonoBehaviour
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
 if (distance < 2f)
 {
 outlineColor = Color.green;
//  Debug.Log("distance Green");
//  Debug.Log(distance);
 }
 else if (distance <= 2.5f && distance > 2f)
 {
 outlineColor = Color.yellow;
//  Debug.Log("distance yellow");
//  Debug.Log(distance);
 }
 else if (distance <= 3.5f && distance > 2.5f)
 {
 outlineColor = orange;
//  Debug.Log("distance orange");
//  Debug.Log(distance);
 }
 else
 {
 outlineColor = Color.red;
//  Debug.Log("distance red");
 }

if (highlight.gameObject.GetComponent<Outline>() != null)
{
highlight.gameObject.GetComponent<Outline>().enabled = true;
 highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
//  Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
}
else
{
 Outline outline = highlight.gameObject.AddComponent<Outline>();
 outline.enabled = true;
 //Debug.Log(highlight.gameObject.GetComponent<Outline>().enabled);
 outline.OutlineColor = outlineColor;
 outline.OutlineWidth = 11.0f;
//  Debug.Log("Outline color: " + highlight.gameObject.GetComponent<Outline>().OutlineColor);
}
 }
 else
 {
 highlight = null;
 }
 }
 }
}
