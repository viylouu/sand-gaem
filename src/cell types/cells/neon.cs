using SimulationFramework;

public class neon : solid {
    public neon() {
        col = Color.Lerp(
            Color.White,
            Color.GhostWhite,
            (float)main.r.NextDouble()
        );

        glow = true;
    }

    public override void update_cell(int x, int y) {
        if(main.r.Next(0, 2) == 0)
            return;

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null || main.cells[x,y-1] is liquid) {
                swap_with_both_texture_updates(x,y, x,y-1);
                return;
            }

        if(within_x_left(x) && within_y_bottom(y))
            if(main.cells[x-1,y-1] == null || main.cells[x-1,y-1] is liquid) {
                swap_with_both_texture_updates(x,y, x-1,y-1);
                return;
            }

        if(within_x_right(x) && within_y_bottom(y))
            if(main.cells[x+1,y-1] == null || main.cells[x+1,y-1] is liquid) {
                swap_with_both_texture_updates(x,y, x+1,y-1);
                return;
            }
    }
}