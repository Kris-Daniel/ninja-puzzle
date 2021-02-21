using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Player
{
	public class MouseLook : MonoBehaviour
	{
		[SerializeField] Transform playerBody;
		[SerializeField] float mouseSensitivity = 100f;

		float xRotation = 0;
	
		// Start is called before the first frame update
		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		// Update is called once per frame
		void Update()
		{
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);
	    
			transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
			playerBody.Rotate(Vector3.up * mouseX);
		}
	}
}
