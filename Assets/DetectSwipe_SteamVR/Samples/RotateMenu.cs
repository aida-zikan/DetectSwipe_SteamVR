using UnityEngine;
using System.Collections;
using zabaglione.vive;

public class RotateMenu : MonoBehaviour {
	
	[SerializeField] private float second=0.3f;
	[SerializeField] GameObject target;
	
	private bool isRotate;
	
	void Start () {
		ViveController vc = GetComponent<ViveController>();
		vc.OnSwipe += OnSwipe;
		isRotate = false;
	}
	
	void OnSwipe(ViveController.SwipeDirection dir) {
		if (isRotate == false) {
			isRotate = true;
			StartCoroutine(Rotation(dir));
		}
	}
	
	IEnumerator Rotation(ViveController.SwipeDirection dir) {
		float angleDir = 0.0f;
		switch(dir) {
		case ViveController.SwipeDirection.LEFT:
			angleDir = 1.0f;
			break;
		case ViveController.SwipeDirection.RIGHT:
			angleDir = -1.0f;
			break;
		}
		//Debug.Log("dir="+ dir+" angle="+angle);
		
		float angle = 0.0f;
		while(angle < 90.0f){
			float deltaAngle = 90 * Time.deltaTime * (1 / second);
			target.transform.Rotate(new Vector3(0 ,0 , angleDir * deltaAngle));
			angle += deltaAngle;
			yield return null;
		}
		isRotate = false;
		yield return null;
	}
}
