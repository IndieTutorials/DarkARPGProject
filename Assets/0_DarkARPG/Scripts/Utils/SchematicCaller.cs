using GamingIsLove.Makinom;
using GamingIsLove.Makinom.Schematics;
using GamingIsLove.ORKFramework;
using UnityEngine;

namespace RustedGames
{
    public sealed class SchematicCaller : MonoBehaviour
    {
        [SerializeField]
        private KeyCode _KeyCode;

        [SerializeField]
        private MakinomSchematicAsset _SchematicAsset;

        private void Update()
        {
            if (Input.GetKeyDown(_KeyCode))
            {
                Schematic.Play(_SchematicAsset, this, null, ORK.Game.ActiveGroup.Leader, null);
            }
        }
    }
}
