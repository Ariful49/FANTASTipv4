using UnityEngine;
using System.Collections;

public class SwipeCamera : MonoBehaviour {
	public float moveSensitivityX = 1.0f;
	public float moveSensitivityY = 1.0f;
	public bool invertMoveX = false;
	public bool invertMoveY = false;
	public float camWidth;
	public float camHeight;

	private Camera _camera;
	private float horizontalExtent, verticalExtent;
	private float minX, maxX,minY,maxY;

	void Start(){
		_camera = Camera.main;
		CalculateCamBound ();
	}

	void Update(){

		Touch[] touches = Input.touches;
		if (touches.Length == 1) {
			if (touches [0].phase == TouchPhase.Moved) {
				Vector2 delta = touches [0].deltaPosition;

				float positionX = delta.x * moveSensitivityX * Time.deltaTime;
				positionX = invertMoveX ? positionX : positionX * -1;

				float positionY = delta.y * moveSensitivityY * Time.deltaTime;
				positionY = invertMoveY ? positionY : positionY * -1;

				_camera.transform.position += new Vector3 (positionX, positionY, 0);

				CalculateCamBound ();
			}
		}
	}

	void CalculateCamBound(){
		verticalExtent = _camera.orthographicSize;
		horizontalExtent = verticalExtent * _camera.aspect;
		minX = horizontalExtent - camWidth / 2.0f;
		maxX = camWidth / 2.0f - horizontalExtent;
		minY = verticalExtent - camHeight / 2.0f;
		maxY = camHeight / 2.0f - verticalExtent;
	}

	void LateUpdate(){
		Vector3 limitedCameraPosition = _camera.transform.position;
		limitedCameraPosition.x = Mathf.Clamp (limitedCameraPosition.x, minX, maxX);
		limitedCameraPosition.y = Mathf.Clamp (limitedCameraPosition.y, minY, maxY);
		_camera.transform.position = limitedCameraPosition;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (camWidth, camHeight, 0));
	}
}
