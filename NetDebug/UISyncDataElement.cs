using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;


namespace NetDebug {
	class UISyncDataElement : UITextPanel<string> {
		public static UISyncDataElement CreateForNpc( int npcWho, int npcNetID ) {
			NPC npc = Main.npc[npcWho];

			string currType = npc?.active == true
				? "" + npc.netID
				: "-";
			string currName = npc?.active == true
				? npc.FullName
				: "-";
			string npcName = npcNetID != 0
				? NPCID.Search.GetName( npcNetID )
				: "-";

			string data = npcWho + ": Is " + currType + " (" + currName + ")"
							+", expecting " + npcNetID + " (" + npcName + ")";

			return new UISyncDataElement( npcWho, data );
		}

		public static UISyncDataElement CreateForItem( int itemWho, int itemType ) {
			Item item = Main.item[itemWho];

			string currType = item?.active == true
				? "" + item.type
				: "-";
			string currName = item?.active == true
				? item.Name
				: "-";
			string itemName = itemType != 0
				? ItemID.Search.GetName( itemType )
				: "-";

			string data = itemWho + ": Is " + currType + " (" + currName + ")"
							+", expecting " + itemType + " (" + itemName + ")";

			return new UISyncDataElement( itemWho, data );
		}



		////////////////

		public int NpcWho;



		////////////////
		
		private UISyncDataElement( int npcWho, string text ) : base( text, 0.75f, false ) {
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