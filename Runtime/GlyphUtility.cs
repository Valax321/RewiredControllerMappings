using System;
using System.IO;
using System.Linq;
using Rewired;
using UnityEngine;

namespace Valax321.ControllerPrompts
{
    public static class GlyphUtility
    {
        private const string k_glyphPath = "ControllerGlyphs";
        private const string k_glyphAliasPath = "ControllerAliases";

        private static string k_keyboardPath => Path.Combine(k_glyphPath, "Keyboard");
        private static string k_mousePath => Path.Combine(k_glyphPath, "Mouse");
        
        public static IGlyphProvider GetKeyboardGlyphMap()
        {
            return Resources.Load<GlyphMap>(k_keyboardPath);
        }

        public static IGlyphProvider GetMouseGlyphMap()
        {
            return Resources.Load<GlyphMap>(k_mousePath);
        }
        
        public static IGlyphProvider GetGlyphsForControllerGUID(Guid controllerID)
        {
            Debug.Log($"Loading glyph map for {controllerID}");
            return Resources.Load<GlyphMap>(Path.Combine(k_glyphPath, controllerID.ToString()));
        }

        public static IGlyphProvider GetGlyphsByAlias(string aliasName)
        {
            Debug.Log($"Loading glyph map for alias {aliasName}");
            return Resources.Load<GlyphMapAlias>(Path.Combine(k_glyphAliasPath, aliasName));
        }

        public static int GetGlyphIDForAction(Player player, int actionID)
        {
            var controller = player.controllers.GetLastActiveController();
            return GetGlyphIDForAction(player, actionID, controller);
        }

        public static int GetGlyphIDForAction(Player player, int actionID, ControllerType specificType)
        {
            var controller = player.controllers.GetController(specificType, 0 /* TODO: what */);
            return GetGlyphIDForAction(player, actionID, controller);
        }

        private static int GetGlyphIDForAction(Player player, int actionID, Controller controller)
        {
            int glyphID = 0;
            var action = player.controllers.maps.GetFirstElementMapWithAction(controller.type, actionID, true);
            
            switch (controller.type)
            {
                case ControllerType.Keyboard:
                    glyphID = (int)action.keyboardKeyCode;
                    break;
                case ControllerType.Mouse:
                    glyphID = controller.GetElementById(action.elementIdentifierId).id;
                    break;
                default:
                    var template = controller.Templates.First();
                    glyphID = template.GetElement(action.elementIndex).id;
                    break;
            }

            return glyphID;
        }

        public static ControllerType GetLastControllerType(Player player)
        {
            return player.controllers.GetLastActiveController().type;
        }
    }
}