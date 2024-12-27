using System.Collections.Specialized;
using System.Numerics;
using Silk.NET.OpenAL;
using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;

using thrustr.utils;

partial class main {
    public static ITexture tex;
    public static ITexture btex;

    public static cell[,] cells = new cell[640,360];
    public static bool[,] updateds;

    static Type sel_cel_type = typeof(sand);
    static byte sel_cel = 1;

    static int place_radius = 4;

    static float fps = 1f/15;
    static float timer;

    static postprocess post_shader = new();

    static Vector2 square_start_pos;
    static Vector2 square_end_pos;

    static bool squaring;

    static bool post_processing = true;
    static bool fixed_updating = false;

    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        post_shader.tex = tex;
        post_shader.btex = btex;

        DateTime curtime = DateTime.UtcNow;

        c.Scale(1,-1);
        if(post_processing) {
            c.Fill(post_shader);
            c.DrawRect(0,0, 640,360, Alignment.BottomLeft);
        } else
            c.DrawTexture(tex,0,0,640,360,Alignment.BottomLeft);
        c.ResetState();

        c.Flush();

        Console.Clear();

        Console.WriteLine($"render speed: {(DateTime.UtcNow-curtime).TotalMilliseconds} milliseconds");

        curtime = DateTime.UtcNow;

        if(fixed_updating) {
            timer += Time.DeltaTime;

            if(timer >= fps) {
                timer = 0;
                update();
            }
        } else
            update();

        Console.WriteLine($"update speed: {(DateTime.UtcNow-curtime).TotalMilliseconds} milliseconds");

        if(Mouse.IsButtonDown(MouseButton.Right)) {
            if(!squaring)
                square_start_pos = Mouse.Position;

            squaring = true;
        }

        if(Mouse.IsButtonReleased(MouseButton.Right)) {
            squaring = false;

            square_end_pos = Mouse.Position;

            int startx = 0;
            int endx = 0;

            int starty = 0;
            int endy = 0;

            if(square_start_pos.X > square_end_pos.X) {
                startx = (int)square_end_pos.X;
                endx = (int)square_start_pos.X;
            } else {
                endx = (int)square_end_pos.X;
                startx = (int)square_start_pos.X;
            }
 
            if(square_start_pos.Y > square_end_pos.Y) {
                starty = (int)square_end_pos.Y;
                endy = (int)square_start_pos.Y;
            } else {
                endy = (int)square_end_pos.Y;
                starty = (int)square_start_pos.Y;
            }

            for(int x = startx; x < endx; x++)
                for(int y = starty; y < endy; y++) {
                    if(sel_cel_type == null)
                        place_cell_screen_space(null, x,y);
                    else
                        place_cell_with_no_override_screen_space(sel_cel_type, x,y);
                }
        }

        if(squaring) {
            c.Stroke(Color.Green);
            c.DrawRect(square_start_pos,Mouse.Position-square_start_pos,Alignment.TopLeft);
            c.ResetState();
        }

        if(Mouse.IsButtonDown(MouseButton.Left)) {
            for(int x = -place_radius; x <= place_radius; x++)
                for(int y = -place_radius; y <= place_radius; y++)
                    if(math.sqrdist(Mouse.Position, Mouse.Position + new Vector2(x,y)) < place_radius*place_radius) {
                        if(sel_cel_type == null)
                            place_cell_screen_space(null, Iround(Mouse.Position.X)+x, Iround(Mouse.Position.Y)+y);
                        else
                            place_cell_with_no_override_screen_space(sel_cel_type, Iround(Mouse.Position.X)+x, Iround(Mouse.Position.Y)+y);
                    }
        }

        select_cell();

        c.DrawText(sel_cel_type?.Name ?? "air", 16, 638,2, Alignment.TopRight);

        c.DrawText($"{math.round(1/Time.DeltaTime)} fps", 16, 2,2, Alignment.TopLeft);
    }

    static void update() {
        updateds = new bool[cells.GetLength(0),cells.GetLength(1)];

        for(int x = 0; x < cells.GetLength(0); x++)
            for(int y = 0; y < cells.GetLength(1); y++)
                if(cells[x,y] != null && !updateds[x,y])
                    cells[x,y].update_cell(x,y);
        
        tex.ApplyChanges();
        btex.ApplyChanges();
    }
}