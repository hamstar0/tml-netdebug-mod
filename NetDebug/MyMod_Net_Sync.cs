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

		////

		private void ApplyNpcSync( IDictionary<int, int> changes ) {
			foreach( KeyValuePair<int, int> kv in changes ) {
				int chgWho = kv.Key;
				int chgNetID = kv.Value;

				this.RecentNpcChanges[chgWho] = (chgNetID, 0);

				this.HUD.AddNpcListEntry( chgWho, chgNetID );
			}
		}

		private void ApplyItemSync( IDictionary<int, int> changes ) {
			foreach( KeyValuePair<int, int> kv in changes ) {
				int chgWho = kv.Key;
				int chgType = kv.Value;

				this.RecentItemChanges[chgWho] = (chgType, 0);

				this.HUD.AddItemListEntry( chgWho, chgType );
			}
		}
	}
}