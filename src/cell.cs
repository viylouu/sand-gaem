using SimulationFramework;

public abstract class cell {
    public Color col;

    public abstract void update_cell(int x, int y);

    public static bool within_x_left(float x) => x > 0;
    public static bool within_x_right(float x) => x < main.cells.GetLength(0)-1;
    public static bool within_y_top(float y) => y < main.cells.GetLength(1)-1;
    public static bool within_y_bottom(float y) => y > 0;

    public static void swap(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        cell current = main.cells_next[x1,y1];
        main.cells_next[x1,y1] = main.cells_next[x2,y2];
        main.cells_next[x2,y2] = current;
    }

    public static void swap_with_null(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        main.cells_next[x2,y2] = main.cells_next[x1,y1];
        main.cells_next[x1,y1] = null;
    }

    public static void swap_with_texture_update(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap(x1,y1,x2,y2);

        main.tex[x1,y1] = main.cells_next[x1,y1].col;
        main.tex[x2,y2] = main.cells_next[x2,y2].col;
    }

    public static void swap_with_null_with_texture_update(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap_with_null(x1,y1,x2,y2);

        main.tex[x1,y1] = Color.Transparent;
        main.tex[x2,y2] = main.cells_next[x2,y2].col;
    }
}