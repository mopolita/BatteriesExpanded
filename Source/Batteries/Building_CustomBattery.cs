using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Batteries
{
    [StaticConstructorOnStartup]
    public class Building_CustomBattery : Building
    {
        private const float MinEnergyToExplode = 250f;
        private const float EnergyToLoseWhenExplode = 200f;
        private const float ExplodeChancePerDamage = 0.05f;
        
        public ModExtension_BarSize Props => def.GetModExtension<ModExtension_BarSize>();

        private static readonly Vector2 BarSize = new Vector2(1.3f, 0.4f);

        private static readonly Material BatteryBarFilledMat =
            SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.9f, 0.85f, 0.2f));

        private static readonly Material BatteryBarUnfilledMat =
            SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.3f, 0.3f, 0.3f));

        private int ticksToExplode;
        private Sustainer wickSustainer;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksToExplode, "ticksToExplode");
        }

        public override void Tick()
        {
            base.Tick();
            if (ticksToExplode <= 0)
            {
                return;
            }

            if (wickSustainer == null)
            {
                StartWickSustainer();
            }
            else
            {
                wickSustainer.Maintain();
            }

            ticksToExplode--;
            if (ticksToExplode != 0)
            {
                return;
            }

            var radius = Rand.Range(0.5f, 1f) * 1.5f;
            GenExplosion.DoExplosion(Rotation.FacingCell, Map, radius, DamageDefOf.Flame, null);
            GetComp<CompPowerBattery>().DrawPower(EnergyToLoseWhenExplode);
        }

        public override void PostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostApplyDamage(dinfo, totalDamageDealt);
            if (Destroyed || ticksToExplode != 0 || dinfo.Def != DamageDefOf.Flame ||
                !(Rand.Value < ExplodeChancePerDamage) ||
                !(GetComp<CompPowerBattery>().StoredEnergy > MinEnergyToExplode))
            {
                return;
            }

            ticksToExplode = Rand.Range(70, 150);
            StartWickSustainer();
        }

        private void StartWickSustainer()
        {
            SoundInfo info = SoundInfo.InMap(this, MaintenanceType.PerTick);
            wickSustainer = SoundDefOf.HissSmall.TrySpawnSustainer(info);
        }
       
        public override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            CompPowerBattery comp = GetComp<CompPowerBattery>();
            GenDraw.FillableBarRequest fillableBarRequest = default;
            
            if (Props != null)
                fillableBarRequest.size = new Vector2(Props.sizeX, Props.sizeY);
            else
                fillableBarRequest.size = BarSize;
            
            fillableBarRequest.center = drawLoc + Vector3.up * 0.1f;
            fillableBarRequest.fillPercent = comp.StoredEnergy / comp.Props.storedEnergyMax;
            fillableBarRequest.filledMat = BatteryBarFilledMat;
            fillableBarRequest.unfilledMat = BatteryBarUnfilledMat;
            fillableBarRequest.margin = 0.15f;
            GenDraw.FillableBarRequest r = fillableBarRequest;
            Rot4 rotation = base.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
            if (ticksToExplode > 0 && base.Spawned)
            {
                base.Map.overlayDrawer.DrawOverlay(this, OverlayTypes.BurningWick);
            }
        }

    }
}
