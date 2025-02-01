using UnityEngine;

[DefaultExecutionOrder(-5)]
public class InteractableDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public float detectionRange = 6f;

    private Vector3[] offsets = new Vector3[2]
    {
        new Vector3(0f, 0.2f, 0f),
        new Vector3(0f, 0.8f, 0f),
    };

    public Interactable DetectedObject { get; protected set; }
    public bool HasDetectedObject => DetectedObject != null;

    private void Update()
    {
        RaycastHit hit;

        foreach (var offset in offsets)
        {
            if (Raycast(transform.position + offset, out hit))
            {
                return;
            }
        }

        ClearInteractable();
    }

    private bool Raycast(Vector3 origin, out RaycastHit hit)
    {
        Debug.DrawRay(origin - transform.forward, transform.forward * detectionRange, Color.yellow);

        if (Physics.Raycast(origin - transform.forward, transform.forward, out hit, detectionRange, layerMask))
        {
            Debug.DrawLine(origin - transform.forward, hit.transform.position, Color.red);

            Collider collider = hit.collider;
            if (collider != null)
            {
                var interactable = collider.GetComponent<Interactable>();
                if (interactable != null && interactable.IsInteractable)
                {
                    SetInteractable(interactable);

                    //Debug.Log($"Detected object {hit.transform.name} ");

                    return true;
                }
            }
            else
            {
                //Debug.Log($"{hit.transform.name} has no collider ");
            }
        }

        return false;
    }

    protected void SetInteractable(Interactable interactable)
    {
        DetectedObject = interactable;

        UIManager.Instance.SetDetectedInfo(interactable.ObjectName);

        //Debug.Log($"COLLISION - Player collided with {interactable.name}");
    }

    protected void ClearInteractable()
    {
        DetectedObject = null;

        UIManager.Instance.ClearDetectedInfo();

        Debug.Log("Cleare detected item");
    }
}