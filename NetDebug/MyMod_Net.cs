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
					: (short)0;

				packet.Write( data );
			}

			//

			len = Main.item.Length - 1;
			for( int i = 0; i < len; i++ ) {
				Item item = Main.item[i];
				short data = item?.active == true
					? (short)item.type
					: (short)0;

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
				int otherNetID = (int)reader.ReadInt16();
				bool otherActive = otherNetID != 0;

				NPC currNpc = Main.npc[i];
				bool currActive = currNpc?.active == true;

				if( currActive ) {
					if( currNpc?.netID != otherNetID ) {
						npcChanges[i] = otherNetID;
					}
				} else if( otherActive ) {
					npcChanges[i] = otherNetID;
				}
			}

			//

			len = Main.item.Length - 1;
			for( int i=0; i<len; i++ ) {
				int otherType = (int)reader.ReadInt16();
				bool otherActive = otherType != 0;

				Item currItem = Main.item[i];
				bool currActive = currItem?.active == true;

				if( currActive ) {
					if( currItem?.type != otherType ) {
						itemChanges[i] = otherType;
					}
				} else if( otherActive ) {
					itemChanges[i] = otherType;
				}
			}

			this.ApplySync( npcChanges, itemChanges );
		}
	}
}