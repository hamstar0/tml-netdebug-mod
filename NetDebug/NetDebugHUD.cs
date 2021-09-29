using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using HUDElementsLib;


namespace NetDebug {
	partial class NetDebugHUD : HUDElement {
		public static Color MaxFadeColor { get; } = new Color( 112, 112, 112 );



		////////////////

		private UIList NpcsList;
		private UIList ItemsList;
		private UITextPanel<string> ClearOldButton;

		private IDictionary<int, UISyncDataElement> NpcListElements = new Dictionary<int, UISyncDataElement>();
		private IDictionary<int, UISyncDataElement> ItemListElements = new Dictionary<int, UISyncDataElement>();



		////////////////

		public NetDebugHUD() : base( "Net Debug", new Vector2( 64f, 96f ), new Vector2( 800f, 480f ) ) {
			var container = new UIPanel();
			container.Width.Set( 0f, 1f );
			container.Height.Set( 0f, 1f );
			container.BackgroundColor = Color.White * 0.05f;
			container.BorderColor *= 0.2f;
			this.Append( container );

			//

			this.ClearOldButton = new UITextPanel<string>( "Clear Old Desyncs", 1f );
			this.ClearOldButton.BackgroundColor = Color.White * 0.2f;
			this.ClearOldButton.BorderColor *= 0.2f;
			this.ClearOldButton.Left.Set( -192f, 0.5f );
			this.ClearOldButton.Top.Set( -12f, 0f );
			this.ClearOldButton.SetPadding( 4f );
			this.ClearOldButton.OnClick += (_, __) => {
				this.ClearOldButton.BackgroundColor = Color.White * 0.4f;
				NetDebugMod.Instance.ClearOldDesyncs();
			};
			this.ClearOldButton.OnMouseOver += (_, __) => {
				this.ClearOldButton.BackgroundColor = Color.White * 0.3f;
			};
			this.ClearOldButton.OnMouseOut += (_, __) => {
				this.ClearOldButton.BackgroundColor = Color.White * 0.2f;
			};
			container.Append( this.ClearOldButton );

			//

			var npcListlabel = new UIText( "Desynced NPCs" );
			npcListlabel.Top.Set( -8f, 0f );
			container.Append( npcListlabel );

			var itemListlabel = new UIText( "Desynced Items" );
			itemListlabel.Top.Set( -8f, 0f );
			itemListlabel.Left.Set( 0f, 0.5f );
			container.Append( itemListlabel );

			//
			
			this.NpcsList = this.InitializeList( container, true );
			this.ItemsList = this.InitializeList( container, false );
		}

		private UIList InitializeList( UIPanel container, bool isLeft ) {
			var listContainer = new UIPanel();
			listContainer.BackgroundColor = Color.White * 0.05f;
			listContainer.BorderColor *= 0.2f;
			listContainer.Width.Set( 0f, 0.5f );
			listContainer.Height.Set( 0f, 1f );
			listContainer.Top.Set( 8f, 0f );
			listContainer.Left.Set( 0f, isLeft ? 0f : 0.5f );
			listContainer.SetPadding( 0f );
			listContainer.MarginTop = 0f;
			listContainer.MarginBottom = 0f;
			listContainer.MarginLeft = 0f;
			listContainer.MarginRight = 0f;
			container.Append( listContainer );

			var list = new UIList();
			list.Width.Set( -16f, 1f );
			list.Height.Set( 0f, 1f );
			listContainer.Append( list );

			var scrollbar = new UIScrollbar();
			scrollbar.Left.Set( -20f, 1f );
			scrollbar.Top.Set( 8f, 0f );
			scrollbar.Height.Set( -16f, 1f );
			scrollbar.SetView( 100f, 1000f );
			scrollbar.HAlign = 0f;
			listContainer.Append( scrollbar );

			list.SetScrollbar( scrollbar );

			return list;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			this.UpdateClearButtonInteractions();

			base.Update( gameTime );
		}
	}
}