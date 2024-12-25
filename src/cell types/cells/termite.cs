using SimulationFramework;

using thrustr.utils;

public class termite : solid {
    public bool living;

    public bool wet;

    public static Type[] eatables = {
        typeof(scaffold)
    };

    public termite() {
        col = Color.Lerp(
            Color.Brown,
            Color.Chocolate,
            (float)main.r.NextDouble()
        );

        living = true;
        wet = false;
    }

    public override void update_cell(int x, int y) {
        if(main.r.Next(0,10) == 0)
            return;

        if(living) {
            if(main.r.Next(0,60) == 0) {
                int dir = 0;

                for(int i = 1; i <= main.r.Next(1,5); i++)
                    if(main.r.Next(0,i*i+1) == 0) {
                        dir = main.r.Next(0,4);

                        if(dir == 0)
                            if(within_x_left(x,i))
                                if(main.cells[x-i,y] != null) {
                                    bool eatable = false;
                                    for(int j = 0; j < eatables.Length; j++)
                                        if(main.cells[x-i,y].GetType() == eatables[j]) {
                                            eatable = true;
                                            break;
                                        }

                                    if(eatable)
                                        set_cell_with_texture_update(x-i,y, null);
                                }

                        if(dir == 1)
                            if(within_x_right(x,i))
                                if(main.cells[x+i,y] != null) {
                                    bool eatable = false;
                                    for(int j = 0; j < eatables.Length; j++)
                                        if(main.cells[x+i,y].GetType() == eatables[j]) {
                                            eatable = true;
                                            break;
                                        }

                                    if(eatable)
                                        set_cell_with_texture_update(x+i,y, null);
                                }

                        if(dir == 2)
                            if(within_y_bottom(y,i))
                                if(main.cells[x,y-i] != null) {
                                    bool eatable = false;
                                    for(int j = 0; j < eatables.Length; j++)
                                        if(main.cells[x,y-i].GetType() == eatables[j]) {
                                            eatable = true;
                                            break;
                                        }

                                    if(eatable)
                                        set_cell_with_texture_update(x,y-i, null);
                                }

                        if(dir == 3)
                            if(within_y_top(y,i))
                                if(main.cells[x,y+i] != null) {
                                    bool eatable = false;
                                    for(int j = 0; j < eatables.Length; j++)
                                        if(main.cells[x,y+i].GetType() == eatables[j]) {
                                            eatable = true;
                                            break;
                                        }

                                    if(eatable)
                                        set_cell_with_texture_update(x,y+i, null);
                                }
                    }
            }

            if(within_x_left(x)) {
                if(main.cells[x-1,y] is liquid)
                    stop_living(x,y);

                if(!living)
                    return;

                if(main.cells[x-1,y] is termite)
                    if((main.cells[x-1,y] as termite).wet)
                        stop_living(x,y);

                if(!living)
                    return;
            }

            if(within_x_right(x)) {
                if(main.cells[x+1,y] is liquid)
                    stop_living(x,y);

                if(!living)
                    return;

                if(main.cells[x+1,y] is termite)
                    if((main.cells[x+1,y] as termite).wet)
                        stop_living(x,y);

                if(!living)
                    return;
            }

            if(within_y_bottom(y)) {
                if(main.cells[x,y-1] is liquid)
                    stop_living(x,y);

                if(!living)
                    return;

                if(main.cells[x,y-1] is termite)
                    if((main.cells[x,y-1] as termite).wet)
                        stop_living(x,y);

                if(!living)
                    return;
            }

            if(within_y_top(y)) {
                if(main.cells[x,y+1] is liquid)
                    stop_living(x,y);

                if(!living)
                    return;

                if(main.cells[x,y+1] is termite)
                    if((main.cells[x,y+1] as termite).wet)
                        stop_living(x,y);

                if(!living)
                    return;
            }
        }

        if(within_y_bottom(y))
            if(main.cells[x,y-1] == null || main.cells[x,y-1] is water) {
                swap_with_texture_update(x,y, x,y-1);
                return;
            }

        if(!living) {
            if(within_y_bottom(y) && within_x_left(x))
                if(main.cells[x-1,y-1] == null || main.cells[x-1,y-1] is water) {
                    swap_with_texture_update(x,y, x-1,y-1);
                    return;
                }

            if(within_y_bottom(y) && within_x_right(x))
                if(main.cells[x+1,y-1] == null || main.cells[x+1,y-1] is water) {
                    swap_with_texture_update(x,y, x+1,y-1);
                    return;
                }
        }

        if(living) {
            int m_dir = (int)math.round((float)main.r.NextDouble()*4);

            switch(m_dir) {
                case 0:
                    if(within_x_left(x))
                        if(main.cells[x-1,y] == null) {
                            swap_with_texture_update(x,y, x-1,y);
                            return;
                        }
                    break;
                case 1:
                    if(within_x_left(x) && within_y_top(y))
                        if(main.cells[x-1,y+1] == null) {
                            swap_with_texture_update(x,y, x-1,y+1);
                            return;
                        }
                    break;
                case 2:
                    if(within_y_top(y))
                        if(main.cells[x,y+1] == null) {
                            swap_with_texture_update(x,y, x,y+1);
                            return;
                        }
                    break;
                case 3:
                    if(within_x_right(x) && within_y_top(y))
                        if(main.cells[x+1,y+1] == null) {
                            swap_with_texture_update(x,y, x+1,y+1);
                            return;
                        }
                    break;
                case 4:
                    if(within_x_right(x))
                        if(main.cells[x+1,y] == null) {
                            swap_with_texture_update(x,y, x+1,y);
                            return;
                        }
                    break;
            }
        }
    }

    void stop_living(int x, int y) {
        living = false;
        wet = true;
        col = Color.Lerp(col, Color.Blue, (float)main.r.NextDouble()*.25f+.25f);
        main.tex[x,y] = col;
    }
} 