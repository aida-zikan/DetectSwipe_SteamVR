using System;
using UnityEngine;

namespace zabaglione.vive {
	
	public class ViveController : MonoBehaviour {
		
		public enum SwipeDirection
		{
			NONE,
			UP,
			DOWN,
			LEFT,
			RIGHT
		};
		
		
		public event Action<SwipeDirection> OnSwipe;
		public event Action<Vector2> OnClick;
		public event Action<Vector2> OnDown;
		public event Action<Vector2> OnUp;
		public event Action<Vector2> OnDoubleClick;
		
		
		[SerializeField] private float doubleClickTime = 0.3f; 
		[SerializeField] private float swipeWidth = 0.5f;
		
		private Vector2 startPosition = Vector2.zero;
		private Vector2 endPosition = Vector2.zero;
		private Vector2 lastPosition = Vector2.zero;
		
		private float lastUpTime;
		private float lastHorizontalValue;
		private float lLastVerticalValue;
		
		public float DoubleClickTime{ get { return doubleClickTime; } }
		
		private SteamVR_TrackedObject trackedObject;
		
		void Start()
		{
			trackedObject = GetComponent<SteamVR_TrackedObject>();
		}
		
		private void Update()
		{
			CheckInput();
		}
		
		
		private void CheckInput()
		{
			SwipeDirection swipe = SwipeDirection.NONE;
			
			var device = SteamVR_Controller.Input((int) trackedObject.index);
			Vector2 position = device.GetAxis();
			
			if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
			{
				startPosition = position;
				if (OnDown != null)
					OnDown(position);
			}
			
			if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
			{
				endPosition =lastPosition;
				swipe = DetectSwipe ();
			}
			
			if (swipe != SwipeDirection.NONE && OnSwipe != null)
				OnSwipe(swipe);
			
			if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
			{
				if (OnUp != null)
					OnUp(position);
				
				if (Time.time - lastUpTime < doubleClickTime)
				{
					if (OnDoubleClick != null)
						OnDoubleClick(position);
				}
				else
				{
					if (OnClick != null)
						OnClick(position);
				}
				
				lastUpTime = Time.time;
			}
			lastPosition = position;
		}
		
		
		private SwipeDirection DetectSwipe ()
		{
			Vector2 swipeData = (endPosition - startPosition).normalized;
			//Debug.Log("swipData="+swipeData);
			bool swipeIsVertical = Mathf.Abs (swipeData.x) < swipeWidth;
			bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < swipeWidth;
			
			if (swipeData.y > 0f && swipeIsVertical)
				return SwipeDirection.UP;
			
			if (swipeData.y < 0f && swipeIsVertical)
				return SwipeDirection.DOWN;
			
			if (swipeData.x > 0f && swipeIsHorizontal)
				return SwipeDirection.RIGHT;
			
			if (swipeData.x < 0f && swipeIsHorizontal)
				return SwipeDirection.LEFT;
			
			return SwipeDirection.NONE;
		}
		
		private void OnDestroy()
		{
	            // Ensure that all events are unsubscribed when this is destroyed.
			OnSwipe = null;
			OnClick = null;
			OnDoubleClick = null;
			OnDown = null;
			OnUp = null;
		}
	}
}
