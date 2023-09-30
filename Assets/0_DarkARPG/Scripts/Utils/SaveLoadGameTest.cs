using UnityEngine;

namespace RustedGames
{
	public sealed class SaveLoadGameTest : MonoBehaviour
	{
		[SerializeField]
		private KeyCode _SaveKeyCode;

		[SerializeField]
		private KeyCode _LoadKeyCode;

        private void Update()
        {
            if (Input.GetKeyDown(_SaveKeyCode))
            {
                GamingIsLove.Makinom.Maki.SaveGame.Save(0);
            }

            if (Input.GetKeyDown(_LoadKeyCode))
            {
                GamingIsLove.Makinom.Maki.SaveGame.Load(0);
            }

        }



    }
}