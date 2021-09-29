using System;
using Terraria;
using Terraria.UI;
using HUDElementsLib;


namespace NetDebug {
	partial class NetDebugHUD : HUDElement {
		private bool IsClearButtonHover = false;
		private bool IsClearButtonClick = false;



		////////////////

		private void UpdateClearButtonInteractions() {	// <- This exists because ui interactivity unimplemented in HUD Elements Lib
			CalculatedStyle dim = this.ClearOldButton.GetOuterDimensions();

			if( !Main.mouseLeft ) {
				if( this.IsClearButtonClick ) {
					this.IsClearButtonClick = false;

					this.ClearOldButton.MouseOut( null );
				}
			}

			//if( this.IsMouseHovering ) {
			if( dim.ToRectangle().Contains(Main.MouseScreen.ToPoint()) ) {
				Main.LocalPlayer.mouseInterface = true;

				if( !this.IsClearButtonHover ) {
					this.IsClearButtonHover = true;

					this.ClearOldButton.MouseOver( null );
				}

				if( Main.mouseLeft ) {
					if( !this.IsClearButtonClick ) {
						this.IsClearButtonClick = true;

						this.ClearOldButton.Click( null );
					}
				}
			} else {
				if( this.IsClearButtonHover ) {
					this.IsClearButtonHover = false;

					this.ClearOldButton.MouseOut( null );
				}
			}
		}
	}
}