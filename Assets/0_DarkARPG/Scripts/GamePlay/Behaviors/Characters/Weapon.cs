using UnityEngine;
using RustedGames.Data;

namespace RustedGames
{
	public sealed class Weapon : MonoBehaviour
	{
		[SerializeField]
		private Character _Character;
		public Character Character => _Character;

		[SerializeField]
		private AnimationSet _WeaponAnimationSet;
		public AnimationSet WeaponAnimationSet => _WeaponAnimationSet;

		public static event System.Action OnWeaponChanged;

        private void Start()
        {
            if (_Character == null)
            {
				_Character = transform.root.gameObject.GetComponent<Character>();
            }

			if (_Character == null)
            {
				Debug.LogError("Character not found in parent gameobject!");
				return;
            }

			_Character.SetCurrentAnimationSet(_WeaponAnimationSet);
			OnWeaponChanged?.Invoke();
		}

        private void OnDestroy()
        {
			_Character.SetDefaultAnimationSet();
			OnWeaponChanged?.Invoke();
		}
    }
}