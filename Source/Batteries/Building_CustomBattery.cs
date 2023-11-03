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

        private static Vector2 BarSize = new Vector2(0.4f, 0.4f);

        private Vector2 GetBarSize()
        {
            FieldInfo field = typeof(Building_Battery).GetField("BarSize", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
            if (field == null)
            {
                return Vector2.zero;
            }
            object value = field.GetValue(this);
            return (Vector2)value;
        }
        private void SetBarSize(Vector2 barSize)
        {
            FieldInfo field = typeof(Building_Battery).GetField("BarSize", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField);
            field?.SetValue(this, barSize);
        }
        public override void Draw()
        {
            if (Props != null)
            {
                BarSize = new Vector2(Props.sizeX, Props.sizeY);
            }
            Vector2 barSize = GetBarSize();
            SetBarSize(BarSize);
            try
            {
                base.Draw();
            }
            finally
            {
                SetBarSize(barSize);
            }
        }
    }
}
