using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valax321.ControllerPrompts
{
    public interface IGlyphProvider : IEnumerable<GlyphMap.Entry>
    {
        Sprite GetSprite(int glyphID);
    }
}