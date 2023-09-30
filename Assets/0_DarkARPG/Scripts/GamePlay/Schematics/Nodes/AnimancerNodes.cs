using UnityEngine;
using System.Collections.Generic;
using GamingIsLove.Makinom;
using GamingIsLove.Makinom.Schematics;
using GamingIsLove.Makinom.Schematics.Nodes;
using RustedGames;

namespace GamingIsLove.ORKFramework.Schematics.Nodes
{

    [EditorHelp("TrySet State", "The state for the character to attempt to enter")]
    [NodeInfo("Animation/Animancer/Character","Animancer/Character")]
    public class TrySetStateNode : BaseSchematicNode
    {
        [EditorHelp("State Name", "The name of the state that will be attempted to enter.", "")]
        [EditorWidth(true)]
        [EditorReflectionField(EditorReflectionFieldType.Class, typeof(CharacterState))]
        public string stateName = "";

        [EditorSeparator]
        [EditorTitleLabel("Character Object")]
        public SchematicObjectSelection character = new SchematicObjectSelection();


        [EditorHelp("Time Between (s)", "The time in seconds between try setting the state to two objects.\n" +
        "Only used if greater than 0 and more than one object will be used.", "")]
        public FloatValue<SchematicObjectSelection> timeBetweenValue = new FloatValue<SchematicObjectSelection>();

        [EditorHelp("Wait Between", "Wait for all objects to try set the state before executing the next node.\n" +
         "If disabled, the next node will execute immediately while this node continues.", "")]
        public bool waitBetween = true;

        [EditorHelp("Debug Log", "Write debug logs to the console.", "")]
        public bool debugLog = false;

        [EditorHelp("Debug Type", "Select the debug type will be used:\n" +
                    "- Log: Printed to 'Debug.Log'.\n" +
                    "- Warning: Printed to 'Debug.LogWarning'.\n" +
                    "- Error: Printed to 'Debug.LogError'.", "")]
        [EditorCondition("debugLog", true)]
        public DebugType type = DebugType.Log;

        

        public TrySetStateNode()
        {
        }

        public override void Execute(Schematic schematic)
        {
            if (this.stateName != "")
            {
                List<GameObject> list = this.character.GetObjects(schematic);

                if (list.Count > 0)
                {
                    new Runtime(this, schematic, list).Continue();

                    if (!this.waitBetween)
                    {
                        schematic.NodeFinished(this.next);
                    }
                }
                else
                {
                    Maki.Pooling.GameObjectLists.Add(list);
                    schematic.NodeFinished(this.next);
                }
            }
            else
            {
                if (debugLog)
                {
                    RustedGames.Tools.CharacterStateHelper.Log(type, "TrySetStateNode: the stateName is empty.");
                    //Debug.LogWarning("TrySetStateNode: the stateName is empty.");
                }
                schematic.NodeFinished(this.next);
            }
        }

        /*
		============================================================================
		Node name functions
		============================================================================
		*/
        public override string GetNodeDetails()
        {
            return this.character.ToString() + ": " + this.stateName;
        }

        public override Color EditorColor
        {
            get { return Maki.EditorSettings.gameObjectNodeColor; }
        }

        /*
		============================================================================
		Runtime class
		============================================================================
		*/
        public class Runtime : SchematicRuntimeNode<TrySetStateNode>
        {
            private List<GameObject> list;
            private int index = 0;
            private float timeBetween = 0;

            public Runtime(TrySetStateNode settings, Schematic schematic, List<GameObject> list) : base(settings, schematic)
            {
                this.list = list;
                this.timeBetween = this.settings.timeBetweenValue.GetValue(schematic);                
            }

            public override void Continue()
            {
                if (this.list[this.index] != null)
                {
                    Character character = this.list[index].GetComponent<Character>();
                    if (character == null)
                    {
                        if (settings.debugLog)
                        {
                            RustedGames.Tools.CharacterStateHelper.Log(settings.type, "TrySetStateNode: searching for Character Component in Parent.");
                           // Debug.LogWarning("TrySetStateNode: searching for Character Component in Parent.");
                        }
                        character = this.list[index].GetComponentInParent<Character>();
                    }

                    if(character != null)
                    {
                        if (settings.debugLog)
                        {
                            RustedGames.Tools.CharacterStateHelper.Log(settings.type, "TrySetStateNode: Character Component Found!.");
                            //Debug.LogWarning("TrySetStateNode: Character Component Found!.");
                        }

                        if (character.StateMachine.TrySetState(
                            RustedGames.Tools.CharacterStateHelper.Get(
                                this.list[index], this.settings.stateName)))
                        {
                            if (settings.debugLog)
                            {
                                RustedGames.Tools.CharacterStateHelper.Log(settings.type, "TrySetStateNode: Success!.");
                               // Debug.LogWarning("TrySetStateNode: Success!.");
                            }
                        }
                        else
                        {
                            if (settings.debugLog)
                            {
                                RustedGames.Tools.CharacterStateHelper.Log(settings.type, "TrySetStateNode: Failed!.");
                                //Debug.LogWarning("TrySetStateNode: Failed!.");
                            }
                        }
                    }
                }

                this.index++;
                // execute next
                if (this.index < this.list.Count)
                {
                    if (this.timeBetween > 0)
                    {
                        this.schematic.StartContinue(this.timeBetween, this);
                    }
                    else
                    {
                        this.Continue();
                    }
                }
                // finish
                else                
                {
                    Maki.Pooling.GameObjectLists.Add(this.list);
                    this.list = null;
                    if (this.settings.waitBetween)
                    {
                        this.schematic.NodeFinished(this.settings.next);
                    }
                }
            }
        }

    }
}