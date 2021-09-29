using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HUDElementsLib;


namespace NetDebug {
	partial class NetDebugHUD : HUDElement {
		public static Color MaxFadeColor { get; } = new Color( 64, 64, 64 );



		////////////////

		public void ApplyNpcChangesToList( IDictionary<int, (int type, int fadeDuration)> changes ) {
			foreach( KeyValuePair<int, (int type, int fade)> kv in changes.ToArray() ) {
				int npcWho = kv.Key;
				int npcType = kv.Value.type;

				if( !this.NpcListElements.ContainsKey(npcWho) ) {
					this.AddNpcListEntry( npcWho, npcType );
				} else {
					this.UpdateNpcListEntry( changes, npcWho );
				}
			}
		}

		public void ApplyItemChangesToList( IDictionary<int, (int type, int fadeDuration)> changes ) {
			foreach( KeyValuePair<int, (int type, int fade)> kv in changes.ToArray() ) {
				int itemWho = kv.Key;
				int itemType = kv.Value.type;

				if( !this.ItemListElements.ContainsKey(itemWho) ) {
					this.AddItemListEntry( itemWho, itemType );
				} else {
					this.UpdateItemListEntry( changes, itemWho );
				}
			}
		}


		////////////////

		private void AddNpcListEntry( int npcWho, int npcNetID ) {
			NPC npc = Main.npc[npcWho];

			string npcName = npcNetID != 0
				? NPCID.Search.GetName( npcNetID )
				: "-";

			string currType = npc?.active == true
				? ""+npc.netID
				: "-";
			string info = npc?.active == true
				? npc.FullName
				: "-";
			string data = npcWho+": Is "+currType+" ("+info+"), expecting "+npcNetID+" ("+npcName+")";

			//

			var elem = new UISyncDataElement( npcWho, data );

			this.NpcsList.Add( elem );

			//

			this.NpcListElements[ npcWho ] = elem;
		}
		
		private void AddItemListEntry( int itemWho, int itemType ) {
			Item item = Main.item[itemWho];

			string itemName = itemType != 0
				? ItemID.Search.GetName( itemType )
				: "-";

			string currType = item?.active == true
				? ""+item.type
				: "-";
			string info = item?.active == true
				? item.Name
				: "-";
			string data = itemWho+": Is "+currType+" ("+info+"), expecting "+itemType+" ("+itemName+")";

			//

			var elem = new UISyncDataElement( itemWho, data );

			this.ItemsList.Add( elem );

			//

			this.ItemListElements[ itemWho ] = elem;
		}

		////////////////

		public bool RemoveNpcEntry( int npcWho ) {
			if( !this.NpcListElements.ContainsKey(npcWho) ) {
				return false;
			}

			this.NpcsList.Remove( this.NpcListElements[npcWho] );
			this.NpcListElements.Remove( npcWho );

			return true;
		}

		public bool RemoveItemEntry( int itemWho ) {
			if( !this.ItemListElements.ContainsKey(itemWho) ) {
				return false;
			}

			this.ItemsList.Remove( this.ItemListElements[itemWho] );
			this.ItemListElements.Remove( itemWho );

			return true;
		}


		////////////////

		private void UpdateNpcListEntry( IDictionary<int, (int type, int fadeDuration)> changes, int npcWho ) {
			float fade = (float)changes[npcWho].fadeDuration / NetDebugMod.MaxFadeDuration;
			if( fade > 1f ) { fade = 1f; }

			this.NpcListElements[ npcWho ].TextColor = Color.Lerp( Color.White, NetDebugHUD.MaxFadeColor, fade );
		}

		private void UpdateItemListEntry( IDictionary<int, (int type, int fadeDuration)> changes, int itemWho ) {
			float fade = (float)changes[itemWho].fadeDuration / NetDebugMod.MaxFadeDuration;
			if( fade > 1f ) { fade = 1f; }

			this.ItemListElements[ itemWho ].TextColor = Color.Lerp( Color.White, NetDebugHUD.MaxFadeColor, fade );
		}
	}
}