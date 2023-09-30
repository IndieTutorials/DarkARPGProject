
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

namespace GamingIsLove.ORKFramework.Components
{
	public class ORK_Cinemachine_CameraControlWrapper : BaseCameraControl
	{
		[Tooltip("Follow the player/camera target.")]
		public bool followTarget = true;

		[Tooltip("Look at the player/camera target.")]
		public bool lookAtTarget = true;

		[Tooltip("Update the camera target when the active camera changes.\n" +
			"This'll use the current target for the new active camera.")]
		public bool updateActiveCamera = false;

		public enum CameraReset { None, LocalSpace, WorldSpace };

		[Tooltip("Reset the camera's position after restoring camera control.\n" +
			"Use 'Local Space' if the camera is a child object of the camera controller.")]
		public CameraReset resetCameraPosition = CameraReset.LocalSpace;

		[Tooltip("Reset the camera's rotation after restoring camera control.\n" +
			"Use 'Local Space' if the camera is a child object of the camera controller.")]
		public CameraReset resetCameraRotation = CameraReset.LocalSpace;

		[Tooltip("Hide the mouse cursor while the camera control is enabled.")]
		public bool hideCursor = false;

		/// <summary>
		/// The wrapped camera control component.
		/// Replace 'CinemachineBrain' with your custom camera control component's name if you're not using Cinemachine (also remove 'using Cinemachine;' and replace it with your camera control's namespace).
		/// </summary>
		protected CinemachineBrain cameraControl;

		protected Camera cameraComponent;

		protected Vector3 cameraPosition = Vector3.zero;

		protected Quaternion cameraRotation = Quaternion.identity;

		/// <summary>
		/// Marks if the initial camera target is set.
		/// </summary>
		protected bool initialChange = false;

		/// <summary>
		/// Searches for the wrapped camera control component in child and parent objects.
		/// Disables the component if no camera control is found.
		/// </summary>
		protected void Start()
		{
			this.cameraControl = this.GetComponentInChildren<CinemachineBrain>();
			if(this.cameraControl == null)
			{
				this.cameraControl = this.GetComponentInParent<CinemachineBrain>();
			}
			if(this.cameraControl == null)
			{
				this.enabled = false;
			}
			else
			{
				this.cameraComponent = this.GetComponentInChildren<Camera>();
				if(this.cameraComponent == null)
				{
					this.cameraComponent = this.GetComponentInParent<Camera>();
				}

				this.cameraControl.m_CameraActivatedEvent.AddListener(this.CameraActivatedEvent);
			}
		}

		public virtual void CameraActivatedEvent(ICinemachineCamera incoming, ICinemachineCamera outgoing)
		{
			if(this.updateActiveCamera)
			{
				this.CameraTargetChanged(null, this.CameraTarget);
			}
		}

		/// <summary>
		/// Changes the target of the wrapped camera control component, called when ORK changes the target.
		/// </summary>
		/// <param name="oldTarget">The game object of the old target.</param>
		/// <param name="newTarget">The game object of the new target.</param>
		public override void CameraTargetChanged(GameObject oldTarget, GameObject newTarget)
		{
			if(this.cameraControl != null &&
				this.cameraControl.ActiveVirtualCamera != null)
			{
				// sets the target of the wrapped component, change this with what's needed for your custom control when not using Cinemachine.
				if(newTarget != null)
				{
					if(this.followTarget)
					{
						this.cameraControl.ActiveVirtualCamera.Follow = newTarget.transform;
					}
					if(this.lookAtTarget)
					{
						this.cameraControl.ActiveVirtualCamera.LookAt = newTarget.GetComponentInChildren<RustedGames.CinemachineLookAtMe>().transform;
					}
				}
				else
				{
					if(this.followTarget)
					{
						this.cameraControl.ActiveVirtualCamera.Follow = null;
					}
					if(this.lookAtTarget)
					{
						this.cameraControl.ActiveVirtualCamera.LookAt = null;
					}
				}
			}
		}

		/// <summary>
		/// Sets the initial camera control target and handles camera target transitions.
		/// </summary>
		protected void LateUpdate()
		{

			if (this.cameraControl.ActiveVirtualCamera == null) { return; }

			if(!this.initialChange)
			{
				GameObject target = this.CameraTarget;
				if(target != null)
				{
					this.initialChange = true;
					this.CameraTargetChanged(null, target);
				}
			}

			if(this.IsCameraTargetTransition)
			{
				this.TransitionUpdate(this.transform.position, this.transform.rotation);
			}
		}

		/// <summary>
		/// Enables the wrapped camera control component.
		/// </summary>
		protected void OnEnable()
		{
			if(this.cameraComponent != null)
			{
				if(CameraReset.LocalSpace == this.resetCameraPosition)
				{
					this.cameraComponent.transform.localPosition = this.cameraPosition;
				}
				else if(CameraReset.WorldSpace == this.resetCameraPosition)
				{
					this.cameraComponent.transform.position = this.cameraPosition;
				}
				if(CameraReset.LocalSpace == this.resetCameraRotation)
				{
					this.cameraComponent.transform.localRotation = this.cameraRotation;
				}
				else if(CameraReset.WorldSpace == this.resetCameraRotation)
				{
					this.cameraComponent.transform.rotation = this.cameraRotation;
				}
			}
			if(this.cameraControl != null)
			{
				this.cameraControl.enabled = true;
			}
			if(this.hideCursor)
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

		/// <summary>
		/// Disables the wrapped camera control component.
		/// </summary>
		protected void OnDisable()
		{
			if(this.cameraComponent != null)
			{
				if(CameraReset.LocalSpace == this.resetCameraPosition)
				{
					this.cameraPosition = this.cameraComponent.transform.localPosition;
				}
				else if(CameraReset.WorldSpace == this.resetCameraPosition)
				{
					this.cameraPosition = this.cameraComponent.transform.position;
				}
				if(CameraReset.LocalSpace == this.resetCameraRotation)
				{
					this.cameraRotation = this.cameraComponent.transform.localRotation;
				}
				else if(CameraReset.WorldSpace == this.resetCameraRotation)
				{
					this.cameraRotation = this.cameraComponent.transform.rotation;
				}
			}
			if(this.cameraControl != null)
			{
				this.cameraControl.enabled = false;
			}
			if(this.hideCursor)
			{
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}
}
