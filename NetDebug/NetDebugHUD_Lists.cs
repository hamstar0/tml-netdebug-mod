using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using HUDElementsLib;


namespace NetDebug {
	partial class NetDebugHUD : HUDElement {
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
		
		private void AddNpcListEntry( int npcWho, int npcType ) {
			NPC npc = Main.npc[npcWho];

			int currType = npc?.active == true
				? npc.netID
				: -1;
			string info = npc?.active == true
				? npc.FullName
				: "-";
			string data = npcWho+": Is "+currType+" ("+info+"), expecting "+npcType;

			//

			var elem = new SyncDataElement( npcWho, data );

			this.NpcsList.Add( elem );

			//

			this.NpcListElements[ npcWho ] = elem;
		}
		
		private void AddItemListEntry( int itemWho, int itemType ) {
			Item item = Main.item[itemWho];

			int currType = item?.active == true
				? item.type
				: -1;
			string info = item?.active == true
				? item.Name
				: "-";
			string data = itemWho+": Is "+currType+" ("+info+"), expecting "+itemType;

			//

			var elem = new SyncDataElement( itemWho, data );

			this.ItemsList.Add( elem );

			//

			this.ItemListElements[ itemWho ] = elem;
		}
		

		////////////////

		private void UpdateNpcListEntry( IDictionary<int, (int type, int fadeDuration)> changes, int npcWho ) {
			float fade = (float)changes[npcWho].fadeDuration / NetDebugMod.MaxFadeDuration;
			if( fade > 1f ) { fade = 1f; }

			this.NpcListElements[ npcWho ].TextColor = Color.Lerp( Color.White, Color.Transparent, fade );
		}

		private void UpdateItemListEntry( IDictionary<int, (int type, int fadeDuration)> changes, int itemWho ) {
			float fade = (float)changes[itemWho].fadeDuration / NetDebugMod.MaxFadeDuration;
			if( fade > 1f ) { fade = 1f; }

			this.ItemListElements[ itemWho ].TextColor = Color.Lerp( Color.White, Color.Transparent, fade );
		}
	}
}