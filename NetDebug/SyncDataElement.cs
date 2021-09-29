using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace NetDebug {
	class SyncDataElement : UITextPanel<string> {
		public int NpcWho;



		////////////////

		public SyncDataElement( int npcWho, string text ) : base( text, 0.75f, false ) {
			this.SetPadding( 0f );

			this.NpcWho = npcWho;
		}


		////////////////

		public override int CompareTo( object obj ) {
			if( obj is SyncDataElement ) {
				int otherWho = ( (SyncDataElement)obj ).NpcWho;
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