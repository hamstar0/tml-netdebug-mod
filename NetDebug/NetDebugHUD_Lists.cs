using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HUDElementsLib;


namespace NetDebug {
	partial class NetDebugHUD : HUDElement {
		public bool AddNpcListEntry( int npcWho, int otherNpcNetID ) {
			if( this.NpcListElements.ContainsKey(npcWho) ) {
				return false;
			}

			UISyncDataElement elem = UISyncDataElement.CreateForNpc( npcWho, otherNpcNetID );

			this.NpcsList.Add( elem );

			//

			this.NpcListElements[ npcWho ] = elem;

			return true;
		}
		
		public bool AddItemListEntry( int itemWho, int otherItemType ) {
			if( this.NpcListElements.ContainsKey(itemWho) ) {
				return false;
			}

			UISyncDataElement elem = UISyncDataElement.CreateForItem( itemWho, otherItemType );

			this.ItemsList.Add( elem );

			//

			this.ItemListElements[ itemWho ] = elem;

			return true;
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

		public void UpdateNpcListEntries( IDictionary<int, (int type, int fadeDuration)> changes ) {
			foreach( int npcWho in changes.Keys ) {
				float fade = (float)changes[npcWho].fadeDuration / NetDebugMod.MaxFadeDuration;
				if( fade > 1f ) { fade = 1f; }

				this.NpcListElements[npcWho].TextColor = Color.Lerp( Color.White, NetDebugHUD.MaxFadeColor, fade );
			}
		}

		public void UpdateItemListEntries( IDictionary<int, (int type, int fadeDuration)> changes ) {
			foreach( int itemWho in changes.Keys ) {
				float fade = (float)changes[itemWho].fadeDuration / NetDebugMod.MaxFadeDuration;
				if( fade > 1f ) { fade = 1f; }

				this.ItemListElements[ itemWho ].TextColor = Color.Lerp( Color.White, NetDebugHUD.MaxFadeColor, fade );
			}
		}
	}
}