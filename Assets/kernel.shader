Shader "Kernel/kernel"
{
    Properties
    {
       _RealTime ("-", Float) = 0
    }
    CGINCLUDE
    #include "UnityCG.cginc"
    float _RealTime; 

	float4 frag_update_pos(v2f_img i) : SV_Target {     
        return float4(2.0 * i.uv.x * 100.0, 5.0 * sin(i.uv.x * i.uv.y * 10000.0 / 200.0 + _RealTime), 2.0 * i.uv.y * 100.0, 0);
    }
    ENDCG
	SubShader
    {
        Pass 
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag_update_pos
            ENDCG
        }
    }
}