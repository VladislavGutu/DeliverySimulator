Shader "Unlit/14_rainmipmap" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_GridSize ("Grid-Size", Float) = 1
		_Distort ("Distortion", Range(-4, 4)) = 1
		_Blur ("Blur", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}