using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace NetDebug {
	public partial class NetDebugMod : Mod {
		public const float MaxFadeDuration = 30f;



		////////////////

		private void UpdateRecentNpcChanges() {
			foreach( KeyValuePair<int, (int type, int fade)> kv in this.RecentNpcChanges.ToArray() ) {
				int npcWho = kv.Key;
				int fade = kv.Value.fade + 1;

				if( fade >= NetDebugMod.MaxFadeDuration ) {
					this.RecentNpcChanges.Remove( npcWho );
				} else {
					this.RecentNpcChanges[npcWho] = (kv.Value.type, fade);
				}
			}
		}
	}
}