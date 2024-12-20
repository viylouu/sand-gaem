using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;

partial class main {
    public static ITexture tex;

    public static cell[,] cells = new cell[320,180];         // only used to check cells
    public static cell[,] cells_next = new cell[320,180];    // only used to set cells

    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Scale(1,-1);
        c.DrawTexture(tex, 0,0, 320,180, Alignment.BottomLeft);
        c.ResetState();

        update();

        if(Mouse.IsButtonDown(MouseButton.Left))
            place_cell_screen_space(new sand(), Iround(Mouse.Position.X), Iround(Mouse.Position.Y));
    }

    static void update() {
        cells_next = cells;

        for(int x = 0; x < cells.GetLength(0); x++)
            for(int y = 0; y < cells.GetLength(1); y++)
                if(cells[x,y] != null)
                    cells[x,y].update_cell(x,y);

        cells = cells_next;
        
        tex.ApplyChanges();
    }
}