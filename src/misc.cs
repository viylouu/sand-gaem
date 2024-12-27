using SimulationFramework;
using SimulationFramework.Input;

using thrustr.utils;

partial class main {
    static int Iround(float val) => (int)math.round(val);

    static void place_cell(Type cell_type, int x, int y) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;

        cell.update_around(x,y);
        
        if(cell_type == null) {
            cells[x,y] = null;
            tex[x,y] = Color.Transparent;
            btex[x, y] = Color.Transparent;
        } else {
            cell c = (cell)Activator.CreateInstance(cell_type);

            cells[x, y] = c;
            tex[x, y] = c.col;
            if(c.glow)
                btex[x, y] = c.col;
            else
                btex[x, y] = Color.Transparent;
        }
    }

    static void place_cell_with_no_override(Type cell_type, int x, int y) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;
        
        if(cells[x,y] != null)
            return;

        cell.update_around(x,y);

        if(cell_type == null) {
            cells[x,y] = null;
            tex[x,y] = Color.Transparent;
            btex[x, y] = Color.Transparent;
        } else {
            cell c = (cell)Activator.CreateInstance(cell_type);

            cells[x, y] = c;
            tex[x, y] = c.col;
            if(c.glow)
                btex[x, y] = c.col;
            else
                btex[x, y] = Color.Transparent;
        }
    }

    static void place_cell_screen_space(Type cell_type, int x, int y) {
        y = 360-y;

        place_cell(cell_type, x, y);
    }

    static void place_cell_with_no_override_screen_space(Type cell_type, int x, int y) {
        y = 360-y;

        place_cell_with_no_override(cell_type, x, y);
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
        
        cell.update_around(x,y);

        if(c == null) {
            cells[x,y] = null;
            tex[x,y] = Color.Transparent;
        } else {
            cells[x, y] = c;
            tex[x, y] = c.col;
        }
    }

    static void select_cell() {
        sel_cel_type = cell_types[sel_cel];

        if(sel_cel+Mouse.ScrollWheelDelta < 0)
            sel_cel = 0;
        else
            sel_cel = (byte)(sel_cel + Mouse.ScrollWheelDelta);

        if(sel_cel > cell_types.Length-1)
            sel_cel = (byte)(cell_types.Length-1);
    }

    static Type[] cell_types = {
        null,
        typeof(sand),
        typeof(stone),
        typeof(fire),
        typeof(scaffold),
        typeof(water),
        typeof(termite),
        typeof(clearer),
        typeof(neon)
    };
}