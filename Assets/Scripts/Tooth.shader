Shader "CUSTOM/Tooth"
{
    Properties
    {
        _Color("Color", color) = (1, 1, 1, 1)
        _CleanTex ("Clean Texture", 2D) = "white" {}
        _DirtyTex ("Dirty Texture", 2D) = "white" {}
        _Dirtyness("Dirtyness", Range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _CleanTex;
            sampler2D _DirtyTex;
            Float _Dirtyness;   

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }   

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 ctextureColor = tex2D(_CleanTex, i.uv);
                fixed4 dtextureColor = tex2D(_DirtyTex, i.uv);
                return lerp(ctextureColor, dtextureColor, _Dirtyness) * _Color;
            }
            ENDCG
        }
    }
}
