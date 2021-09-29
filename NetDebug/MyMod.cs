using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HUDElementsLib;


namespace NetDebug {
	public partial class NetDebugMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-netdebug-mod";



		////////////////

		public static NetDebugMod Instance => ModContent.GetInstance<NetDebugMod>();



		////////////////

		private int ServerSyncTimer = 0;

		private IDictionary<int, (int type, int fadeDuration)> RecentNpcChanges = new Dictionary<int, (int, int)>();
		private IDictionary<int, (int type, int fadeDuration)> RecentItemChanges = new Dictionary<int, (int, int)>();


		////////////////

		internal NetDebugHUD HUD { get; private set; }



		////////////////

		public override void Load() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.HUD = new NetDebugHUD();

				HUDElementsLibAPI.AddWidget( this.HUD );
			}
		}


		////////////////

		public override void MidUpdateTimeWorld() {
			if( Main.netMode == NetmodeID.Server ) {
				if( this.ServerSyncTimer-- <= 0 ) {
					this.ServerSyncTimer = 15;

					this.SendDataSync();
				}
			}

			this.UpdateRecentNpcChanges();
		}
	}
}