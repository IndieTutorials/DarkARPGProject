// Fill out your copyright notice in the 81-C# Script-NewBehaviourScript.cs.txt

using GamingIsLove.ORKFramework;
using UnityEngine;

namespace RustedGames
{
    public sealed class ORKHelpers : MonoBehaviour
    {
        private void Start()
        {
            Combatant combatant = GamingIsLove.ORKFramework.ORKComponentHelper.GetCombatant(gameObject);
            if (combatant != null && combatant.Abilities.HasAbilities)
            {
                CombatantSetting combatantSetting = combatant.Setting;
                LearnAbility[] abilitiesLearnt = combatantSetting.abilityDevelopment.atLevel;
                foreach(LearnAbility learnAbility in abilitiesLearnt)
                {
                    AbilityShortcut ability = learnAbility.ability.GetAbility(combatant);
                    Debug.Log($"Ability: {ability.GetName()} level {ability.GetLevel()}");
                }
            }
        }
    }
}
