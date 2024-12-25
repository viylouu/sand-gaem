using SimulationFramework;
using thrustr.utils;

public class water : liquid {
    public water() {
        col = Color.Lerp(
            Color.Blue,
            Color.DodgerBlue,
            (float)main.r.NextDouble()
        );

        depletes_fire = true;
    }

    public override void update_cell(int x, int y) {
        if(main.r.Next(0, 10) == 0)
            return;

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null) {
                swap_with_null_with_texture_update(x,y, x,y-1);
                return;
            }

        if(within_x_left(x) && within_y_bottom(y))
            if(main.cells[x-1,y-1] == null) {
                swap_with_null_with_texture_update(x,y, x-1,y-1);
                return;
            }

        if(within_x_right(x) && within_y_bottom(y))
            if(main.cells[x+1,y-1] == null) {
                swap_with_null_with_texture_update(x,y, x+1,y-1);
                return;
            }

        int dir = (int)math.round((float)main.r.NextDouble());

        if(dir == 0) {
            if(within_x_left(x))
                if(main.cells[x-1,y] == null) {
                    swap_with_null_with_texture_update(x,y, x-1,y);
                    return;
                }
        }

        if(within_x_right(x))
            if(main.cells[x+1,y] == null) {
                swap_with_null_with_texture_update(x,y, x+1,y);
                return;
            }
    }
}