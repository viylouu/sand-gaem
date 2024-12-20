using SimulationFramework;

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
        y = 180-y;

        place_cell(cell_type, x, y);
    }

    static void place_cell_screen_space_with_remap(Type cell_type, int x, int y) {
        x /= Window.Width;
        y /= Window.Height;

        x *= cells.GetLength(0);
        y *= cells.GetLength(1);

        place_cell_screen_space(cell_type, x,y);
    }
}