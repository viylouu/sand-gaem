using SimulationFramework;

partial class main {
    static void Main() {
        Simulation sim = Simulation.Create(init, rend);
        sim.Run();
    }

    static void init() {
        Window.Title = "balls";
    }
}