using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valax321.ControllerPrompts
{
    [CreateAssetMenu(menuName = "Controllers/Controller Glyph Map Alias")]
    public class GlyphMapAlias : ScriptableObject, IGlyphProvider
    {
        [SerializeField] private GlyphMap m_original;

        public bool hasAsset => m_original;
        
        public Sprite GetSprite(int glyphID)
        {
            if (m_original)
            {
                return m_original.GetSprite(glyphID);
            }
            
            return null;
        }

        public IEnumerator<GlyphMap.Entry> GetEnumerator()
        {
            return m_original ? m_original.GetEnumerator() : null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}