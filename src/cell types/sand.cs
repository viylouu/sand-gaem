using SimulationFramework;

public class sand : cell {
    public sand() {
        col = Color.Lerp(
            Color.Blue,
            Color.Red,
            (float)main.r.NextDouble()
        );
    }

    public override void update_cell(int x, int y) {
        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null)
                swap_with_null_with_texture_update(x,y, x,y-1);

        if(within_x_left(x) && within_y_bottom(y))
            if(main.cells[x-1,y-1] == null)
                swap_with_null_with_texture_update(x,y, x-1,y-1);

        if(within_x_right(x) && within_y_bottom(y))
            if(main.cells[x+1,y-1] == null)
                swap_with_null_with_texture_update(x,y, x+1,y-1);
    }
}