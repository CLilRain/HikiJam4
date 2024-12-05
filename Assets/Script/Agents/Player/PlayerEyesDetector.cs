using UnityEngine;

public class PlayerEyesDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public float detectionRange = 5f;

    public DetectionReceiver foundObject { get; private set; }
    public bool hasFoundObject { get; private set; } = false;

    public void DetectItem()
    {
        hasFoundObject = false;

        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, layerMask))
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.yellow);

            Collider collider = hit.collider;
            if (collider != null)
            {
                var receiver = collider.GetComponent<DetectionReceiver>();
                if (receiver != null)
                {
                    hasFoundObject = true;
                    foundObject = receiver;
                    receiver.OnDetection();
                    return;
                }
            }
            else
            {
                Debug.Log($"Detected object {hit.transform.name} has no collider ");
            }
        }

        foundObject = null;
        UIManager.Instance.ClearDetectedInfo();
    }
}