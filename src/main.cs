using SimulationFramework;
using SimulationFramework.Drawing;

partial class main {
    public static Random r = new();

    static void Main() {
        Simulation sim = Simulation.Create(init, rend);
        sim.Run();
    }

    static void init() {
        Window.Title = "balls";

        tex = Graphics.CreateTexture(640,360);

        Simulation.SetFixedResolution(640,360,Color.Black);
    }
}