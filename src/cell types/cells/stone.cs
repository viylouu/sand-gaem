using SimulationFramework;

public class stone : solid {
    public stone() {
        col = Color.Lerp(
            Color.DimGray,
            Color.DarkGray,
            (float)main.r.NextDouble()
        );
    }

    public override void update_cell(int x, int y) {
        if(within_x_left(x) && within_x_right(x) && within_y_top(y))
            if(main.cells[x-1,y+1] is stone && main.cells[x+1,y+1] is stone)
                return;

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null || main.cells[x,y-1] is liquid)
                swap_with_texture_update(x,y, x,y-1);
    }
}