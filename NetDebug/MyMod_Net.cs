using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace NetDebug {
	public partial class NetDebugMod : Mod {
		private void SendDataSync() {
			ModPacket packet = this.GetPacket();

			//

			int len = Main.npc.Length - 1;
			for( int i = 0; i < len; i++ ) {
				NPC npc = Main.npc[i];
				short data = npc?.active == true
					? (short)npc.netID
					: (short)-1;

				packet.Write( data );
			}

			//

			len = Main.item.Length - 1;
			for( int i = 0; i < len; i++ ) {
				Item item = Main.item[i];
				short data = item?.active == true
					? (short)item.type
					: (short)-1;

				packet.Write( data );
			}

			//

			packet.Send( -1, -1 );
		}


		////////////////

		public override void HandlePacket( BinaryReader reader, int whoAmI ) {
			var npcChanges = new Dictionary<int, int>();
			var itemChanges = new Dictionary<int, int>();
			int len;

			//

			len = Main.npc.Length - 1;
			for( int i=0; i<len; i++ ) {
				int incNetID = (int)reader.ReadInt16();
				bool incActive = incNetID != -1;

				if( Main.npc[i].active == incActive ) {
					continue;
				}
				if( Main.npc[i].netID == incNetID ) {
					continue;
				}

				npcChanges[i] = incNetID;
			}

			//

			len = Main.item.Length - 1;
			for( int i=0; i<len; i++ ) {
				int incType = (int)reader.ReadInt16();
				bool incActive = incType != -1;

				if( Main.item[i].active == incActive ) {
					continue;
				}
				if( Main.item[i].type == incType ) {
					continue;
				}

				itemChanges[i] = incType;
			}

			this.ApplySync( npcChanges, itemChanges );
		}
	}
}