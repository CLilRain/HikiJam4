using UnityEngine;

namespace PlayerController3D
{
	[DefaultExecutionOrder(-5)]
	public class ThirdPersonLook : MonoBehaviour
	{
		[Tooltip("The target that is being followed by this camera")]
        [SerializeField]
        private Transform target;

		[Space]
        [SerializeField]
        private float panSpeed = 450f;

		[Space]
        [SerializeField]
        private float tiltSpeed = 350f;

        [SerializeField]
        private float tiltAngleMin = 2f;

        [SerializeField]
        private float tiltAngleMax = 75f;

		[Space]
        [SerializeField]
        private float zoomSpeed = 200f;

        [SerializeField]
        private float zoomDistMin = 2f;

        [SerializeField]
        private float zoomDistMax = 15f;

		private Quaternion targetRotation;
		private float yOffset = 1f;
		private float cameraDist = 5f;

		private void Awake()
		{
            var rotationWithTile = target.rotation * Quaternion.Euler(35f, 0f, 0f);
            transform.rotation = rotationWithTile;
            targetRotation = rotationWithTile;
            transform.position = target.position + rotationWithTile * new Vector3(0f, 0f, -cameraDist);
        }

		private void Update()
		{
			ZoomUpdate();
			RotationUpdate();

			transform.rotation = targetRotation;

			var offset = transform.rotation * new Vector3(0f, yOffset, -cameraDist);
            transform.position = target.position + offset;
		}

		private void RotationUpdate()
		{
			if (Input.GetMouseButton(1))
			{
                //Adjust rotation
                Vector3 euler = transform.rotation.eulerAngles;
                euler.y += Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
                euler.x -= Input.GetAxis("Mouse Y") * tiltSpeed * Time.deltaTime;
                euler.x = Mathf.Clamp(euler.x, tiltAngleMin, tiltAngleMax);

                targetRotation = Quaternion.Euler(euler.x, euler.y, 0f);
            }
		}

        private void ZoomUpdate()
		{
			cameraDist -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
			cameraDist = Mathf.Clamp(cameraDist, zoomDistMin, zoomDistMax);
		}
	}
}