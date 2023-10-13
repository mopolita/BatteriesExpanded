using System;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

namespace Batteries
{
    public class Building_Minibattery : Building_Battery
    {
        private static readonly Vector2 BarSize = new Vector2(0.4f, 0.4f);

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
