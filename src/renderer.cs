using SimulationFramework;
using SimulationFramework.Drawing;

using thrustr.utils;

partial class main {
    static void rend(ICanvas c) {
        c.Clear(Color.Black);
        c.DrawText("hi", 48, Window.Width/2,Window.Height/2, Alignment.Center);
    }
}