using System.ComponentModel.DataAnnotations;
using SimulationFramework;
using thrustr.utils;

public class fire : cell {
    public int ticks;

    public fire() {
        col = Color.Lerp(
            Color.Red,
            Color.Orange,
            (float)main.r.NextDouble()
        );

        ticks = 0;
    }

    public override void update_cell(int x, int y) {
        ticks++;

        if(main.r.Next(0, 10) == 0)
            return;

        //spreading

        if(within_x_left(x))
            if(main.cells[x-1,y] != null && main.cells[x-1,y] is not fire)
                set_cell_with_texture_update(x-1,y, new fire());

        if(within_x_right(x))
            if(main.cells[x+1,y] != null && main.cells[x+1,y] is not fire)
                set_cell_with_texture_update(x+1,y, new fire());

        if(within_y_bottom(y))
            if(main.cells[x,y-1] != null && main.cells[x,y-1] is not fire)
                set_cell_with_texture_update(x,y-1, new fire());

        if(within_y_top(y))
            if(main.cells[x,y+1] != null && main.cells[x,y+1] is not fire)
                set_cell_with_texture_update(x,y+1, new fire());

        //remove after time

        if(ticks > main.r.Next(180,320))
            set_cell_with_texture_update(x,y, null);

        //go up

        if(within_y_top(y))
            if(main.cells[x,y+1] == null)
                swap_with_null_with_texture_update(x,y, x,y+1);

        //go left/right

        int dir = (int)math.round((float)main.r.NextDouble());

        if(main.r.Next(0,5) == 0) {
            if(dir == 0) {
                if(within_x_right(x))
                    if(main.cells[x+1,y] == null)
                        swap_with_null_with_texture_update(x,y, x+1,y);
            }

            if(within_x_left(x))
                if(main.cells[x-1,y] == null)
                    swap_with_null_with_texture_update(x,y, x-1,y);
        }

        //go upleft/upright

        dir = (int)math.round((float)main.r.NextDouble());

        if(dir == 0) {
            if(within_x_left(x) && within_y_top(y))
                if(main.cells[x-1,y+1] == null)
                    swap_with_null_with_texture_update(x,y, x-1,y+1);
        }

        if(within_x_right(x) && within_y_top(y))
            if(main.cells[x+1,y+1] == null)
                swap_with_null_with_texture_update(x,y, x+1,y+1);
    }
}