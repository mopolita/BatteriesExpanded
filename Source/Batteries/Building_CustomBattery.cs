using System;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

namespace Batteries
{
    public class Building_CustomBattery : Building_Battery
    {
        public ModExtension_BatterySize Props => def.GetModExtension<ModExtension_BatterySize>();

        private void SetBarSize()
        {
            FieldInfo field = typeof(Building_Battery).GetField("BarSize", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField);
            if (field != null)
            {
                if (Props != null)
                {
                    field.SetValue(this, new Vector2(Props.sizeX, Props.sizeY));
                }
                else
                {
                    field.SetValue(this, new Vector2(1.3f, 0.4f));
                }
            }
        }

        public override void Draw()
        {
            SetBarSize();
            try
            {
                base.Draw();
            }
            finally
            {
                SetBarSize();
            }
        }
    }
}
