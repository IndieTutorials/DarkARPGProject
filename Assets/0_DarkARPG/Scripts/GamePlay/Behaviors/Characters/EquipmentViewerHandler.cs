using UnityEngine;

namespace RustedGames
{
	public sealed class EquipmentViewerHandler : MonoBehaviour
	{
		[SerializeField] private Character _Character;

        private void Start()
        {
            GamingIsLove.Makinom.Maki.SaveGame.GameLoaded += SaveGame_GameLoaded;
            CheckEquipmentVisibility();
        }

        private void SaveGame_GameLoaded()
        {
            Debug.Log("Game Loaded!");
        }

        private void OnDestroy()
        {
            GamingIsLove.Makinom.Maki.SaveGame.GameLoaded -= SaveGame_GameLoaded;
        }

        private void CheckEquipmentVisibility()
        {
            for(int i = 0; i < GamingIsLove.ORKFramework.ORK.EquipmentSlots.Count; i++)
            {
                if (_Character.Combatant.Equipment[i].Available)
                {
                    if (_Character.Combatant.Equipment[i].Equipped &&
                       _Character.Combatant.Equipment[i].Equipment.Setting.equipSchematics != null)
                    {
                        Debug.Log("Checking Equipment: " + _Character.Combatant.Equipment[i].Equipment.GetName());
                        if (_Character.Combatant.Equipment[i].Equipment.Setting.equipSchematics.equipSchematicAsset.HasAsset)
                        {
                            GamingIsLove.Makinom.Schematics.Schematic.Play(
                                _Character.Combatant.Equipment[i].Equipment.Setting.equipSchematics.equipSchematicAsset.StoredAsset,
                                this,
                                null,
                                _Character.Combatant,
                                null
                                );
                        }
                    }
                }
            }
        }
    }
}