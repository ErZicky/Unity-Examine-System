using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectScript : MonoBehaviour
{
    [SerializeField] private float distance; //Raycast max distance
    [SerializeField] private Transform InspectArea; //A gameobject child of the camera where the object will be moved and rotated
    [SerializeField] private float rotationSpeed = 125f;
    [SerializeField] private float dropSpeed = 0.2f;
    [SerializeField] private float pickupSpeed = 0.2f;
    [SerializeField] private bool InvertMouse = false;
    [SerializeField] private string TargetTag = "Object";
    [Header("Using the FPS camera is suggested")]
    [SerializeField] private Transform RayOrigin;
    [Header("Change 'PlayerController' in the code with your own FPS controller script")]
    [SerializeField] private PlayerController PlayerControllerScript; //my fps controller change it with yours



    private Vector3 originalPos;
    private bool Inspecting = false;
    private GameObject InspectedObj;



    void Update()
    {

      if(Input.GetKeyDown(KeyCode.Mouse0))
      {
        Vector3 fwdV = RayOrigin.TransformDirection(Vector3.forward);
        Debug.DrawRay(RayOrigin.position, fwdV * distance, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(RayOrigin.position, fwdV, out hit, distance))
        {
          if(hit.transform.tag == TargetTag && !Inspecting)
          {

            InspectedObj = hit.transform.gameObject;
            originalPos = hit.transform.position;
            Inspecting = true;
            StartCoroutine(pickupItem());

          }
        }
      }



      if(Inspecting)
      {
        InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, InspectArea.position, pickupSpeed);

        if(!InvertMouse)
        InspectArea.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)* Time.deltaTime * rotationSpeed);
        else
        InspectArea.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0)* Time.deltaTime * rotationSpeed);

      }
      else if(InspectedObj != null)
      {
        InspectedObj.transform.SetParent(null);
        InspectArea.rotation = Quaternion.identity;
        InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, originalPos, dropSpeed);
      }

      if(Input.GetKeyDown(KeyCode.Mouse1) && Inspecting)
      {
        Inspecting = false;
        StartCoroutine(droppItem());

      }


    }

    IEnumerator pickupItem()
    {
        
      //collider and rigidbodies cause problem during the inspection so we disable them during it  
      if(InspectedObj.GetComponent<Collider>())
      {
        InspectedObj.GetComponent<Collider>().enabled = false;
      }
      if(InspectedObj.GetComponent<Rigidbody>())
      {
        InspectedObj.GetComponent<Rigidbody>().useGravity = false;
      }

      PlayerControllerScript.enabled = false;
      yield return new WaitForSeconds(0.2f);
      InspectedObj.transform.SetParent(InspectArea);
    }

    IEnumerator droppItem()
    {
      InspectedObj.transform.rotation = Quaternion.identity;
      yield return new WaitForSeconds(0.2f);
      PlayerControllerScript.enabled = true;

      InspectedObj.transform.SetParent(InspectArea);

      if(InspectedObj.GetComponent<Collider>())
      {
        InspectedObj.GetComponent<Collider>().enabled= true;
      }
      if(InspectedObj.GetComponent<Rigidbody>())
      {
        InspectedObj.GetComponent<Rigidbody>().useGravity = true;
      }
    }

}
