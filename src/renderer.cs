using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;

using thrustr.utils;

partial class main {
    public static ITexture tex;

    public static cell[,] cells = new cell[320,180];         // only used to check cells
    public static cell[,] cells_next = new cell[320,180];    // only used to set cells

    static Type sel_cel_type = typeof(sand);
    static byte sel_cel = 1;

    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        c.Scale(1,-1);
        c.DrawTexture(tex, 0,0, 320,180, Alignment.BottomLeft);
        c.ResetState();

        update();

        if(Mouse.IsButtonDown(MouseButton.Left))
            place_cell_screen_space(sel_cel_type, Iround(Mouse.Position.X), Iround(Mouse.Position.Y));

        switch(sel_cel) {
            case 0:
                sel_cel_type = null; break;
            case 1:
                sel_cel_type = typeof(sand); break;
        }

        sel_cel += (byte)Mouse.ScrollWheelDelta;

        if(sel_cel < 0)
            sel_cel = 0;

        sel_cel %= 2;

        c.DrawText(sel_cel_type?.Name ?? "air", 16, 318,2, Alignment.TopRight);
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