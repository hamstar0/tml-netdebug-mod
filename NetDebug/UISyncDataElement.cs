using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace NetDebug {
	class UISyncDataElement : UITextPanel<string> {
		public int NpcWho;



		////////////////

		public UISyncDataElement( int npcWho, string text ) : base( text, 0.75f, false ) {
			this.SetPadding( 0f );
			this.BackgroundColor = Color.White * 0.05f;
			this.BorderColor *= 0.2f;

			this.NpcWho = npcWho;
		}


		////////////////

		public override int CompareTo( object obj ) {
			if( obj is UISyncDataElement ) {
				int otherWho = ( (UISyncDataElement)obj ).NpcWho;
				return this.NpcWho > otherWho
					? 1
					: this.NpcWho < otherWho
					? -1
					: 0;
			}

			return base.CompareTo( obj );
		}
	}
}