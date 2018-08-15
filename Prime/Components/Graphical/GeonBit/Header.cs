using System;
using Microsoft.Xna.Framework;
using Prime.UI;
using GeonBit.UI.Entities;

namespace Prime.UI
{
    public class Header : UIEntity
    {
        private GeonBit.UI.Entities.Header header;

        public string Text
        {
            get
            {
                return header.Text;
            }
            set
            {
                header.Text = value;
            }
        }

        public Header(string text, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
        {
            this.header = new GeonBit.UI.Entities.Header(text, (Anchor) a, offset);

            this.Entity = header;
        }
    }
}
