using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace NetDebug {
	public partial class NetDebugMod : Mod {
		private void ApplySync( IDictionary<int, int> npcChanges, IDictionary<int, int> itemChanges ) {
			if( npcChanges.Count != 0 ) {
				this.ApplyNpcSync( npcChanges );
			}
			if( itemChanges.Count != 0 ) {
				this.ApplyItemSync( itemChanges );
			}
		}

		private void ApplyNpcSync( IDictionary<int, int> changes ) {
			bool hasChanged = true;

			int len = Main.npc.Length - 1;
			for( int i=0; i<len; i++ ) {
				if( !changes.ContainsKey(i) ) {
					continue;
				}

				NPC npc = Main.npc[i];

				if( changes[i] == -1 ) {
					if( npc?.active == true ) {
						this.RecentNpcChanges[i] = (changes[i], 0);
						hasChanged = true;
					}
				} else {
					if( npc?.active != true || npc.type != changes[i] ) {
						this.RecentNpcChanges[i] = (changes[i], 0);
						hasChanged = true;
					}
				}
			}

			if( hasChanged ) {
				this.HUD.ApplyNpcChangesToList( this.RecentNpcChanges );
			}
		}


		private void ApplyItemSync( IDictionary<int, int> changes ) {
			if( changes.Count == 0 ) {
				return;
			}

			bool hasChanged = true;

			int len = Main.item.Length - 1;
			for( int i=0; i<len; i++ ) {
				if( !changes.ContainsKey(i) ) {
					continue;
				}

				Item item = Main.item[i];

				if( changes[i] == -1 ) {
					if( item?.active == true ) {
						this.RecentItemChanges[i] = (changes[i], 0);
						hasChanged = true;
					}
				} else {
					if( item?.active != true || item.type != changes[i] ) {
						this.RecentItemChanges[i] = (changes[i], 0);
						hasChanged = true;
					}
				}
			}

			if( hasChanged ) {
				this.HUD.ApplyItemChangesToList( this.RecentItemChanges );
			}
		}
	}
}