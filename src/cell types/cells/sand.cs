using SimulationFramework;

public class sand : solid {
    public sand() {
        col = Color.Lerp(
            Color.Peru,
            Color.Yellow,
            (float)main.r.NextDouble()
        );

        glow = false;
        update = true;
    }

    public override void update_cell(int x, int y) {
        if(!update)
            return;

        if(main.r.Next(0, 10) == 0)
            return;

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null || main.cells[x,y-1] is liquid) {
                swap_with_texture_update(x,y, x,y-1);
                return;
            }

        if(within_x_left(x) && within_y_bottom(y))
            if(main.cells[x-1,y-1] == null || main.cells[x-1,y-1] is liquid) {
                swap_with_texture_update(x,y, x-1,y-1);
                return;
            }

        if(within_x_right(x) && within_y_bottom(y))
            if(main.cells[x+1,y-1] == null || main.cells[x+1,y-1] is liquid) {
                swap_with_texture_update(x,y, x+1,y-1);
                return;
            }

        ticks_since_last_updated++;

        if(ticks_since_last_updated >= max_ticks_without_update)
            update = false;
    }
}