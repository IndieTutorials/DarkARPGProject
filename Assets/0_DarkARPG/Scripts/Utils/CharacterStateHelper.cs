using GamingIsLove.Makinom;
using UnityEngine;

namespace RustedGames.Tools
{
	public static class CharacterStateHelper
	{


		public static CharacterState Get(GameObject gameObject, string stateName)
        {
			if (gameObject != null)
            {
				System.Type type = GamingIsLove.Makinom.Maki.ReflectionHandler.GetTypeOrInterface(stateName, typeof(CharacterState));
				if (type!= null)
                {
					//Debug.LogWarning("CharacterStateHelper: Found type "+ type.Name);
					return gameObject.GetComponent(type) as CharacterState;
                }
            }
			return null;
        }

		public static void Log(DebugType type, string content)
		{
			if (DebugType.Log == type)
			{
				Debug.Log(content);
			}
			else if (DebugType.Warning == type)
			{
				Debug.LogWarning(content);
			}
			else if (DebugType.Error == type)
			{
				Debug.LogError(content);
			}
		}


	}
}