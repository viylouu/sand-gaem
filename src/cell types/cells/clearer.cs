using SimulationFramework;

public class clearer : cell {
    public int lifetime;

    public clearer() {
        col = Color.Red;

        glow = true;
        update = true;
    }

    public override void update_cell(int x, int y) {
        lifetime++;

        if(lifetime >= 15) {
            remove(x,y);
            return;
        }

        if(!update)
            return;

        if(within_x_left(x))
            if(main.cells[x-1,y] is not clearer)
                set_cell_with_both_texture_updates(x-1,y, new clearer());

        if(within_x_right(x))
            if(main.cells[x+1,y] is not clearer)
                set_cell_with_both_texture_updates(x+1,y, new clearer());

        if(within_y_bottom(y))
            if(main.cells[x,y-1] is not clearer)
                set_cell_with_both_texture_updates(x,y-1, new clearer());

        if(within_y_top(y))
            if(main.cells[x,y+1] is not clearer)
                set_cell_with_both_texture_updates(x,y+1, new clearer());

        ticks_since_last_updated++;

        if(ticks_since_last_updated >= max_ticks_without_update)
            update = false;
    }
}