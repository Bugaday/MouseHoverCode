using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverSelection : MonoBehaviour
{

    public LayerMask layerMaskSelectable;
    public Color hoveredColour;
    public Color selectedColour;

    GameObject hoveredObject = null;
    GameObject selectedObject = null;

    Ray camRay;
    RaycastHit camRayHitInfo;
    int layerInt;

    // Start is called before the first frame update
    private void Awake()
    {
        hoveredColour = Color.green;
        selectedColour = Color.magenta;
        print(layerInt);
    }

    private void Start()
    {
        //layerInt = LayerMask.NameToLayer("Selectable");
        layerInt = LayerMask.GetMask("Selectable");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHover();
        //TestHover();
    }

    void CheckForHover()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out camRayHitInfo, Mathf.Infinity,layerInt))
        {
            print("Hovered on: " + camRayHitInfo.collider.name);
            HoveredOnSelectable(camRayHitInfo.collider.gameObject);
        }
        else
        {
            print("Not hovered");
            ClearHover();
        }
    }

    void TestHover()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        print("Test layer mask" + layerMask);
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(camRay, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(camRay.origin,camRay.direction * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void HoveredOnSelectable(GameObject hitObject)
    {
        ClearHover();
        SetColour(hitObject, hoveredColour);
    }

    void ClearHover()
    {

    }

    void SelectObject()
    {

    }

    public void ClearSelection()
    {

    }

    void SetColour(GameObject targetObject, Color tint)
    {
        Renderer r = targetObject.GetComponentInChildren<Renderer>();
        r.material.color = tint;
    }

    void DeselectAll()
    {
        if (selectedObject != null)
        {
            selectedObject = null;
        }
    }
}
