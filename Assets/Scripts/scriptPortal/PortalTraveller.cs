using System.Collections.Generic;
using UnityEngine;

public class PortalTraveller : MonoBehaviour {

    
    public Vector3 getSetFromPortal {
        get; 
        set; }

  

    public virtual void Teleport (Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot) {
        transform.position = pos;
        transform.rotation = rot;
    }

    // fonction appelé lorsque nous touchons le portail pour la première fois
    public virtual void EnterPortal () {
        
    }

    // appelé lorsque nous ne touchons plus le portail
    public virtual void ExitPortal () {
       
    }

    

}