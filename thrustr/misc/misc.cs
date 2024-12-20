using System.Numerics;
using SimulationFramework;
using SimulationFramework.Drawing;

namespace thrustr.utils;

public static class misc_extentions {
    public static void trydispose(this ITexture todisp) {
        if(todisp != null)
            todisp.Dispose();
    }

    public static void trydispose(this ISound todisp) {
        if(todisp != null)
            todisp.Dispose();
    }

    public static void trydispose(this SoundPlayback todisp) {
        if(todisp != null)
            todisp.Dispose();
    }

    public static void DrawText(this ICanvas c, string text, float size, float x, float y, Alignment align = Alignment.TopLeft) => c.DrawAlignedText(text, size, x,y, align);
    public static void DrawText(this ICanvas c, string text, float size, Vector2 pos, Alignment align = Alignment.TopLeft) => c.DrawAlignedText(text, size, pos, align);
}