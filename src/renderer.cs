using System.Numerics;
using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;

using thrustr.utils;

partial class main {
    public static ITexture tex;

    public static cell[,] cells = new cell[640,360];         // only used to check cells
    public static cell[,] cells_next = new cell[640,360];    // only used to set cells

    static Type sel_cel_type = typeof(sand);
    static byte sel_cel = 1;

    static int place_radius = 4;

    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Scale(1,-1);
        c.DrawTexture(tex, 0,0, 640,360, Alignment.BottomLeft);
        c.ResetState();

        update();

        if(Mouse.IsButtonDown(MouseButton.Left)) {
            for(int x = -place_radius; x <= place_radius; x++)
                for(int y = -place_radius; y <= place_radius; y++)
                    if(math.sqrdist(Mouse.Position, Mouse.Position + new Vector2(x,y)) < place_radius*place_radius)
                        place_cell_screen_space(sel_cel_type, Iround(Mouse.Position.X)+x, Iround(Mouse.Position.Y)+y);
        }

        select_cell();

        c.DrawText(sel_cel_type?.Name ?? "air", 16, 638,2, Alignment.TopRight);

        c.DrawText($"{math.round(1/Time.DeltaTime)} fps", 16, 2,2, Alignment.TopLeft);
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