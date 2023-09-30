using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using GamingIsLove.Makinom;
using GamingIsLove.ORKFramework;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Thanks to Keldryn
// http://forum.orkframework.com/discussion/3336/resource-generating-ork-constants-for-scripting#latest

namespace RustedGames
{
#if UNITY_EDITOR
    public static class MakinORKConstantsGenerator
    {
        [MenuItem("Edit/Generate MakinORKConstants.cs")]
        public static void Generate()
        {
            // Try to find an existing file in the project called "ORKConstants.cs"
            string filePath = string.Empty;
            foreach (var file in Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories))
            {
                if (Path.GetFileNameWithoutExtension(file) == "MakinORKConstants")
                {
                    filePath = file;
                    break;
                }
            }

            // If no such file exists already, use the save panel to get a folder in which the file will be placed.
            if (string.IsNullOrEmpty(filePath))
            {
                string directory = EditorUtility.OpenFolderPanel("Choose location for MakinORKConstants.cs", Application.dataPath, "");

                // Canceled choose? Do nothing.
                if (string.IsNullOrEmpty(directory))
                {
                    return;
                }

                filePath = Path.Combine(directory, "MakinORKConstants.cs");
            }


            // Ensure that ORK is instantiated
            if (!Maki.Initialized)
            {
                Maki.Initialize(GamingIsLove.Makinom.Editor.MakinomAssetHelper.LoadProjectAsset());
            }

            Debug.Log("Generating MakinORKConstants.cs...");
            // Write out our file
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("// This file is auto-generated. Modifications are not saved.");
                writer.WriteLine();
                writer.WriteLine("namespace RustedGames");
                writer.WriteLine("{");

                // Write out the tags

                writer.WriteLine(BuildClass("ORKStatusValue", ORK.StatusValues.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKStatusEffect", ORK.StatusEffects.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKAbility", ORK.Abilities.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKEquipmentSlots", ORK.EquipmentSlots.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKEquipment", ORK.Equipment.GetNames().ToList()));

                // Custom
                writer.WriteLine(BuildClass("ORKCombatants", ORK.Combatants.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKItemTypes", ORK.ItemTypes.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKItems", ORK.Items.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKCurrencies", ORK.Currencies.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKInputKeys", Maki.InputKeys.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKAbilityTypes", ORK.AbilityTypes.GetNames().ToList()));

                writer.WriteLine(BuildClass("ORKAreaTypes", ORK.AreaTypes.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKAreas", ORK.Areas.GetNames().ToList()));
                writer.WriteLine(BuildClass("ORKDifficulties", ORK.Difficulties.GetNames().ToList()));


                // End of namespace RustedGames
                writer.WriteLine("}");
                writer.WriteLine();

            }

            Debug.Log("Completed");

            // Refresh
            AssetDatabase.Refresh();
        }
        private static string BuildClass(string className, List<string> constantNames)
        {
            StringBuilder sb = new StringBuilder("    public static class ");
            sb.AppendLine(className);
            sb.AppendLine("    {");

            try
            {
                for (int i = 0; i < constantNames.Count; i++)
                {
                    sb.AppendLine(string.Format("        public const int {0} = {1};", MakeSafeForCode(constantNames[i].ToUpper()),
                        i));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(string.Format("Could not generate class {0}: {1}", className, ex.Message));
            }
            finally
            {
                sb.AppendLine("    }");
                sb.AppendLine();
            }


            return sb.ToString();
        }

        private static string MakeSafeForCode(string str)
        {
            str = Regex.Replace(str, "[^a-zA-Z0-9_]", "_", RegexOptions.Compiled);
            if (char.IsDigit(str[0]))
            {
                str = "_" + str;
            }
            return str;
        }
    }
#endif //UNITY_EDITOR
}