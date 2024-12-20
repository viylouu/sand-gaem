using thrustr.utils;

partial class main {
    static int Iround(float val) => (int)math.round(val);

    static void place_cell(cell c, int x, int y) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;
        
        cells[x, y] = c;
        tex[x, y] = c.col;
    }

    static void place_cell_screen_space(cell c, int x, int y) {
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
            return;
        
        y = 180-y;

        cells[x,y] = c;
        tex[x,y] = c.col;
    }
}