using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Main Settings")]
    public Portal linkedPortal;
    public MeshRenderer screen;

    RenderTexture myTexture;
    Camera portalCam;
    Camera playerCam;

    List<PortalTraveller> listTravellers;


    void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
        listTravellers = new List<PortalTraveller>();

    }

    void Update() {
        for (int i = 0; i < listTravellers.Count; i++) {
            PortalTraveller traveller = listTravellers[i];

            Transform travellerT = traveller.transform;

            Vector3 offset = travellerT.position - transform.position;
            int Side = System.Math.Sign(Vector3.Dot(offset, transform.forward));
            int SideOld = System.Math.Sign(Vector3.Dot(traveller.getSetFromPortal, transform.forward));

            if (Side != SideOld)
            {

                var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerT.localToWorldMatrix;

                traveller.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);
                
                // Can't rely on OnTriggerEnter/Exit to be called next frame since it depends on when FixedUpdate runs
                linkedPortal.TravelEnterPortal(traveller);
                listTravellers.RemoveAt(i);
                i--;

            }
            else {
                traveller.getSetFromPortal = offset;
            }
        }

        
    }

    void defMyTexture() {
        if (myTexture == null || myTexture.width != Screen.width || myTexture.height != Screen.height)
        {
            if (myTexture != null)
            {
                myTexture.Release();
            }
            myTexture = new RenderTexture(Screen.width, Screen.height, 0);
            // Rendu de la vue de la caméra portail à la texture de la vue
            portalCam.targetTexture = myTexture;
            // Afficher la texture de la vue sur l'écran du portail lié
            linkedPortal.screen.material.SetTexture("_MainTex", myTexture);
        }
    }

    public void Render()
    {

        screen.enabled = false;

        defMyTexture();

        var matrix = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(matrix.GetColumn(3), matrix.rotation);

        portalCam.Render();

        screen.enabled = true;
    }



    void TravelEnterPortal(PortalTraveller traveller)
    {
        if (!listTravellers.Contains(traveller))
        {
            traveller.EnterPortal();
            traveller.getSetFromPortal = traveller.transform.position - transform.position;
            listTravellers.Add(traveller);
        }
    }

    void OnTriggerEnter(Collider collide){
        var traveller = collide.GetComponent<PortalTraveller>();
        if (traveller)
        {
            TravelEnterPortal(traveller);
        }
    }

    void OnTriggerExit(Collider collide)
    {
        var traveller = collide.GetComponent<PortalTraveller>();
        if (traveller && listTravellers.Contains(traveller))
        {
            traveller.ExitPortal();
            listTravellers.Remove(traveller);
        }
    }


}
