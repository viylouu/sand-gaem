using SimulationFramework;

public class scaffold : solid {
    public scaffold() {
        col = Color.Lerp(
            Color.BurlyWood,
            Color.BlanchedAlmond,
            (float)main.r.NextDouble()
        );

        glow = false;
        update = true;
    }

    public override void update_cell(int x, int y) {
        if(!update)
            return;

        if(within_x_left(x) && within_x_right(x))
            if(main.cells[x-1,y] is scaffold && main.cells[x+1,y] is scaffold)
                return;

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null || main.cells[x,y-1] is liquid)
                swap_with_texture_update(x,y, x,y-1);

        ticks_since_last_updated++;

        if(ticks_since_last_updated >= max_ticks_without_update)
            update = false;
    }
}