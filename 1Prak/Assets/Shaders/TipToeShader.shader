Shader "Custom/TipToeShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}

        _Vanishing("Vanishing", Range(0,1)) = 0
        [HDR]_LightBandColor("Lightband Color", Color) = (1,1,1,1) //HDR ist wichtig f?r Bloom
        _LightBandThreshold("Light Band Threshold", Range(0,1)) = 0.1
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent"}
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows alpha:fade

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _NoiseTex;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_NoiseTex;
            };

            half _Vanishing;
            half _LightBandThreshold;

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;
            fixed4 _LightBandColor;


            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                fixed4 light_c = tex2D(_MainTex, IN.uv_MainTex) * _LightBandColor;
                //hier den ?bergang anwenden
                fixed4 vanishValues = tex2D(_NoiseTex, IN.uv_NoiseTex);
                fixed vanishValue = vanishValues.r;
                //ausserhalb des lightband, wird normal angezeigt
                if (vanishValue > _Vanishing + _LightBandThreshold) {
                    o.Albedo = c.rgb; // das ?ndern
                    o.Alpha = c.a;
                }//innerhalb des lightband
                else if (vanishValue >= _Vanishing - _LightBandThreshold && vanishValue <= _Vanishing + _LightBandThreshold) {
                    //hier wird der output color gesetzt
                    o.Albedo = light_c.rgb; // das ?ndern
                    o.Alpha = light_c.a;
                }//ausserhalb des lightband, wird transparent angezeigt
                else if (vanishValue < _Vanishing - _LightBandThreshold) {
                    //hier wird der output color gesetzt
                    o.Albedo = c.rgb;
                    o.Alpha = 0;
                }



                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;

            }
            ENDCG
        }
            FallBack "Diffuse"

}