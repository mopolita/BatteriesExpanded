using System;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

namespace Batteries
{
    public class BarSize : DefModExtension
    {
        public float sizeX;
        public float sizeY;
    }

    public class Building_Minibattery : Building_Battery
    {
        private static readonly Vector2 BarSize = new Vector2(0.4f,0.4f);

        private static Vector2 ChangeBarSize(ThingDef thingDef) 
        {
            if (thingDef.HasModExtension<BarSize>())
            {
                float sizeX = thingDef.GetModExtension<BarSize>().sizeX;
                float sizeY = thingDef.GetModExtension<BarSize>().sizeY;
                return new Vector2(sizeX, sizeY);
            }
            return Vector2.zero;
        }
      
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
            if (field != null)
            {
                field.SetValue(this, barSize);
            }
        }

        public override void Draw()
        {
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

    public class Building_Megabattery : Building_Battery
    {
        private static readonly Vector2 BarSize = new Vector2(1.54f, 0.54f);

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
            if (!(field == null))
            {
                field.SetValue(this, barSize);
            }
        }

        public override void Draw()
        {
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
