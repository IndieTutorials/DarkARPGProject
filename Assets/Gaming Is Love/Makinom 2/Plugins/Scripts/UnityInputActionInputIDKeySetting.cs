
// Makinom 2 Unity Input Action
// Version: 1.0.1

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

namespace GamingIsLove.Makinom
{
	[EditorSettingInfo("Unity Input Action", "An input action from the Unity input system is used.")]
	public class UnityInputActionInputIDKeySetting : BaseInputIDKeySetting
	{
		[EditorHelp("Input Action", "Select the input action that will be used.\n" +
			"The action will be enabled automatically.")]
		public AssetSource<InputActionReference> action = new AssetSource<InputActionReference>();


		// input handling
		[EditorHelp("Input Handling", "Select when the input will be recognized:\n" +
			"- Down: When the key is pressed down.\n" +
			"- Hold: While the key is held down.\n" +
			"- Up: When the key is released.\n" +
			"- Any: When the key is pressed down, held down or released.", "")]
		[EditorSeparator]
		public InputHandling handling = InputHandling.Down;

		[EditorHelp("Timeout (s)", "The time in seconds between recognizing two inputs.\n" +
			"Set to 0 to recognize the input every frame.", "")]
		[EditorLimit(0.0f, false)]
		public float inputTimeout = 0;

		[EditorHelp("Hold Time (s)", "The time in seconds the input has to be held to recognizing the input.\n" +
			"Set to 0 to recognize the input immediately.", "")]
		[EditorLimit(0.0f, false)]
		[EditorCondition("handling", InputHandling.Down)]
		[EditorElseCondition]
		[EditorEndCondition]
		[EditorDefaultValue(0.0f)]
		public float inputHoldTime = 0;


		// in-game
		private InputAction inputAction;

		private bool isCanceled = false;

		private bool isHeld = false;

		private float lastAxis = 0;

		public UnityInputActionInputIDKeySetting()
		{

		}

		public override void Clear()
		{
			this.inputAction = null;
			this.isCanceled = false;
			this.isHeld = false;
			this.lastAxis = 0;
		}


		/*
		============================================================================
		Input functions
		============================================================================
		*/
		public override float InputTimeout
		{
			get { return this.inputTimeout; }
		}

		public override float InputHoldTime
		{
			get { return this.inputHoldTime; }
		}

		public override InputHandling Handling
		{
			get { return this.handling; }
		}

		public override void TickBlocked(InputIDKey inputKey, int inputKeyID)
		{
			if(this.inputHoldTime > 0 &&
				this.inputAction != null)
			{
				if(this.isCanceled)
				{
					inputKey.HoldTimeout = -this.inputHoldTime;
				}
			}
			this.isCanceled = false;
		}

		public override void Tick(InputIDKey inputKey, int inputKeyID)
		{
			if(this.inputAction == null &&
				this.action.StoredAsset != null)
			{
				this.inputAction = this.action.StoredAsset.action;
				if(this.inputAction != null)
				{
					this.inputAction.Enable();
					this.inputAction.started += this.ActionStarted;
					this.inputAction.performed += this.ActionPerformed;
					this.inputAction.canceled += this.ActionCanceled;
				}
			}

			if(this.inputAction != null)
			{
				inputKey.UpdateAxis = this.lastAxis;

				// set input down time
				if(this.inputHoldTime > 0)
				{
					if(this.inputAction.triggered)
					{
						inputKey.HoldTimeout = Time.realtimeSinceStartup + this.inputHoldTime;
						return;
					}
					else if(this.isCanceled)
					{
						inputKey.HoldTimeout = -this.inputHoldTime;
						if(InputHandling.Hold == this.handling)
						{
							return;
						}
					}
				}

				if((InputHandling.Down == this.handling && this.inputAction.triggered) ||
					(InputHandling.Hold == this.handling && this.isHeld) ||
					(InputHandling.Up == this.handling && this.isCanceled) ||
					(InputHandling.Any == this.handling &&
						(this.inputAction.triggered || this.isHeld || this.isCanceled)))
				{
					inputKey.Timeout = Time.realtimeSinceStartup + this.inputTimeout;

					inputKey.InputReceived = true;
				}
			}
			this.isCanceled = false;
		}

		private void ToAxis(object value)
		{
			if(value is bool)
			{
				this.lastAxis = 1;
			}
			else if(value is float)
			{
				this.lastAxis = (float)value;
			}
			else if(value is Vector2)
			{
				this.lastAxis = ((Vector2)value).x;
			}
			else if(value is Vector3)
			{
				this.lastAxis = ((Vector3)value).x;
			}
		}

		private void ActionStarted(InputAction.CallbackContext context)
		{
			this.ToAxis(context.ReadValueAsObject());
			this.isHeld = false;
			this.isCanceled = false;
		}

		private void ActionPerformed(InputAction.CallbackContext context)
		{
			this.ToAxis(context.ReadValueAsObject());
			this.isHeld = true;
		}

		private void ActionCanceled(InputAction.CallbackContext context)
		{
			this.lastAxis = 0;
			this.isHeld = false;
			this.isCanceled = true;
		}
	}
}
