

using System;
using UnityEngine;
using Animancer;

namespace RustedGames
{
	[Serializable]
	public class DirectionalAnimationSet8
	{
		[SerializeField]
		private ClipTransition _Up;
		public ClipTransition Up => _Up;

		[SerializeField]
		private ClipTransition _UpRight;
		public ClipTransition UpRight => _UpRight;

		[SerializeField]
		private ClipTransition _Right;
		public ClipTransition Right => _Right;

		[SerializeField]
		private ClipTransition _DownRight;
		public ClipTransition DownRight => _DownRight;

		[SerializeField]
		private ClipTransition _Down;
		public ClipTransition Down => _Down;

		[SerializeField]
		private ClipTransition _DownLeft;
		public ClipTransition DownLeft => _DownLeft;

		[SerializeField]
		private ClipTransition _Left;
		public ClipTransition Left => _Left;

		[SerializeField]
		private ClipTransition _UpLeft;
		public ClipTransition UpLeft => _UpLeft;


		/************************************************************************************************************************/

		/// <summary>Returns the animation closest to the specified `direction`.</summary>
		public virtual ClipTransition GetClip(Vector2 direction)
		{
			var angle = Mathf.Atan2(direction.y, direction.x);
			var octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
			switch (octant)
			{
				case 0: return _Right;
				case 1: return _UpRight;
				case 2: return _Up;
				case 3: return _UpLeft;
				case 4: return _Left;
				case 5: return _DownLeft;
				case 6: return _Down;
				case 7: return _DownRight;
				default: throw new ArgumentOutOfRangeException("Invalid octant");
			}
		}

		public enum Direction
		{
			/// <summary><see cref="Vector2.up"/>.</summary>
			Up,

			/// <summary><see cref="Vector2.right"/>.</summary>
			Right,

			/// <summary><see cref="Vector2.down"/>.</summary>
			Down,

			/// <summary><see cref="Vector2.left"/>.</summary>
			Left,

			/// <summary><see cref="Vector2"/>(0.7..., 0.7...).</summary>
			UpRight,

			/// <summary><see cref="Vector2"/>(0.7..., -0.7...).</summary>
			DownRight,

			/// <summary><see cref="Vector2"/>(-0.7..., -0.7...).</summary>
			DownLeft,

			/// <summary><see cref="Vector2"/>(-0.7..., 0.7...).</summary>
			UpLeft,
		}

		protected virtual string GetDirectionName(int direction) => ((Direction)direction).ToString();

		/************************************************************************************************************************/

		/// <summary>Returns the animation associated with the specified `direction`.</summary>
		public ClipTransition GetClip(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up: return _Up;
				case Direction.Right: return _Right;
				case Direction.Down: return _Down;
				case Direction.Left: return _Left;
				case Direction.UpRight: return _UpRight;
				case Direction.DownRight: return _DownRight;
				case Direction.DownLeft: return _DownLeft;
				case Direction.UpLeft: return _UpLeft;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual ClipTransition GetClip(int direction) => GetClip((Direction)direction);

		/************************************************************************************************************************/

		/// <summary>Sets the animation associated with the specified `direction`.</summary>
		public void SetClip(Direction direction, ClipTransition clip)
		{
			switch (direction)
			{
				case Direction.Up: _Up = clip; break;
				case Direction.Right: _Right = clip; break;
				case Direction.Down: _Down = clip; break;
				case Direction.Left: _Left = clip; break;
				case Direction.UpRight: _UpRight = clip; break;
				case Direction.DownRight: _DownRight = clip; break;
				case Direction.DownLeft: _DownLeft = clip; break;
				case Direction.UpLeft: _UpLeft = clip; break;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual void SetClip(int direction, ClipTransition clip) => SetClip((Direction)direction, clip);

		/************************************************************************************************************************/

		/// <summary>Returns a vector representing the specified `direction`.</summary>
		public static Vector2 DirectionToVector(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up: return Vector2.up;
				case Direction.Right: return Vector2.right;
				case Direction.Down: return Vector2.down;
				case Direction.Left: return Vector2.left;
				case Direction.UpRight: return Diagonals.UpRight;
				case Direction.DownRight: return Diagonals.DownRight;
				case Direction.DownLeft: return Diagonals.DownLeft;
				case Direction.UpLeft: return Diagonals.UpLeft;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual Vector2 GetDirection(int direction) => DirectionToVector((Direction)direction);

		/************************************************************************************************************************/

		/// <summary>Returns the direction closest to the specified `vector`.</summary>
		public static Direction VectorToDirection(Vector2 vector)
		{
			var angle = Mathf.Atan2(vector.y, vector.x);
			var octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
			switch (octant)
			{
				case 0: return Direction.Right;
				case 1: return Direction.UpRight;
				case 2: return Direction.Up;
				case 3: return Direction.UpLeft;
				case 4: return Direction.Left;
				case 5: return Direction.DownLeft;
				case 6: return Direction.Down;
				case 7: return Direction.DownRight;
				default: throw new ArgumentOutOfRangeException("Invalid octant");
			}
		}

		/************************************************************************************************************************/

		/// <summary>Returns a copy of the `vector` pointing in the closest direction this set type has an animation for.</summary>
		public static Vector2 SnapVectorToDirection(Vector2 vector)
		{
			var magnitude = vector.magnitude;
			var direction = VectorToDirection(vector);
			vector = DirectionToVector(direction) * magnitude;
			return vector;
		}

		public virtual Vector2 Snap(Vector2 vector) => SnapVectorToDirection(vector);

		public static class Diagonals
		{
			/************************************************************************************************************************/

			/// <summary>1 / (Square Root of 2).</summary>
			public const float OneOverSqrt2 = 0.70710678118f;

			/// <summary>A vector with a magnitude of 1 pointing up to the right.</summary>
			/// <remarks>The value is approximately (0.7, 0.7).</remarks>
			public static Vector2 UpRight => new Vector2(OneOverSqrt2, OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing down to the right.</summary>
			/// <remarks>The value is approximately (0.7, -0.7).</remarks>
			public static Vector2 DownRight => new Vector2(OneOverSqrt2, -OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing down to the left.</summary>
			/// <remarks>The value is approximately (-0.7, -0.7).</remarks>
			public static Vector2 DownLeft => new Vector2(-OneOverSqrt2, -OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing up to the left.</summary>
			/// <remarks>The value is approximately (-0.707, 0.707).</remarks>
			public static Vector2 UpLeft => new Vector2(-OneOverSqrt2, OneOverSqrt2);

			/************************************************************************************************************************/
		}

		public ClipTransition[] GetClips()
		{
			ClipTransition[] clipArray = new ClipTransition[8] { _Up, _UpRight, _Right, _DownRight, _Down, _DownLeft, _Left, _UpLeft };
			return clipArray;
		}
	}
}