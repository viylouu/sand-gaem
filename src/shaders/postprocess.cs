using System.Numerics;
using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Drawing.Shaders;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;

partial class postprocess : CanvasShader {
    public ITexture tex;
    public ITexture btex;

    float e = 2.71828f;
    float tau = 6.283185307179586f;

    float gauss(int x, int y, int sigma) {
        return Pow(1/tau*sigma*sigma*e,-(x*x+y*y)/(2*sigma*sigma));
    }

    public override ColorF GetPixelColor(Vector2 pos) {
        Vector4 blur = new();

        int rad = 16, trad = 0;

        for(int i = -rad; i <= rad; i++)
            trad++;

        for(int x = -rad; x <= rad; x++)
            for(int y = -rad; y <= rad; y++) {
                ColorF col = btex.Sample(new Vector2(x,y) + pos);
                blur += new Vector4(col.R,col.G,col.B,col.A)*gauss(x,y,trad); 
            }

        blur /= trad*trad;

        blur *= 2.25f;

        return ColorF.Lerp(tex.Sample(pos), new ColorF(blur.X,blur.Y,blur.Z,1), Clamp(blur.W,0,1));
    }
}