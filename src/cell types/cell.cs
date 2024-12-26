using SimulationFramework;

public abstract class cell {
    public Color col;
    public bool glow;

    public abstract void update_cell(int x, int y);

    public static bool within_x_left(float x) => x > 0;
    public static bool within_x_right(float x) => x < main.cells.GetLength(0)-1;
    public static bool within_y_top(float y) => y < main.cells.GetLength(1)-1;
    public static bool within_y_bottom(float y) => y > 0;

    public static bool within_x_left(float x, int amt) => x > amt;
    public static bool within_x_right(float x, int amt) => x < main.cells.GetLength(0)-amt;
    public static bool within_y_top(float y, int amt) => y < main.cells.GetLength(1)-amt;
    public static bool within_y_bottom(float y, int amt) => y > amt;

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

        if(main.cells_next[x1,y1] == null)
            main.tex[x1,y1] = Color.Transparent;
        else
            main.tex[x1,y1] = main.cells_next[x1,y1].col;

        if(main.cells_next[x2,y2] == null)
            main.tex[x2,y2] = Color.Transparent;
        else
            main.tex[x2,y2] = main.cells_next[x2,y2].col;
    }
    public static void swap_with_brightness_texture_update(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap(x1,y1,x2,y2);

        if(main.cells_next[x1,y1] == null)
            main.btex[x1,y1] = Color.Transparent;

        if(main.cells_next[x2,y2] == null)
            main.btex[x2,y2] = Color.Transparent;
        else
            main.btex[x2,y2] = main.cells_next[x2,y2].col;
    }
    public static void swap_with_both_texture_updates(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap(x1,y1,x2,y2);

        if(main.cells_next[x1,y1] == null)
            main.tex[x1,y1] = Color.Transparent;
        else
            main.tex[x1,y1] = main.cells_next[x1,y1].col;

        if(main.cells_next[x2,y2] == null)
            main.tex[x2,y2] = Color.Transparent;
        else
            main.tex[x2,y2] = main.cells_next[x2,y2].col;

        if(main.cells_next[x1,y1] == null)
            main.btex[x1,y1] = Color.Transparent;

        if(main.cells_next[x2,y2] == null)
            main.btex[x2,y2] = Color.Transparent;
        else
            main.btex[x2,y2] = main.cells_next[x2,y2].col;
    }

    public static void swap_with_null_with_texture_update(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap_with_null(x1,y1,x2,y2);

        main.tex[x1,y1] = Color.Transparent;
        main.tex[x2,y2] = main.cells_next[x2,y2].col;
    }
    public static void swap_with_null_with_brightness_texture_update(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap_with_null(x1,y1,x2,y2);

        main.btex[x1,y1] = Color.Transparent;
        main.btex[x2,y2] = main.cells_next[x2,y2].col;
    }
    public static void swap_with_null_with_both_texture_updates(int x1, int y1, int x2, int y2) {
        if(main.cells_next[x1,y1] == null)
            return;

        swap_with_null(x1,y1,x2,y2);

        main.tex[x1,y1] = Color.Transparent;
        main.tex[x2,y2] = main.cells_next[x2,y2].col;

        main.btex[x1,y1] = Color.Transparent;
        main.btex[x2,y2] = main.cells_next[x2,y2].col;
    }

    public static void remove(int x, int y) => set_cell_with_both_texture_updates(x, y, null);
    public static void set_cell(int x, int y, cell c) => main.cells_next[x,y] = c;
    public static void set_cell_with_texture_update(int x, int y, cell c) {
        set_cell(x,y,c);

        if(c == null)
            main.tex[x,y] = Color.Transparent;
        else
            main.tex[x,y] = main.cells_next[x,y].col;
    }
    public static void set_cell_with_brightness_texture_update(int x, int y, cell c) {
        set_cell(x,y,c);

        if(c == null)
            main.btex[x,y] = Color.Transparent;
        else
            main.btex[x,y] = main.cells_next[x,y].col;
    }
    public static void set_cell_with_both_texture_updates(int x, int y, cell c) {
        set_cell(x,y,c);

        if(c == null)
            main.tex[x,y] = Color.Transparent;
        else
            main.tex[x,y] = main.cells_next[x,y].col;

        if(c == null)
            main.btex[x,y] = Color.Transparent;
        else
            main.btex[x,y] = main.cells_next[x,y].col;
    }
}