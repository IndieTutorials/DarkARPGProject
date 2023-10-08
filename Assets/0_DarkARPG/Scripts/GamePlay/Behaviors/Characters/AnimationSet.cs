/*
The MIT License
https://opensource.org/license/mit/
Copyright © 2023 Rusted Games
*/


using UnityEngine;
using Animancer;

namespace RustedGames.Data
{
	[CreateAssetMenu(menuName ="DarkARPG/AnimationSet", fileName ="New AnimationSet", order = 0)]
	public class AnimationSet : ScriptableObject
	{
		[Header("Locomotion")]
		[SerializeField]
		private ClipTransition _Idle;
		public ClipTransition Idle => _Idle;

		[SerializeField]
		private ClipTransition _Walk;
		public ClipTransition Walk => _Walk;

		[SerializeField]
		private ClipTransition _Jog;
		public ClipTransition Jog => _Jog;


		[SerializeField]
		private ClipTransition _Sprint;
		public ClipTransition Sprint => _Sprint;


		[SerializeField]
		private ClipTransition _Jump;
		public ClipTransition Jump => _Jump;


		[SerializeField]
		private ClipTransition _BackStep;
		public ClipTransition BackStep => _BackStep;

		[SerializeField]
		private ClipTransition _Roll;
		public ClipTransition Roll => _Roll;

		[Header("Dash Animations")]
		[SerializeField]
		private DirectionalAnimationSet8 _DirectionalDash;
		public DirectionalAnimationSet8 DirectionalDash => _DirectionalDash;

		[Header("Strafe Animations Walking")]
		[SerializeField]
		private DirectionalAnimationSet8 _DirectionalStrafeWalk;
		public DirectionalAnimationSet8 DirectionalStrafeWalk => _DirectionalStrafeWalk;

		[Header("Strafe Animations Jogging")]
		[SerializeField]
		private DirectionalAnimationSet8 _DirectionalStrafeJog;
		public DirectionalAnimationSet8 DirectionalStrafeJog => _DirectionalStrafeJog;

		[Header("Combat Animation Set")]
		[SerializeField]
		private AttackTransition _QuickAttack;
		public AttackTransition QuickAttack => _QuickAttack;

	}
}