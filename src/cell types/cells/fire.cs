using SimulationFramework;
using thrustr.utils;

public class fire : gas {
    public int ticks;

    public Type[] burnables = {
        typeof(termite),
        typeof(scaffold),
        typeof(neon)
    };

    public fire() {
        col = Color.Lerp(
            Color.Red,
            Color.Orange,
            (float)main.r.NextDouble()
        );

        ticks = 0;

        glow = true;
    }

    public override void update_cell(int x, int y) {
        ticks++;

        if(within_x_left(x))
            if(main.cells[x-1,y] != null)
                if(main.cells[x-1,y] is liquid) 
                    if((main.cells[x-1,y] as liquid).depletes_fire){
                        remove(x,y);
                        return;
                    }

        if(within_x_right(x))
            if(main.cells[x+1,y] != null)
                if(main.cells[x+1,y] is liquid)
                    if((main.cells[x+1,y] as liquid).depletes_fire){
                        remove(x,y);
                        return;
                    }

        if(within_y_top(y))
            if(main.cells[x,y+1] != null)
                if(main.cells[x,y+1] is liquid)
                    if((main.cells[x,y+1] as liquid).depletes_fire){
                        remove(x,y);
                        return;
                    }

        if(within_y_bottom(y))
            if(main.cells[x,y-1] != null)
                if(main.cells[x,y-1] is liquid)
                    if((main.cells[x,y-1] as liquid).depletes_fire){
                        remove(x,y);
                        return;
                    }

        if(main.r.Next(0, 10) == 0)
            return;

        //spreading

        int dir = 0;

        for(int i = 1; i <= main.r.Next(1,5); i++)
            if(main.r.Next(0,i*i+1) == 0) {
                dir = main.r.Next(0,4);

                if(dir == 0)
                    if(within_x_left(x,i))
                        if(main.cells[x-i,y] != null) {
                            bool burnable = false;
                            for(int j = 0; j < burnables.Length; j++)
                                if(main.cells[x-i,y].GetType() == burnables[j]) {
                                    burnable = true;
                                    break;
                                }

                            if(burnable) 
                                set_cell_with_both_texture_updates(x-i,y, new fire());
                        }

                if(dir == 1)
                    if(within_x_right(x,i))
                        if(main.cells[x+i,y] != null) {
                            bool burnable = false;
                            for(int j = 0; j < burnables.Length; j++)
                                if(main.cells[x+i,y].GetType() == burnables[j]) {
                                    burnable = true;
                                    break;
                                }

                            if(burnable)
                                set_cell_with_both_texture_updates(x+i,y, new fire());
                        }

                if(dir == 2)
                    if(within_y_bottom(y,i))
                        if(main.cells[x,y-i] != null) {
                            bool burnable = false;
                            for(int j = 0; j < burnables.Length; j++)
                                if(main.cells[x,y-i].GetType() == burnables[j]) {
                                    burnable = true;
                                    break;
                                }

                            if(burnable)
                                set_cell_with_both_texture_updates(x,y-i, new fire());
                        }

                if(dir == 3)
                    if(within_y_top(y,i))
                        if(main.cells[x,y+i] != null) {
                            bool burnable = false;
                            for(int j = 0; j < burnables.Length; j++)
                                if(main.cells[x,y+i].GetType() == burnables[j]) {
                                    burnable = true;
                                    break;
                                }

                            if(burnable)
                                set_cell_with_both_texture_updates(x,y+i, new fire());
                        }
            }

        //remove after time

        if(ticks > main.r.Next(180,320))
            set_cell_with_both_texture_updates(x,y, null);

        //go up

        if(within_y_top(y))
            if(main.cells[x,y+1] == null)
                swap_with_null_with_both_texture_updates(x,y, x,y+1);

        //go left/right

        dir = (int)math.round((float)main.r.NextDouble());

        if(main.r.Next(0,5) == 0) {
            if(dir == 0) {
                if(within_x_right(x))
                    if(main.cells[x+1,y] == null)
                        swap_with_null_with_both_texture_updates(x,y, x+1,y);
            }

            if(within_x_left(x))
                if(main.cells[x-1,y] == null)
                    swap_with_null_with_both_texture_updates(x,y, x-1,y);
        }

        //go upleft/upright

        dir = (int)math.round((float)main.r.NextDouble());

        if(dir == 0) {
            if(within_x_left(x) && within_y_top(y))
                if(main.cells[x-1,y+1] == null)
                    swap_with_null_with_both_texture_updates(x,y, x-1,y+1);
        }

        if(within_x_right(x) && within_y_top(y))
            if(main.cells[x+1,y+1] == null)
                swap_with_null_with_both_texture_updates(x,y, x+1,y+1);
    }
}