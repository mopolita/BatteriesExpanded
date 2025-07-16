using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Batteries
{
	[StaticConstructorOnStartup]
	public class Building_CustomBattery : Building_Battery
	{
		private int ticksToExplode;

		public ModExtension_BarSize Props => def.GetModExtension<ModExtension_BarSize>();

		private static readonly Vector2 BarSize = new Vector2(1.3f, 0.4f);

		private static readonly Material BatteryBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.9f, 0.85f, 0.2f));

		private static readonly Material BatteryBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.3f, 0.3f, 0.3f));

		public override void DrawAt(Vector3 drawLoc, bool flip = false)
		{
			base.DrawAt(drawLoc, flip);
			CompPowerBattery comp = GetComp<CompPowerBattery>();
			GenDraw.FillableBarRequest r = new GenDraw.FillableBarRequest
			{
				center = drawLoc + Vector3.up * 0.1f,
				size = (Props != null ? new Vector2(Props.sizeX, Props.sizeY) : BarSize),
				fillPercent = comp.StoredEnergy / comp.Props.storedEnergyMax,
				filledMat = BatteryBarFilledMat,
				unfilledMat = BatteryBarUnfilledMat,
				margin = 0.15f
			};
			Rot4 rotation = Rotation;
			rotation.Rotate(RotationDirection.Clockwise);
			r.rotation = rotation;
			GenDraw.DrawFillableBar(r);
			if (ticksToExplode > 0 && Spawned)
			{
				Map.overlayDrawer.DrawOverlay(this, OverlayTypes.BurningWick);
			}
		}
	}
}
