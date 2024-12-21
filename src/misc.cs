using SimulationFramework;
using SimulationFramework.Input;

using thrustr.utils;

partial class main {
    static int Iround(float val) => (int)math.round(val);

    static void place_cell(Type cell_type, int x, int y) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;
        
        if(cell_type == null) {
            cells[x,y] = null;
            tex[x,y] = Color.Transparent;
        } else {
            cell c = (cell)Activator.CreateInstance(cell_type);

            cells[x, y] = c;
            tex[x, y] = c.col;
        }
    }

    static void place_cell_screen_space(Type cell_type, int x, int y) {
        y = 360-y;

        place_cell(cell_type, x, y);
    }

    static void place_cell_screen_space_with_remap(Type cell_type, int x, int y) {
        x /= Window.Width;
        y /= Window.Height;

        x *= cells.GetLength(0);
        y *= cells.GetLength(1);

        place_cell_screen_space(cell_type, x,y);
    }

    public static void set_cell(int x, int y, cell c) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;
        
        if(c == null) {
            cells_next[x,y] = null;
            tex[x,y] = Color.Transparent;
        } else {
            cells_next[x, y] = c;
            tex[x, y] = c.col;
        }
    }

    static void select_cell() {
        switch(sel_cel) {
            case 0:
                sel_cel_type = null; break;
            case 1:
                sel_cel_type = typeof(sand); break;
            case 2:
                sel_cel_type = typeof(stone); break;
            case 3:
                sel_cel_type = typeof(fire); break;
            case 4:
                sel_cel_type = typeof(scaffold); break;
            case 5:
                sel_cel_type = typeof(water); break;
        }

        sel_cel = (byte)(sel_cel + Mouse.ScrollWheelDelta);

        if(sel_cel > 5)
            sel_cel = 5;
    }
}