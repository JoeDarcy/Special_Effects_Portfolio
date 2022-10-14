 using UnityEngine;
 [RequireComponent(typeof(Camera))]
 [ExecuteInEditMode]
 public class MirrorFlipCamera : MonoBehaviour
 {
	 [SerializeField] private bool flipHorizontal = false;
	 [SerializeField] private bool flipVertical = false;
	 private int xFlip = 1;
	 private int yFlip = 1;

	private Camera cameraFlip;

	private void Start()
	{
		// Get camera on game object
		cameraFlip = GetComponent<Camera>();

		// Set xFlip direction
		if (flipHorizontal)
		{
			xFlip = -1;
		}
		else
		{
			xFlip = 1;
		}

		// Set yFlip direction
		if (flipVertical)
		{
			yFlip = -1;
		}
		else
		{
			yFlip = 1;
		}
		
		Matrix4x4 mat = cameraFlip.projectionMatrix * Matrix4x4.Scale(new Vector3(xFlip, yFlip, 1));
		cameraFlip.projectionMatrix = mat;
	}
}