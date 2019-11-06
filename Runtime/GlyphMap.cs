using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Valax321.ControllerPrompts
{
    [CreateAssetMenu(menuName = "Controllers/Controller Glyph Map")]
    public class GlyphMap : ScriptableObject, IGlyphProvider
    {
        [System.Serializable]
        public struct Entry
        {
            public int id;
            public Sprite asset;
        }
        
        [SerializeField] private Sprite m_defaultGlyph;
        [SerializeField] private Entry[] m_glyphs;

        public Sprite GetSprite(int id)
        {
            var spr = m_defaultGlyph;

            foreach (var g in m_glyphs)
            {
                if (g.id != id) 
                    continue;
                spr = g.asset;
                break;
            }

            return spr;
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            return m_glyphs.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
