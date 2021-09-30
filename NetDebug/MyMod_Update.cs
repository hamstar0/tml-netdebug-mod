using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NetDebug {
	public partial class NetDebugMod : Mod {
		public const float MaxFadeDuration = 60f;



		////////////////

		private void UpdateRecentChanges() {
			this.UpdateRecentChangesTimers();

			if( Main.netMode != NetmodeID.Server ) {
				this.HUD.UpdateNpcListEntries( this.RecentNpcChanges );
				this.HUD.UpdateItemListEntries( this.RecentItemChanges );
			}
		}

		////

		private void UpdateRecentChangesTimers() {
			foreach( KeyValuePair<int, (int type, int fade)> kv in this.RecentNpcChanges.ToArray() ) {
				int who = kv.Key;
				int fade = kv.Value.fade + 1;

				//if( fade >= NetDebugMod.MaxFadeDuration ) {
					//this.RecentNpcChanges.Remove( who );
					//
					//this.HUD.RemoveNpcEntry( who );
				//} else {
				//}

				this.RecentNpcChanges[who] = (kv.Value.type, fade);
			}

			foreach( KeyValuePair<int, (int type, int fade)> kv in this.RecentItemChanges.ToArray() ) {
				int who = kv.Key;
				int fade = kv.Value.fade + 1;

				//if( fade >= NetDebugMod.MaxFadeDuration ) {
					//this.RecentItemChanges.Remove( who );
					//
					//this.HUD.RemoveItemEntry( who );
				//} else {
				//}

				this.RecentItemChanges[who] = (kv.Value.type, fade);
			}
		}
	}
}