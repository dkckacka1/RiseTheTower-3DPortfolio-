Shader "Custom/UI/StageBackGroundShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _UVOffsetX("UVOffsetX", Range(0,1)) = 0
        _UVOffsetY("UVOffsetY", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _UVOffsetX;
        float _UVOffsetY;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, float2 (IN.uv_MainTex.x + _UVOffsetX, IN.uv_MainTex.y + _UVOffsetY));
            o.Emission = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
